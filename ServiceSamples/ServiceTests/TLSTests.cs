using AuthenticationUtility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SoapUtility.UserSessionServiceReference;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace ServiceTests
{
    [TestClass]
    public class TLSTests
    {
        public static string sessionUrl = "/api/services/UserSessionService/AifUserSessionService/GetUserSessionInfo";

        [TestMethod]
        public void TLSAuthTest()
        {
            string GetUserSessionOperationPath = string.Format("{0}{1}", ClientConfiguration.Default.UriString.TrimEnd('/'), sessionUrl);
            
            // Creates an HttpWebRequest for user session URL
            HttpWebRequest aadRequest = (HttpWebRequest)WebRequest.Create(GetUserSessionOperationPath);

            // Change TLS version of HTTP request if the TLS version value is defined in ClientConfiguration
            if (!string.IsNullOrWhiteSpace(ClientConfiguration.OneBox.TLSVersion))
            {
                aadRequest.ProtocolVersion = Version.Parse(ClientConfiguration.OneBox.TLSVersion);
            }

            string tlsRequestVersion = aadRequest.ProtocolVersion.ToString();
            Console.WriteLine("The TLS protocol version for the HTTP request is {0}.", tlsRequestVersion);

            aadRequest.Headers[OAuthHelper.OAuthHeader] = OAuthHelper.GetAuthenticationHeader();
            aadRequest.Method = "POST";
            aadRequest.ContentLength = 0;

            // Get HttpWebResponse for the response
            var aadResponse = (HttpWebResponse)aadRequest.GetResponse();

            string tlsResponseVersion = aadResponse.ProtocolVersion.ToString();            
            Console.WriteLine("The TLS protocol version of the server response is {0}.", tlsResponseVersion);

            Assert.IsTrue(aadResponse.StatusCode == HttpStatusCode.OK);

            // Get response string
            using (Stream responseStream = aadResponse.GetResponseStream())
            {
                using (StreamReader streamReader = new StreamReader(responseStream))
                {
                    string responseString = streamReader.ReadToEnd();

                    Assert.IsFalse(string.IsNullOrEmpty(responseString));

                    Console.WriteLine(string.Format("Request sent using version {0}. Successfully received response with version {1}.", tlsRequestVersion, tlsResponseVersion));

                    Console.WriteLine(string.Format("\nResponse string: {0}.", responseString));
                }
            }

            // Releases the resources of the response.
            aadResponse.Close();
        }
    }
}
