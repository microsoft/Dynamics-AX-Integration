using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

namespace AX7.DIXFFileAgent.UIApp
{
    internal class AdvancedDataFlowNetwork
    {
        public ConcurrentQueue<string> InputQueue { get; set; }
        public ConcurrentQueue<string> InprocessQueue { get; set; }         

        public AdvancedDataFlowNetwork()
        {
            InputQueue = new ConcurrentQueue<string>();
            InprocessQueue = new ConcurrentQueue<string>();
        }
        public async Task ProcessInputQueue()
        {
            if (!InputQueue.IsEmpty)
            {
                string fileName;
                while (InputQueue.TryDequeue(out fileName))
                {                    
                    await Task.Run(() =>
                    {
                        string outputFileName = Path.Combine(Settings.InProcessDir, Path.GetFileName(fileName));
                        if (File.Exists(outputFileName))
                        {
                            File.Delete(outputFileName);
                        }
                        File.Move(fileName, outputFileName);
                        InprocessQueue.Enqueue(outputFileName);
                        Console.WriteLine("File - " + outputFileName + " - moved to inprocess location.");
                    });                    
                }
            }

            //await Console.Out.WriteLineAsync("Starting inprocess queue processing...");

            // Process uploads
            await ProcessInprocessQueue();

            //await Console.Out.WriteLineAsync("Completed processing inprocess queue...");
        }

        public async Task<bool> ProcessInprocessQueue()
        {
            if (!InprocessQueue.IsEmpty)
            {
                string fileName;
                while (InprocessQueue.TryDequeue(out fileName))
                {
                    byte[] buffer = new byte[1024];
                                        
                    using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read, 0x1000, true))
                    {
                        int bytesRead = 0;
                        long totalBytes = 0;
                        do
                        {
                            // Asynchronously read from the file stream.
                            bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length);
                            totalBytes += bytesRead;

                        } while (bytesRead > 0);
                    
                        fileStream.Seek(0, SeekOrigin.Begin);
                        
                        var httpClientHelper = new HttpClientHelper();
                        string correlationId = Settings.UseFileNameAsExternalCorrelationId ? Path.GetFileName(fileName) : null;
                        Uri enqueueUri = httpClientHelper.GetEnqueueUri();
                        Console.WriteLine("Sending request - " + enqueueUri + " - for file " + Path.GetFileName(fileName) + ". Request size - " + totalBytes + " bytes.");

                        var response = await httpClientHelper.SendPostRequestAsync(enqueueUri, fileStream, correlationId);
                        if (response.IsSuccessStatusCode)
                        {
                            var messageId = await response.Content.ReadAsStringAsync();
                            await Console.Out.WriteLineAsync("File - " + Path.GetFileName(fileName) + " - enqueued successfully.").ConfigureAwait(false);
                            await Console.Out.WriteLineAsync("Response - " + messageId.ToString()).ConfigureAwait(false);

                            await Task.Run(() =>
                            {
                                var successOutput = Path.Combine(Settings.SuccessDir, Path.GetFileName(fileName));
                                if (File.Exists(successOutput))
                                {
                                    File.Delete(successOutput);
                                }
                                File.Move(fileName, successOutput);
                                Console.Out.WriteLineAsync("File moved to success location.");
                            });
                        }
                        else
                        {
                            await Console.Out.WriteLineAsync("File - " + Path.GetFileName(fileName) + " - enqueued failed.").ConfigureAwait(false);
                            await Console.Out.WriteLineAsync("Response - Status: " + response.StatusCode + ", Reason: " + response.ReasonPhrase).ConfigureAwait(false);
                            await Task.Run(() =>
                            {
                                var failureOutput = Path.Combine(Settings.ErrorDir, Path.GetFileName(fileName));
                                if (File.Exists(failureOutput))
                                {
                                    File.Delete(failureOutput);
                                }
                                File.Move(fileName, failureOutput);
                                Console.Out.WriteLineAsync("File moved to failure location.").ConfigureAwait(false);
                            });
                        }
                    }                   
                }
            }

            return true;
        }
        
    }

   
}
