using System;
using System.Threading.Tasks;

namespace RecurringIntegrationApp
{
    /// <summary>
    /// Abstraction for picking a strategy to 
    /// process the data files. 
    /// Implement this strategy class and plugin via the FileProcessor 
    /// to automate file processing  with a different state machine 
    /// or with a different implementation logic.
    /// </summary>
    public abstract class DataFlowNetworkStrategy
    {
        /// <summary>
        /// Form instance (required for logging)
        /// </summary>
        public DIXFRecurringJobsProcessor formInstance { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="view">Form object</param>
        public DataFlowNetworkStrategy(DIXFRecurringJobsProcessor view)
        {
            this.formInstance = view;
        }

        /// <summary>
        /// Initialize the data network
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// Post a message to start processing
        /// </summary>
        /// <param name="dataMessage">ClientDataMessage instance</param>
        /// <returns></returns>
        #pragma warning disable 1998
        public virtual async Task PostMessageAsync(ClientDataMessage dataMessage)
        {
            throw new NotImplementedException();
        }
        #pragma warning restore 1998

        /// <summary>
        /// Tear down/cleanup the data flow network
        /// </summary>
        public abstract void TearDown();

    }
}
