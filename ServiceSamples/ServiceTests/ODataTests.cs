using AuthenticationUtility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Net;

namespace ServiceTests
{
    [TestClass]
    public class ODataTests
    {
        public string ODataEntityPath = ClientConfiguration.Default.UriString + "data";

        [TestMethod]
        public void ODataAuthTest()
        {
            var authenticationHeader = OAuthHelper.GetAuthenticationHeader();

            var request = HttpWebRequest.Create(ODataEntityPath);
            request.Headers[OAuthHelper.OAuthHeader] = authenticationHeader;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader streamReader = new StreamReader(responseStream))
                    {
                        string responseString = streamReader.ReadToEnd();

                        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                        Assert.IsFalse(string.IsNullOrEmpty(responseString));
                    }
                }
            }
        }
    }}
