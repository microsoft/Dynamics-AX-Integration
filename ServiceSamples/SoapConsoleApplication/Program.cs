using AuthenticationUtility;
using SoapUtility.UserSessionServiceReference;
using SoapUtility.StorageServices;
using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace SoapConsoleApplication
{
    class Program
    {
        public const string UserSessionServiceName = "UserSessionService";

        [STAThread]
        static void Main(string[] args)
        {
            var aosUriString = ClientConfiguration.Default.UriString;

            var oauthHeader = OAuthHelper.GetAuthenticationHeader();
            var serviceUriString = SoapUtility.SoapHelper.GetSoapServiceUriString(UserSessionServiceName, aosUriString);
            var storageServiceUriString = SoapUtility.SoapHelper.GetSoapServiceUriString("StorageServiceGroup", aosUriString);

            var endpointAddress = new System.ServiceModel.EndpointAddress(serviceUriString);
            var storageEndpointAddress = new System.ServiceModel.EndpointAddress(storageServiceUriString);
            var binding = SoapUtility.SoapHelper.GetBinding();

            var client = new UserSessionServiceClient(binding, endpointAddress);
            var storageClient = new StorageServicesClient(binding, storageEndpointAddress);
            var channel = client.InnerChannel;
            var storageClientchannel = storageClient.InnerChannel;

            UserSessionInfo sessionInfo = null;
            Stream fileStream;


            using (OperationContextScope operationContextScope = new OperationContextScope(storageClientchannel))
            {
                HttpRequestMessageProperty requestMessage = new HttpRequestMessageProperty();
                requestMessage.Headers[OAuthHelper.OAuthHeader] = oauthHeader;
                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestMessage;
                fileStream = ((StorageServices)storageClientchannel).DownloadFile(new DownloadFile()).result;
            }

            using (OperationContextScope operationContextScope = new OperationContextScope(channel))
            {
                HttpRequestMessageProperty requestMessage = new HttpRequestMessageProperty();
                 requestMessage.Headers[OAuthHelper.OAuthHeader] = oauthHeader;
                 OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestMessage;
                 sessionInfo = ((UserSessionService)channel).GetUserSessionInfo(new GetUserSessionInfo()).result;
            }

            fileStream.Seek(0, System.IO.SeekOrigin.Begin);


            StreamReader reader = new StreamReader(fileStream);
            string text = reader.ReadToEnd();

            Console.WriteLine();
            Console.WriteLine("User ID: {0}", sessionInfo.UserId);
            Console.WriteLine("Is Admin: {0}", sessionInfo.IsSysAdmin);
            Console.ReadLine();
        }
    }
}

