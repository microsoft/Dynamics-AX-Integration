using System;
using System.Configuration;

namespace RecurringIntegrationApp
{
    /// <summary>
    /// Settings manager to manage the client
    ///  settings for the app 
    /// </summary>
    class SettingManager
    {  
        /// <summary>
        /// Read a specific setting value for the given key
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Value of the setting for the given Key</returns>
        public static string ReadSetting(string key)
        {
            string result = null;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                result = appSettings[key];                            
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
            return result;
        }

    }
}
