using System.Collections.Generic;
using System.IO;

namespace RecurringIntegrationApp
{
    /// <summary>
    /// Interface that represents a data source.
    /// Any data source (such as a shared folder or FTP source)
    /// must implement this interface
    /// </summary>
    /// <typeparam name="T">Type of element that IDataSource implements interact with</typeparam>
    interface IDataSource<T> where T : ClientDataMessage
    {
        /// <summary>
        /// Create a data message with the target data message
        /// properties using the source stream
        /// </summary>
        /// <param name="sourceStream">Source stream pointing to input data</param>
        /// <param name="targetDataMessage">The target (this datasource's) destination message</param>
        /// <returns>DataSourceOperationResult object</returns>
        DataSourceOperationResult Create(Stream sourceStream, T targetDataMessage);

        /// <summary>
        /// Open a read stream for the specific message
        /// </summary>
        /// <param name="message">Data message in the current datasource's location to be read</param>
        /// <returns>Data stream</returns>
        Stream Read(T message);

        /// <summary>
        /// Delete the specific data message
        /// </summary>
        /// <param name="dataMessage">Client data message to be deleted</param>
        /// <returns>DataSourceOperationResult object</returns>
        DataSourceOperationResult Delete(T dataMessage);

        /// <summary>
        /// Find all the current messages in the data source's 
        /// location (used only if the current datasource is used as input location 
        /// for messages)
        /// </summary>
        /// <returns>List of all messages</returns>
        IEnumerable<ClientDataMessage> FindAll();
        
    }

}
