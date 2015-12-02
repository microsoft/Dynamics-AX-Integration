using AuthenticationUtility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SoapUtility.UserSessionServiceReference;
using System;
using System.IO;
using System.Net;

namespace ServiceTests
{
    [TestClass]
    public class JsonXppAuthTests
    {
        public string GetUserSessionOperationPath = ClientConfiguration.Default.UriString + "api/services/UserSessionService/AifUserSessionService/GetUserSessionInfo";
        public string ApplyTimeZoneOperationPath = ClientConfiguration.Default.UriString + "api/services/UserSessionService/AifUserSessionService/ApplyTimeZone";


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

        [TestMethod]
        public void JsonSoapContractTest()
        {
            var request = HttpWebRequest.Create(ApplyTimeZoneOperationPath);
            request.Headers[OAuthHelper.OAuthHeader] = OAuthHelper.GetAuthenticationHeader();
            request.Method = "POST";

            DateTime inputDateTime = DateTime.Now;
            var requestContract = new ApplyTimeZone();
            requestContract.dateTime = inputDateTime;
            requestContract.timeZoneOffset = 3;
            var requestContractString = JsonConvert.SerializeObject(requestContract);

            using (var stream = request.GetRequestStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(requestContractString);
                }
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader streamReader = new StreamReader(responseStream))
                    {
                        string responseString = streamReader.ReadToEnd();
                        DateTime appliedTimeZone = JsonConvert.DeserializeObject<DateTime>(responseString);

                        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                        Assert.IsFalse(string.IsNullOrEmpty(responseString));
                        Console.WriteLine(responseString);
                        Assert.IsNotNull(appliedTimeZone);
                        Assert.AreNotEqual(appliedTimeZone.Hour, inputDateTime.Hour);
                    }
                }
            }
        }

        [TestMethod]
        public void JsonWeaklyTypedContractTest()
        {
            var request = HttpWebRequest.Create(ApplyTimeZoneOperationPath);
            request.Headers[OAuthHelper.OAuthHeader] = OAuthHelper.GetAuthenticationHeader();
            request.Method = "POST";

            DateTime inputDateTime = DateTime.Now;
            var requestContract = new
            {
                dateTime = inputDateTime,
                timeZoneOffset = 3
            };
            var requestContractString = JsonConvert.SerializeObject(requestContract);

            using (var stream = request.GetRequestStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(requestContractString);
                }
            }

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader streamReader = new StreamReader(responseStream))
                    {
                        string responseString = streamReader.ReadToEnd();
                        JToken jsonObject = JToken.Parse(responseString);
                        DateTime appliedTimeZone = jsonObject.Value<DateTime>();

                        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                        Assert.IsFalse(string.IsNullOrEmpty(responseString));
                        Console.WriteLine(responseString);
                        Assert.IsNotNull(appliedTimeZone);
                        Assert.AreNotEqual(appliedTimeZone.Hour, inputDateTime.Hour);
                    }
                }
            }
        }
    }
}
