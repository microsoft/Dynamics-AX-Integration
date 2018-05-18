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
        // In the AOT you will find UserSessionService in Service Groups and AifUserSessionService under Services.
        public static string sessionUrl = "/api/services/UserSessionService/AifUserSessionService/GetUserSessionInfo";
        
        static void Main(string[] args)
        {
            string GetUserSessionOperationPath = string.Format("{0}{1}", ClientConfiguration.Default.UriString.TrimEnd('/'), sessionUrl);
            
            var request = HttpWebRequest.Create(GetUserSessionOperationPath);     
            // If you call GetAuthenticationHeader with true you will the auth via AAD Web App, otherwise via Native AAD App
            request.Headers[OAuthHelper.OAuthHeader] = OAuthHelper.GetAuthenticationHeader();
            request.Method = "POST";
            request.ContentLength = 0;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader streamReader = new StreamReader(responseStream))
                    {
                        string responseString = streamReader.ReadToEnd();

                        Console.WriteLine(responseString);
                    }
                }
            }

            Console.ReadLine();
        }

    }
}
