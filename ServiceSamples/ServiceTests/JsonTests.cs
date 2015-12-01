using AuthenticationUtility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Net;

namespace ServiceTests
{
    [TestClass]
    public class JsonXppAuthTests
    {
        public string GetUserSessionOperationPath = ClientConfiguration.Default.UriString + "api/services/UserSessionService/AifUserSessionService/GetUserSessionInfo";


        [TestMethod]
        public void JsonAuthTest()
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

                        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                        Assert.IsFalse(string.IsNullOrEmpty(responseString));
                        Console.WriteLine(responseString);
                    }
                }
            }
        }
    }}
