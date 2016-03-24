using Microsoft.Dynamics.AX.Framework.Tools.DataManagement.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace RecurringIntegrationApp
{
    /// <summary>
    /// Concrete strategy that implements the DataFlowNetworkStrategy.
    /// Models a data flow network that represents the client side runtime/ 
    /// state machine to process data files as they are submitted and processed
    /// with a recurring AX7 DIXF endpoint
    /// </summary>
    internal class DefaultDataFlowNetwork : DataFlowNetworkStrategy, IDisposable
    {
        /// <summary>
        /// Input queue to track documents in the input queue
        /// </summary>
        public ConcurrentQueue<ClientDataMessage> InputQueue { get; set; }

        /// <summary>
        /// Queue that tracks inprocess documents
        /// </summary>
        private ConcurrentQueue<ClientDataMessage> InprocessQueue { get; set; }

        /// <summary>
        /// Intermediate queue that tracks enqueued jobs that do not have a 
        /// "completion" status.
        /// </summary>
        private ConcurrentDictionary<string, ClientDataMessage> EnqueuedJobs { get; set; }

        /// <summary>
        /// Input job count
        /// </summary>
        private int inputJobCount = 0;

        /// <summary>
        /// Inprocess job count
        /// </summary>
        private int inprocessJobCount = 0;

        /// <summary>
        /// Failed job count
        /// </summary>
        private int failedJobCount = 0;

        /// <summary>
        /// Successful job count
        /// </summary>
        private int successfulJobCount = 0;

        /// <summary>
        /// Timer for status polling
        /// </summary>
        private System.Timers.Timer statusPoller = null;

        public DefaultDataFlowNetwork(DIXFRecurringJobsProcessor formInstance): base(formInstance)
        {
            // Init queues
            InputQueue = new ConcurrentQueue<ClientDataMessage>();

            InprocessQueue = new ConcurrentQueue<ClientDataMessage> ();

            EnqueuedJobs = new ConcurrentDictionary<string, ClientDataMessage>();

            // Init timer
            statusPoller = new System.Timers.Timer(Settings.StatusPollingInterval);
            statusPoller.Elapsed += async (sender, e) => await StatusPollerElapsed(sender, e);
            statusPoller.Start();
        }

        public override void Initialize()
        {            
            Task.Run(() => this.postExistingDataMessagesAsync());
        }

        public override async Task PostMessageAsync(ClientDataMessage dataMessage)
        {            
            // Update stats
            Interlocked.Increment(ref inputJobCount);

            base.formInstance.updateStats(StatType.Input, inputJobCount);

            // Enqueue document to the input queue
            this.InputQueue.Enqueue(dataMessage);

            // Trigger processing
            await this.processInputQueue();
        }

        public override void TearDown()
        {
            if (statusPoller != null)
            {
                statusPoller.Close();
                statusPoller.Dispose();
                statusPoller = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// IDisposable impl
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                statusPoller.Close();
                statusPoller.Dispose();
            }            
        }

        /// <summary>
        /// Processes input queue by dequeueing each
        /// item, moving them to the in process location and
        /// then enqueuing them to the in process queue.
        /// </summary>
        /// <returns>Task object for continuation</returns>
        public async Task processInputQueue()
        {
            if (!InputQueue.IsEmpty)
            {
                ClientDataMessage dataMessage;

                // For each item in the input queue, dequeue and move
                // item to the in process queue
                while (InputQueue.TryDequeue(out dataMessage))
                {
                    var targetDataMessage = new ClientDataMessage()
                    {
                        Name = dataMessage.Name,
                        FullPath = Path.Combine(Settings.InProcessDir, dataMessage.Name),
                        MessageStatus = MessageStatus.InProcess
                    };

                    // Move to inprocess location
                    await this.moveDataToTargetAsync(dataMessage, targetDataMessage);                        

                    // Enqueue to the inprocess queue
                    InprocessQueue.Enqueue(targetDataMessage);                    

                    // Update stats
                    base.formInstance.logText("File:  " + targetDataMessage.Name + " -  moved to inprocess location.");

                    Interlocked.Increment(ref inprocessJobCount);

                    base.formInstance.updateStats(StatType.Inprocess, inprocessJobCount);
                }
            }

            // Process uploads
            await processInprocessQueue();
        }

        /// <summary>
        /// Timer callback to invoke status check on enqueued 
        /// messages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns>Empty Task</returns>
        private async Task StatusPollerElapsed(object sender, ElapsedEventArgs e)
        {
            if (EnqueuedJobs.Count > 0)
            {
                base.formInstance.logText("Checking enqueued jobs' status...");
            }

            foreach (KeyValuePair<string, ClientDataMessage> item in EnqueuedJobs)
            {
                // Check status for current item with message id - item.Key
                var jobStatusDetail = await getStatus(item.Key);

                // If status was found and is not null, 
                if (jobStatusDetail != null)
                {
                    // post process the status received
                    var postProcessSucceeded = await postProcessMessageAsync(jobStatusDetail, item.Value);

                    // If the status received is any of the completion state (any of the error state or processed state)
                    // the treat processing to be complete and remove the item from the tracked dictionary
                    if (postProcessSucceeded)
                    {
                        ClientDataMessage _removed;

                        EnqueuedJobs.TryRemove(item.Key, out _removed);
                    }
                }
            }
        }

        private async Task<bool> processInprocessQueue()
        {
            if (!InprocessQueue.IsEmpty)
            {
                ClientDataMessage dataMessage;

                // For each document in the in process queue, read async and
                // submit to the AX7 endpoint to enqueue.
                while (InprocessQueue.TryDequeue(out dataMessage))
                {
                    IDataSource<ClientDataMessage> sourceDataSource = 
                        DataSourceFactory.GetDataSourceForMessage(dataMessage);                    

                    Stream sourceStream = sourceDataSource.Read(dataMessage);

                    if (sourceStream != null)
                    {
                        try
                        {
                            sourceStream.Seek(0, SeekOrigin.Begin);

                            var httpClientHelper = new HttpClientHelper();
                            string correlationId = dataMessage.Name;
                            Uri enqueueUri = httpClientHelper.GetEnqueueUri();

                            base.formInstance.logText("Enqueueing job:  " + dataMessage.Name + ". File size:  " + sourceStream.Length + " bytes.");

                            // Post Enqueue request
                            var response = await httpClientHelper.SendPostRequestAsync(enqueueUri, sourceStream, correlationId);

                            if (response.IsSuccessStatusCode)
                            {
                                // Log success and add to Enqueued jobs for further processing
                                var messageId = await response.Content.ReadAsStringAsync();

                                // Log enqueue success
                                base.formInstance.logText("File:  " + dataMessage.Name + " -  enqueued successfully.");

                                base.formInstance.logText("Message identifier for: " + dataMessage.Name + " - is: " + messageId);

                                // Queue for futher status processing
                                EnqueuedJobs.TryAdd(messageId, new ClientDataMessage(dataMessage.FullPath, MessageStatus.Enqueued));
                            }
                            else
                            {
                                // Enqueue failed. Move message to error location.

                                base.formInstance.logText("Enqueue failed for file:  " + dataMessage.Name);

                                base.formInstance.logText("Failure response:  Status: " + response.StatusCode + ", Reason: " + response.ReasonPhrase);

                                var targetDataMessage = new ClientDataMessage()
                                {
                                    Name = dataMessage.Name,
                                    FullPath = Path.Combine(Settings.ErrorDir, dataMessage.Name),
                                    MessageStatus = MessageStatus.Failed
                                };

                                // Move data to error location
                                await this.moveDataToTargetAsync(dataMessage, targetDataMessage);

                                // Enqueue failure
                                await this.updateStatsAsync(null, StatType.Failure, targetDataMessage, response);
                            }
                        }
                        catch (Exception _ex)
                        {
                            base.formInstance.logText("Failure processing file: " + dataMessage.Name + ".Exception : " + _ex.Message);
                        }
                        finally
                        {
                            if (sourceStream != null)
                            {
                                sourceStream.Close();
                                sourceStream.Dispose();
                            }
                        }

                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Get submitted job status using the Enqueue response -
        /// MessageId
        /// </summary>
        /// <param name="messageId">Correlation identifier for the submitted job returned as the Enqueue response</param>
        /// <returns>DataJobStatusDetail object that includes detailed job status</returns>
        private async Task<DataJobStatusDetail> getStatus(string messageId)
        {
            DataJobStatusDetail jobStatusDetail = null;

            /// get status
            UriBuilder statusUri = new UriBuilder(Settings.RainierUri);

            statusUri.Path = Program.JobStatusRelativePath + Settings.RecurringJobId;                            
              
            statusUri.Query = "jobId=" + messageId.Replace(@"""", "");

            //send a request to get the message status
            HttpClientHelper clientHelper = new HttpClientHelper();

            var response = await clientHelper.GetRequestAsync(statusUri.Uri);
            if (response.IsSuccessStatusCode)
            {   
                // Deserialize response to the DataJobStatusDetail object
                jobStatusDetail = JsonConvert.DeserializeObject<DataJobStatusDetail>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                base.formInstance.logText("Status call failed. Status code: " + response.StatusCode + " , Reason: " + response.ReasonPhrase);
            }            

            return jobStatusDetail;
        }

        /// <summary>
        /// Process message once status is received by moving document to the
        /// appropriate folder and writing out a log for the processed document
        /// </summary>
        /// <param name="jobStatusDetail">DataJobStatusDetail object</param>
        /// <param name="fileName">Name of the file whose status is being processed</param>
        /// <returns>Is post processing successful or not</returns>
        private async Task<bool> postProcessMessageAsync(DataJobStatusDetail jobStatusDetail, 
            ClientDataMessage dataMessage)
        {
            bool retVal = false;

            if (jobStatusDetail == null || jobStatusDetail.DataJobStatus == null)
                return retVal;

            switch (jobStatusDetail.DataJobStatus.DataJobState)
            {
                // Document was processed successfully with no pre, post or functional
                // errors
                case DataJobState.Processed:
                    {   
                        var targetDataMessage = new ClientDataMessage()
                        {
                            Name = dataMessage.Name,
                            FullPath = Path.Combine(Settings.SuccessDir, dataMessage.Name),
                            MessageStatus = MessageStatus.Succeeded
                        };

                        await this.moveDataToTargetAsync(dataMessage, targetDataMessage);

                        await this.updateStatsAsync(jobStatusDetail, StatType.Success, targetDataMessage);

                        retVal = true;
                    }
                    break;

                // Document had some processing error
                case DataJobState.PostProcessError:
                case DataJobState.PreProcessError:
                case DataJobState.ProcessedWithErrors:
                    {                        
                        var targetDataMessage = new ClientDataMessage()
                        {
                            Name = dataMessage.Name,
                            FullPath = Path.Combine(Settings.ErrorDir, dataMessage.Name),
                            MessageStatus = MessageStatus.Failed
                        };

                        await this.moveDataToTargetAsync(dataMessage, targetDataMessage);

                        await this.updateStatsAsync(jobStatusDetail, StatType.Failure, targetDataMessage);                        
                    }
                    break;
            }

            return retVal;
        }

        /// <summary>
        /// Write the DataJobStatusDetail out as a status log for
        /// either the successful of failed processing of a document.
        /// </summary>
        /// <param name="jobStatusDetail">DataJobStatusDetail object</param>
        /// <param name="logFileFullPath">Full path of the log file to write out to</param>
        /// <returns>Empty task</returns>
        private async Task writeStatusLogFileAsync(DataJobStatusDetail jobStatusDetail, 
            ClientDataMessage targetDataMessage, 
            HttpResponseMessage httpResponse)
        {
            if (targetDataMessage == null)
                return;

            string logData = string.Empty;

            IDataSource<ClientDataMessage> logDataSource =
                        DataSourceFactory.GetDataSourceForMessage(targetDataMessage);

            var logFilePath = Path.Combine(Path.GetDirectoryName(targetDataMessage.FullPath),
                        (Path.GetFileNameWithoutExtension(targetDataMessage.FullPath) + 
                        "_log.txt"));

            if (null != jobStatusDetail)
            {
                logData = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(jobStatusDetail));
            }
            else if (null != httpResponse)
            {
                logData = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(httpResponse));
            }

            var targetLogDataMessage = new ClientDataMessage()
            {
                Name = Path.GetFileName(logFilePath),
                FullPath = logFilePath,
                MessageStatus = targetDataMessage.MessageStatus
            };

            
            using (var logMemoryStream = new MemoryStream(Encoding.Default.GetBytes(logData)))
            {
                logDataSource.Create(logMemoryStream, targetLogDataMessage);
            }

        }

        private async Task moveDataToTargetAsync(ClientDataMessage sourceDataMessage,
            ClientDataMessage targetDataMessage)
        {
            Stream sourceStream = null;

            IDataSource<ClientDataMessage> sourceDataSource =
                DataSourceFactory.GetDataSourceForMessage(sourceDataMessage);

            await Task.Run(() =>
            {
                try
                {
                    sourceStream = sourceDataSource.Read(sourceDataMessage);

                    if (sourceStream != null)
                    {
                        IDataSource<ClientDataMessage> targetDataSource =
                            DataSourceFactory.GetDataSourceForMessage(targetDataMessage);

                        targetDataSource.Create(sourceStream, targetDataMessage);

                        sourceStream.Dispose();

                        sourceDataSource.Delete(sourceDataMessage);
                    }

                }
                catch { }
                finally
                {
                    if (sourceStream != null)
                    {
                        sourceStream.Dispose();
                        sourceStream = null;
                    }
                }                
            });            
        }


        private async Task updateStatsAsync(DataJobStatusDetail jobStatusDetail, 
            StatType statusType, 
            ClientDataMessage targetDataMessage,
            HttpResponseMessage httpResponse = null)
        {
            switch(statusType)
            {
                case StatType.Success:

                    // Write log
                    await writeStatusLogFileAsync(jobStatusDetail, targetDataMessage, httpResponse);

                    // Update stats
                    Interlocked.Increment(ref successfulJobCount);

                    Interlocked.Decrement(ref inprocessJobCount);

                    base.formInstance.updateStats(StatType.Success, successfulJobCount);

                    base.formInstance.updateStats(StatType.Inprocess, inprocessJobCount);

                    base.formInstance.logText("Processing completed successfully for job: " + targetDataMessage.Name);

                    break;

                case StatType.Failure:

                    // Write log
                    await writeStatusLogFileAsync(jobStatusDetail, targetDataMessage, httpResponse);

                    // Update stats
                    Interlocked.Increment(ref failedJobCount);

                    Interlocked.Decrement(ref inprocessJobCount);

                    base.formInstance.updateStats(StatType.Failure, failedJobCount);

                    base.formInstance.updateStats(StatType.Inprocess, inprocessJobCount);

                    if (null == jobStatusDetail)
                    {
                        // Enqueue failed
                        base.formInstance.logText(string.Format("File: {0} -  moved to error location.", targetDataMessage.Name));
                    }
                    else
                    {
                        // Enqueue was successful, but failure processing data
                        base.formInstance.logText("Processing completed with errors for job: " + targetDataMessage.Name);
                    }
                    
                    break;
            }
            
        }

        private async Task postExistingDataMessagesAsync()
        {
            IDataSource<ClientDataMessage> inputLocationDS = DataSourceFactory.GetDataSourceForLocation(Settings.InputDir);
            
            foreach (ClientDataMessage dataMessage in inputLocationDS.FindAll())
            {
                base.formInstance.logText("File - " + dataMessage.Name + " - found in input location. Initializing data processing sequence...");

                await this.PostMessageAsync(dataMessage);                
            }
        }
    }

}
