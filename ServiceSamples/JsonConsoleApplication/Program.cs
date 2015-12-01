using AuthenticationUtility;
using System;
using System.IO;
using System.Net;

namespace OAuthXppConsoleApplication
{
    class Program
    {
        public static string GetUserSessionOperationPath = ClientConfiguration.Default.UriString + "api/services/UserSessionService/AifUserSessionService/GetUserSessionInfo";

        static void Main(string[] args)
        {
            var request = HttpWebRequest.Create(GetUserSessionOperationPath);
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
