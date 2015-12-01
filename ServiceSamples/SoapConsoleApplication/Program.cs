using AuthenticationUtility;
using SoapUtility.UserSessionServiceReference;
using System;
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

            var endpointAddress = new System.ServiceModel.EndpointAddress(serviceUriString);
            var binding = SoapUtility.SoapHelper.GetBinding();

            var client = new UserSessionServiceClient(binding, endpointAddress);
            var channel = client.InnerChannel;

            UserSessionInfo sessionInfo = null;

            using (OperationContextScope operationContextScope = new OperationContextScope(channel))
            {
                HttpRequestMessageProperty requestMessage = new HttpRequestMessageProperty();
                 requestMessage.Headers[OAuthHelper.OAuthHeader] = oauthHeader;
                 OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestMessage;
                 sessionInfo = ((UserSessionService)channel).GetUserSessionInfo(new GetUserSessionInfo()).result;
            }

            Console.WriteLine();
            Console.WriteLine("User ID: {0}", sessionInfo.UserId);
            Console.WriteLine("Is Admin: {0}", sessionInfo.IsSysAdmin);
            Console.ReadLine();
        }
    }
}

