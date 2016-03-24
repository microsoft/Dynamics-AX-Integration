using System;
using System.IO;

namespace RecurringIntegrationApp
{
    /// <summary>
    /// Serialize/deeserialize current app settings
    /// </summary>
    public class Settings
    {
        #region Members

        public static string InputDir { get; set; }

        public static string InProcessDir { get; set; }

        public static string ErrorDir { get; set; }

        public static string SuccessDir { get; set; }

        public static int StatusPollingInterval { get; set; }

        public static string RainierUri { get; set; }

        public static string AzureAuthEndpoint { get; set; }

        public static string RainierUserName { get; set; }

        public static string RainierUserPassword { get; set; }

        public static string ClientId { get; set; }

        public static string AadTenant { get; set; }        

        public static Guid RecurringJobId { get; set; }

        public static string EntityName { get; set; }

        public static bool IsDataPackage { get; set; }

        public static bool UseCompany { get; set; }

        public static string Company { get; set; }

        #endregion

        /// <summary>
        /// Initialize settings based on app.config
        /// </summary>
        /// <returns></returns>
        public static bool InitSettings()
        {
            bool settingsValid = true;            

            InputDir = SettingManager.ReadSetting("Input Directory");
            if (!Directory.Exists(InputDir))
            {
                Console.WriteLine("Input directory does not exist");
                settingsValid = false;
            }

            InProcessDir = SettingManager.ReadSetting("InProcess Directory");
            if (!Directory.Exists(InProcessDir))
            {
                Console.WriteLine("InProcess directory does not exist");
                settingsValid = false;
            }

            ErrorDir = SettingManager.ReadSetting("Error Directory");
            if (!Directory.Exists(ErrorDir))
            {
                Console.WriteLine("Error directory does not exist");
                settingsValid = false;
            }

            SuccessDir = SettingManager.ReadSetting("Success Directory");
            if (!Directory.Exists(SuccessDir))
            {
                Console.WriteLine("Success directory does not exist");
                settingsValid = false;
            }

            string statusPollingIntervalStr = SettingManager.ReadSetting("Status Polling Interval");
            int statusPollerInterval;
            if(!Int32.TryParse(statusPollingIntervalStr, out statusPollerInterval))
            {
                statusPollerInterval = 60000;
            }
            StatusPollingInterval = statusPollerInterval;

            RainierUri = SettingManager.ReadSetting("Rainier Uri");

            AzureAuthEndpoint = SettingManager.ReadSetting("Azure Auth Endpoint");

            AadTenant = SettingManager.ReadSetting("Aad Tenant");

            RainierUserName = SettingManager.ReadSetting("User");

            RainierUserPassword = SettingManager.ReadSetting("Password");

            ClientId = SettingManager.ReadSetting("Azure Client Id");

            string RecurringJobIdStr = SettingManager.ReadSetting("Recurring Job Id");
            Guid _id;
            if (!Guid.TryParse(RecurringJobIdStr, out _id) || Guid.Empty == _id)
            {
                Console.WriteLine("Recurring Job Id must be set");
                settingsValid = false;
            }
            else
            {
                Settings.RecurringJobId = _id;
            }

            EntityName = SettingManager.ReadSetting("Entity Name");
            if (string.IsNullOrEmpty(EntityName))
            {
                Console.WriteLine("Entity Name must be set");
                settingsValid = false;
            }

            IsDataPackage = Convert.ToBoolean(SettingManager.ReadSetting("Is Data Package"));            

            Company = SettingManager.ReadSetting("Company");
            if (string.IsNullOrEmpty(Settings.Company))
            {
                Console.WriteLine("Company is invalid");
                settingsValid = false;
            }
          
            if (settingsValid)
            {
                Console.WriteLine("******************************************************************");
                Console.WriteLine(string.Format("Running recurring job with the following parameters: " +
                    "\r\nTenant: {0}\r\nActivity: {1}\r\nEntity: {2}\r\nCompany: {3}\r\nIsDataPackage: {4}",
                    Settings.RainierUri, Settings.RecurringJobId, Settings.EntityName, Settings.Company, 
                    Settings.IsDataPackage.ToString()));
                Console.WriteLine("******************************************************************");

            }
            return settingsValid;

        }
    }
}
