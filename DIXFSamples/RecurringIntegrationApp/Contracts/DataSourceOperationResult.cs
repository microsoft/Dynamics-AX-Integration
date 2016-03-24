using System;

namespace RecurringIntegrationApp
{
    /// <summary>
    /// Contract for returning the result of a data
    /// operation
    /// </summary>
    class DataSourceOperationResult
    {
        /// <summary>
        /// Is the data operation successful
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// Exception data if any
        /// </summary>
        public Exception ExceptionData { get; set; }

        /// <summary>
        /// Any inforrmation message to return to the caller
        /// </summary>
        public string InfoMessage { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        public DataSourceOperationResult()
        {
            this.IsSuccessful = false;
            this.ExceptionData = null;
            this.InfoMessage = string.Empty;
        }
    }
}
