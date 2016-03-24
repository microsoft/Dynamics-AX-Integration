using System.IO;

namespace RecurringIntegrationApp
{
    /// <summary>
    /// Contract to abstract the input data file
    /// and its processing status (on the client)
    /// </summary>
    public class ClientDataMessage
    {
        /// <summary>
        /// Name.
        /// </summary>        
        public string Name { get; set; }

        /// <summary>
        /// Full path of the message
        /// </summary>
      
        public string FullPath { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public MessageStatus MessageStatus { get; set; }

        public ClientDataMessage() { }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="fullPath">Full path to the file</param>
        /// <param name="status">Current status of this data message</param>
        public ClientDataMessage(string fullPath, MessageStatus status)
        {            
            this.FullPath = fullPath;
            if (!string.IsNullOrEmpty(this.FullPath))
            {
                this.Name = Path.GetFileName(this.FullPath);
            }
            this.MessageStatus = status;
        }
    }


    /// <summary>
    /// Message status
    /// </summary>
    public enum MessageStatus
    {
        Input,

        InProcess,

        Enqueued,

        Failed,

        Succeeded
    }
}
