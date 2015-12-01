using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel;
using System.ServiceModel.Channels;
using AuthenticationUtility;
using SoapUtility;
using SoapUtility.UserSessionServiceReference;

namespace ServiceTests
{
    [TestClass]
    public class SoapTests
    {
        public const string UserSessionServiceName = "UserSessionService";

        [TestMethod]
        public void BasicSoapAuthTest()
        {
            var authenticationHeader = OAuthHelper.GetAuthenticationHeader();
            var serviceUriString = SoapHelper.GetSoapServiceUriString(UserSessionServiceName, ClientConfiguration.Default.UriString);

            var endpointAddress = new System.ServiceModel.EndpointAddress(serviceUriString);
            var binding = SoapHelper.GetBinding();

            var client = new UserSessionServiceClient(binding, endpointAddress);

            var channel = client.InnerChannel;
            UserSessionInfo sessionInfo = null;
            using (OperationContextScope operationContextScope = new OperationContextScope(channel))
            {
                HttpRequestMessageProperty requestMessage = new HttpRequestMessageProperty();
                requestMessage.Headers[OAuthHelper.OAuthHeader] = authenticationHeader;
                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestMessage;
                sessionInfo = ((UserSessionService)channel).GetUserSessionInfo(new GetUserSessionInfo()).result;
            }

            Assert.IsNotNull(sessionInfo);
            Assert.IsNotNull(sessionInfo.UserId);
        }
    }}
