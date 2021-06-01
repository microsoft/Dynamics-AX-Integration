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
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace OAuthXppConsoleApplication
{
    class Program
    {
        // In the AOT you will find UserSessionService in Service Groups and AifUserSessionService under Services.
        public static string sessionUrl = "/api/services/UserSessionService/AifUserSessionService/GetUserSessionInfo";
        
        // Set to true to authenticate as an AAD Web App, or false to autenticate as a Native AAD App.
        // See https://docs.microsoft.com/en-us/azure/active-directory/develop/v2-app-types
        private const bool UseAadWebAppAuthentication = false;

        static void Main(string[] args)
        {
            var uriPrefix = UseAadWebAppAuthentication
                ? ClientConfiguration.Default.ActiveDirectoryResource
                : ClientConfiguration.Default.UriString;
            string GetUserSessionOperationPath = string.Format("{0}{1}", uriPrefix.TrimEnd('/'), sessionUrl);
            
            var request = HttpWebRequest.Create(GetUserSessionOperationPath);     
            // If you call GetAuthenticationHeader with true you will the auth via AAD Web App, otherwise via Native AAD App
            request.Headers[OAuthHelper.OAuthHeader] = OAuthHelper.GetAuthenticationHeader(UseAadWebAppAuthentication);
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
