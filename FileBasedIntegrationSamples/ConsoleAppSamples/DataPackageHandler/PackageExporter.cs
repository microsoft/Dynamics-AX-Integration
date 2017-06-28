using Microsoft.WindowsAzure.Storage.Blob;
using ODataClient.Microsoft.Dynamics.DataEntities;
using System;
using System.IO;
using System.Threading;

namespace DataPackageHandler
{
    public class PackageExporter
    {
        public static void ExportPackage(Resources d365Client, string filePath)
        {
            // Setup Step 
            // - Create an export project within Dynamics called Integration_Outbound_Flow_01 in company USMF before you run the following code
            // - It can of any data format (csv, xml etc.) and can include any number of data entities


            // 1. Initiate export of a data project to create a data package within Dynamics 365 for Operations
            Console.WriteLine("Initiating export of a data project...");
            var executionId = d365Client.DataManagementDefinitionGroups.ExportToPackage("Integration_Outbound_Flow_01", Guid.NewGuid().ToString(), string.Empty, false, "USMF").GetValue();
            Console.WriteLine("Initiating export of a data project...Complete");

            // 2. Check if execution is completed
            DMFExecutionSummaryStatus? output = null;
            int maxLoop = 100;

            do
            {
                Console.WriteLine("Waiting for package to execution to complete");

                Thread.Sleep(5000);
                maxLoop--;

                if (maxLoop <= 0)
                {
                    break;
                }

                Console.WriteLine("Checking status...");

                output = d365Client.DataManagementDefinitionGroups.GetExecutionSummaryStatus(executionId).GetValue();

                Console.WriteLine("Status of export is " + output.Value);

            }
            while (output == DMFExecutionSummaryStatus.NotRun || output == DMFExecutionSummaryStatus.Executing);

            if (output.HasValue 
                && output.Value != DMFExecutionSummaryStatus.Succeeded 
                && output.Value != DMFExecutionSummaryStatus.PartiallySucceeded)
            {
                throw new Exception("Operation Failed");
            }

            // 3. Get downloable Url to download the package           
            var downloadUrl = d365Client.DataManagementDefinitionGroups.GetExportedPackageUrl(executionId).GetValue();


            // 4. Download the file from Url to a local folder
            Console.WriteLine("Downloading the file ...");
            var blob = new CloudBlockBlob(new Uri(downloadUrl));
            blob.DownloadToFile(Path.Combine(filePath, Guid.NewGuid().ToString() + ".zip"), System.IO.FileMode.Create);
            Console.WriteLine("Downloading the file ...Complete");

        }
    }
}
