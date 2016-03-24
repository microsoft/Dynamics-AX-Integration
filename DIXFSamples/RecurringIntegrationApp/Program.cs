using System;
using System.Windows.Forms;

namespace RecurringIntegrationApp
{
    /// <summary>
    /// Entry poin to the application
    /// </summary>
    static class Program
    {
        public const string EnqueueRelativePath = "api/connector/enqueue/";

        public const string JobStatusRelativePath = "api/connector/jobstatus/";

        public const string ExternalCorrelationHeader = "x-ms-dyn-externalidentifier";
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool settingsInitialized = Settings.InitSettings();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SplashScreen());            
        }      
    }
}
