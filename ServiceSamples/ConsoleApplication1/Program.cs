using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;
using AuthenticationUtility;

namespace ConsoleApplication1
{
    class Program
    {
        private const string ContainerName = "TestStorage";
        private const string DemoFolder = @"C:\Demo";

        
        static void Main(string[] args)
        {
            StorageServices.CallContext callContext = new StorageServices.CallContext();
            StorageServices.StorageServicesClient client = new StorageServices.StorageServicesClient();
            int queueSize;
            string fileName;
            StorageServices.AXFileInfo fileInfo;
            var oauthHeader = OAuthHelper.GetAuthenticationHeader();


            using (OperationContextScope operationContextScope = new OperationContextScope(client.InnerChannel))
            {
                HttpRequestMessageProperty requestMessage = new HttpRequestMessageProperty();
                requestMessage.Headers[OAuthHelper.OAuthHeader] = oauthHeader;
                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestMessage;

                while (1 == 1)
                {
                    client.GetQueueSize(callContext, ContainerName, out queueSize);

                    if (queueSize > 0)
                    {
                        client.DequeueFile(callContext, ContainerName, out fileInfo);

                        fileName = string.Format(@"{0}\{1}", DemoFolder, fileInfo.FileName);

                        File.WriteAllText(fileName, fileInfo.FileContent);

                        client.AckAsync(callContext, ContainerName, fileInfo.Id);
                    }
                    else
                    {
                        Thread.Sleep(60000);
                    }                    
                }
            }
        }
    }
}
