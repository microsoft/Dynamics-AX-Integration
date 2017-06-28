using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using ODataClient.Microsoft.Dynamics.DataEntities;
using System;
using System.IO;
using System.Threading;

namespace DataPackageHandler
{
    /// <summary>
    /// Showcases data package import using the DataManagementDefinitionGroups APIs. 
    /// </summary>
    class PackageImporter
    {
        public static void ImportPackage(Resources d365Client, string filePath)
        {
            // 1. Get writable Url from Dynamics 365 for Operations
            var azureWritableUrlOutput = d365Client.DataManagementDefinitionGroups.GetAzureWriteUrl(new Guid().ToString()).GetValue();
            var azureWriteUrl = JsonConvert.DeserializeObject<AzureUrlResult>(azureWritableUrlOutput);

            Console.WriteLine("Received azure writable url.");

            // 2. Upload the file to Dynamics 365 for Operations
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                var blob = new CloudBlockBlob(new Uri(azureWriteUrl.BlobUrl));
                blob.UploadFromStream(stream);                
            }

            Console.WriteLine("Uploaded file to blob storage");

            // 3. Import the data package 
            var executionId = d365Client.DataManagementDefinitionGroups.ImportFromPackage(
                packageUrl: azureWriteUrl.BlobUrl, 
                definitionGroupId: "Integration-data-project-01", 
                executionId: string.Empty,
                execute: true,
                overwrite: true,
                legalEntityId: "USMF").GetValue();

            Console.WriteLine("Package execution initiated.");

            // 4. Check for status
            DMFExecutionSummaryStatus? output;
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

                Console.WriteLine("Checking status");

                output = d365Client.DataManagementDefinitionGroups.GetExecutionSummaryStatus(executionId).GetValue();

                Console.WriteLine("Status of import is " + output.Value);

            }
            while (output == DMFExecutionSummaryStatus.NotRun || output == DMFExecutionSummaryStatus.Executing);
            
        }
    }

    public class AzureUrlResult
    {
        public string BlobId { get; set; }

        public string BlobUrl { get; set; }
    }
}
