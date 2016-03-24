namespace RecurringIntegrationApp
{
    class DataSourceFactory
    {
        /// <summary>
        /// Factory method to return a datasource based on a given
        /// data message
        /// </summary>
        /// <param name="dataMessage">ClientDataMessage object</param>
        /// <returns>IDataSource object for the specific message</returns>
        public static IDataSource<ClientDataMessage> GetDataSourceForMessage(ClientDataMessage dataMessage)
        {            
            return new SharedFolderDataSource();
        }

        /// <summary>
        /// Factory method to return a datasource based on a given location
        /// </summary>
        /// <param name="dataMessage">ClientDataMessage object</param>
        /// <returns>IDataSource object for the specific location</returns>
        public static IDataSource<ClientDataMessage> GetDataSourceForLocation(string location)
        {           
            return new SharedFolderDataSource();
        }
    }
}
