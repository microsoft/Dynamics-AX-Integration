using AuthenticationUtility;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace OAuthXppConsoleApplication
{
    class Program
    {
        public static string GetUserSessionOperationPath = ClientConfiguration.Default.UriString + "api/services/UserSessionService/AifUserSessionService/GetUserSessionInfo";
        public static string StorageServicePath = ClientConfiguration.Default.UriString + "api/services/storageServiceGroup/storageServices/downloadFile";

        static void Main(string[] args)
        {
            var request = HttpWebRequest.Create(StorageServicePath);
            request.Headers[OAuthHelper.OAuthHeader] = OAuthHelper.GetAuthenticationHeader();
            
            request.Method = "POST";

            string fileId = "{4FEA26D0-315F-471F-9D35-C25816562B50}";
            string jsonText = string.Format("{{\"fileId\":\"{0}\", \"ContainerName\":\"teststorage\"}}", fileId);

            byte[] buffer = Encoding.ASCII.GetBytes(jsonText);
            request.ContentLength = buffer.Length;

            using (Stream postData = request.GetRequestStream())
            {
                postData.Write(buffer, 0, buffer.Length);
            }
            
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
