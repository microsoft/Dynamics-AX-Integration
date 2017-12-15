using AuthenticationUtility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Web;
using System.Security.Authentication;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace OAuthXppConsoleApplication
{
    class Program
    {
        public static string sessionUrl = "/api/services/UserSessionService/AifUserSessionService/GetUserSessionInfo";

        static void Main(string[] args)
        {
            string GetUserSessionOperationPath = string.Format("{0}{1}", ClientConfiguration.Default.UriString.TrimEnd('/'), sessionUrl);

            try
            {
                // Creates an HttpWebRequest for user session URL.
                HttpWebRequest aadRequest = (HttpWebRequest) WebRequest.Create(GetUserSessionOperationPath);

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
                var aadResponse = (HttpWebResponse) aadRequest.GetResponse();

                string tlsResponseVersion = aadResponse.ProtocolVersion.ToString();
                Console.WriteLine("The TLS protocol version of the server response is {0}.", tlsResponseVersion);

                if (aadResponse.StatusCode != HttpStatusCode.OK)
                {
                    Console.WriteLine("Could not get response from the server.");
                    return;
                }

                // Get response string
                using (Stream responseStream = aadResponse.GetResponseStream())
                {
                    using (StreamReader streamReader = new StreamReader(responseStream))
                    {
                        string responseString = streamReader.ReadToEnd();
                        
                        Console.WriteLine(string.Format("\nSuccessfully received response.\nResponse string: {0}.", responseString));
                    }
                }

                // Releases the resources of the response.
                aadResponse.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed with the exception: {0} and stack trace: {1}.", ex.ToString(), ex.StackTrace);
                throw new Exception(ex.Message);
            }

            Console.ReadLine();
        }

    }
}
