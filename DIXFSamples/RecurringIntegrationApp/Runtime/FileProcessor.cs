using System;
using System.IO;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Timers;

namespace RecurringIntegrationApp
{
    /// <summary>
    /// Class that triggers the file processing logic.
    /// Also acts as the context class that selects one 
    /// of the data processing strategy implementations.
    /// </summary>
    class FileProcessor
    {        
        private FileSystemWatcher fileWatcher;
        
        private DIXFRecurringJobsProcessor form;

        /// <summary>
        /// 
        /// </summary>
        public DataFlowNetworkStrategy DataNetworkStrategy { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="formInstance">Form instance. Required for writing log messages (on UI thread)</param>
        public FileProcessor(DIXFRecurringJobsProcessor formInstance)
        {
            // Form context
            this.form = formInstance;

            // Set data processing class
            this.DataNetworkStrategy = new DefaultDataFlowNetwork(this.form);
        }

        /// <summary>
        /// Ctor that explicitly picks a data processing class
        /// </summary>
        /// <param name="formInstance">Form instance. Required for writing log messages (on UI thread)</param>
        /// <param name="dataFlowNetworkObj">Data processing class object</param>
        public FileProcessor(DIXFRecurringJobsProcessor formInstance, DataFlowNetworkStrategy dataFlowNetworkObj)
        {
            // Form context
            this.form = formInstance;

            this.DataNetworkStrategy = dataFlowNetworkObj;
        }

        /// <summary>
        /// Initializes the "data flow" network and sets up polling the
        /// input locations
        /// </summary>
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void Start()
        {
            // Call any initialization routine for the data flow network here.
            this.DataNetworkStrategy.Initialize();

            // Initiaze data source polling
            this.initDatasourceWatcher();            
        }

        /// <summary>
        /// Stop data processing
        /// </summary>
        public void Stop()
        {
            if (this.fileWatcher != null)
            {
                this.fileWatcher.Created -= sharedFolderFileCreatedHandler;

                this.fileWatcher.Dispose();

                this.fileWatcher = null;
            }

            // Tear down the data network
            this.DataNetworkStrategy.TearDown();     
        }

        /// <summary>
        /// Initialize the polling mechanism for the input 
        /// location
        /// </summary>
        private void initDatasourceWatcher()
        {
            // Initialize polling for Folder inputs
            
            // File watcher init
            this.fileWatcher = new FileSystemWatcher();

            // Create a new FileSystemWatcher and set its properties.
            this.fileWatcher.Path = Settings.InputDir;

            /* Watch for changes in LastAccess and LastWrite times, and
                the renaming of files or directories. */
            this.fileWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            // Add event handlers.
            this.fileWatcher.Created += new FileSystemEventHandler(sharedFolderFileCreatedHandler);

            // Begin watching.
            this.fileWatcher.EnableRaisingEvents = true;
        }

          /// <summary>
          /// File created handler for the folder location's file system watcher
          /// </summary>
          /// <param name="source">The file that triggered the event</param>
          /// <param name="e">The args</param>
        private void sharedFolderFileCreatedHandler(object source, FileSystemEventArgs e)
        {
            form.logText("File - " + Path.GetFileName(e.FullPath) + " - found in input location.");

            Task.FromResult(this.DataNetworkStrategy.PostMessageAsync(
                new ClientDataMessage()
                {
                    Name = Path.GetFileName(e.FullPath),
                    FullPath = e.FullPath,
                    MessageStatus = MessageStatus.Input
                }));    
        } 
        
    }
}
