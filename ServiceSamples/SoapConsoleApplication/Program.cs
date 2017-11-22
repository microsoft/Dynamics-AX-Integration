using AuthenticationUtility;
using SoapUtility.UserSessionServiceReference;
using System;
using System.Net;
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
            /* When making service requets to Sandbox or Prod AX environemnts it must be ensured that TLS version is 1.2
             * .NET 4.5 supports TLS 1.2 but it is not the default protocol. The below statement can set TLS version explicity.
             * Note that this statement may not work in .NET 4.0, 3.0 or below.
             * Also note that in .NET 4.6 and above TLS 1.2 is the default protocol.
             */

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

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

