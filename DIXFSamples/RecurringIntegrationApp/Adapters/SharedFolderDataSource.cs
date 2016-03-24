using System;
using System.Collections.Generic;
using System.IO;

namespace RecurringIntegrationApp
{
    /// <summary>
    /// Models a shared folder based datasource. 
    /// Other data sources can be modeled inheriting this
    /// interface and implementing the methods in a similar fashion
    /// </summary>
    class SharedFolderDataSource : IDataSource<ClientDataMessage>
    {
        /// <summary>
        /// Delete a data message
        /// </summary>
        /// <param name="dataMessage">ClientDataMessage object</param>
        /// <returns>Result of the operation as DataSourceOperationResult object</returns>
        public DataSourceOperationResult Delete(ClientDataMessage dataMessage)
        {
            DataSourceOperationResult result = new DataSourceOperationResult();

            try
            {
                if (File.Exists(dataMessage.FullPath))
                {
                    File.Delete(dataMessage.FullPath);
                }

                result.InfoMessage = string.Format("File {0} deleted successfully.", dataMessage.FullPath);
                result.IsSuccessful = true;
            }
            catch(Exception _ex)
            {
                result.ExceptionData = _ex;
            }

            return result;
        }

        /// <summary>
        /// Read data file
        /// </summary>
        /// <param name="dataMessage">ClientDataMessage object</param>
        /// <returns>Stream object</returns>
        public Stream Read(ClientDataMessage dataMessage)
        {
            if (File.Exists(dataMessage.FullPath))
            {
                return new FileStream(dataMessage.FullPath,
                            FileMode.Open,
                            FileAccess.Read,
                            FileShare.Read,
                            0x1000,
                            true);
            }
            return null;
        }

        /// <summary>
        /// Create data file
        /// </summary>
        /// <param name="sourceStream">Source stream</param>
        /// <param name="targetDataMessage">Target data message</param>
        public DataSourceOperationResult Create(Stream sourceStream, ClientDataMessage targetDataMessage)
        {

            DataSourceOperationResult result = new DataSourceOperationResult();
            try
            {                
                using (var fileStream = File.Create(targetDataMessage.FullPath))
                {   
                    sourceStream.Seek(0, SeekOrigin.Begin);
                    sourceStream.CopyTo(fileStream);

                    result.InfoMessage = string.Format("File {0} copied successfully.", targetDataMessage.FullPath);
                    result.IsSuccessful = true;
                }                
            }
            catch(Exception _ex)
            {
                result.InfoMessage = "Error moving file: " + targetDataMessage.FullPath + ". Error: " + _ex.Message;
                result.ExceptionData = _ex;                
            }

            return result;
        }

        /// <summary>
        /// Find all data files
        /// </summary>
        /// <returns>List of ClientDataMessage objects</returns>
        public IEnumerable<ClientDataMessage> FindAll()
        {
            foreach(string fileName in Directory.EnumerateFiles(Settings.InputDir))
            {
                var dataMessage = new ClientDataMessage()
                {
                    Name = Path.GetFileName(fileName),
                    FullPath = fileName,
                    MessageStatus = MessageStatus.Input
                };                

                yield return dataMessage;
            }
        }
    }
}
