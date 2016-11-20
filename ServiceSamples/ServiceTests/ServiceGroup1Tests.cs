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
        public class ServiceGroup1Tests
    {

        public static string ServiceGroup1ServicePath = ClientConfiguration.Default.UriString + "api/services/ServiceGroup1/Service1";
        public static string ServiceGroup1EchoStringOperationPath = ServiceGroup1ServicePath + "/EchoString";

        public void EnsureServiceGroup1IsPresent()
        {
            var getRequest = HttpWebRequest.Create(ServiceGroup1ServicePath);
            getRequest.Headers[OAuthHelper.OAuthHeader] = OAuthHelper.GetAuthenticationHeader();
            getRequest.Method = "GET";
            try
            {
                using (var getResponse = (HttpWebResponse)getRequest.GetResponse())
                {
                    Assert.AreEqual(getResponse.StatusCode, HttpStatusCode.OK);
                }
            }
            catch
            {
                Assert.Inconclusive("ServiceGroup1 and its operations are not currently present under /api/services/ServiceGroup1/Service1/<operation_name>. You can enable the test service group by importing the included project (ServiceContractProject.axpp) and compiling the artifacts.");
            }
        }

        [TestMethod]
        public void JsonWeaklyTypedServiceGroup1EchoStringTest()
        {
            EnsureServiceGroup1IsPresent();

            var request = HttpWebRequest.Create(ServiceGroup1EchoStringOperationPath);
            request.Headers[OAuthHelper.OAuthHeader] = OAuthHelper.GetAuthenticationHeader();
            request.Method = "POST";

            var inputString = "SomeString";
            var requestContract = new
            {
                input = inputString
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
                        string returnedString = jsonObject.Value<string>();

                        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                        Assert.IsFalse(string.IsNullOrEmpty(responseString));
                        Console.WriteLine(responseString);
                        Assert.IsNotNull(returnedString);
                        Assert.AreEqual(inputString, returnedString);
                    }
                }
            }
        }

        [TestMethod]
        public void JsonServiceGroup1EchoStringTest()
        {
            EnsureServiceGroup1IsPresent();

            var request = HttpWebRequest.Create(ServiceGroup1EchoStringOperationPath);
            request.Headers[OAuthHelper.OAuthHeader] = OAuthHelper.GetAuthenticationHeader();
            request.Method = "POST";

            var inputString = "SomeString";
            var requestContract = new EchoStringRequest { input = inputString };
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
                        string returnedString = jsonObject.Value<string>();

                        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                        Assert.IsFalse(string.IsNullOrEmpty(responseString));
                        Console.WriteLine(responseString);
                        Assert.IsNotNull(returnedString);
                        Assert.AreEqual(inputString, returnedString);
                    }
                }
            }
        }

        public class EchoStringRequest {  public string input { get; set; } }


        public static string ServiceGroup1EchoContractListOperationPath = ServiceGroup1ServicePath + "/EchoContractList";

        [TestMethod]
        public void JsonServiceGroup1EchoContractListTest()
        {
            EnsureServiceGroup1IsPresent();

            var request = HttpWebRequest.Create(ServiceGroup1EchoContractListOperationPath);
            request.Headers[OAuthHelper.OAuthHeader] = OAuthHelper.GetAuthenticationHeader();
            request.Method = "POST";

            var inputString = "SomeString";
            var inputContractList = new Contract1[] {
                    new Contract1 { parmStringMember = inputString }
                    , new Contract1 { parmStringMember = inputString + "*" }
                };
            var requestContract = new ContractListRequest {
                input = inputContractList
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
                        Contract1[] returnedContractList = JsonConvert.DeserializeObject<Contract1[]>(responseString);

                        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                        Assert.IsFalse(string.IsNullOrEmpty(responseString));
                        Console.WriteLine(responseString);
                        Assert.IsNotNull(returnedContractList);
                        Assert.AreEqual(returnedContractList.Length, inputContractList.Length);
                        Assert.AreEqual(returnedContractList[0].parmStringMember, inputContractList[0].parmStringMember);
                        Assert.AreEqual(returnedContractList[1].parmStringMember, inputContractList[1].parmStringMember);
                    }
                }
            }
        }

        public class Contract1 { public string parmStringMember { get; set; } }
        public class ContractListRequest {  public Contract1[] input { get; set; } }


        public static string ServiceGroup1EchoComplexContractOperationPath = ServiceGroup1ServicePath + "/EchoComplexContract";

        [TestMethod]
        public void JsonServiceGroup1EchoComplexContractTest()
        {
            EnsureServiceGroup1IsPresent();

            var request = HttpWebRequest.Create(ServiceGroup1EchoComplexContractOperationPath);
            request.Headers[OAuthHelper.OAuthHeader] = OAuthHelper.GetAuthenticationHeader();
            request.Method = "POST";

            var inputString = "SomeString";
            var inputContractList = new Contract1[] {
                    new Contract1 { parmStringMember = inputString }
                    , new Contract1 { parmStringMember = inputString + "*" }
                };
            var requestContract = new ComplexContractRequest
            {
                input = new ComplexContract { parmContractList = inputContractList  }
            };
            //var requestContractString = JsonConvert.SerializeObject(requestContract);
            var requestContractString = JsonConvert.SerializeObject(requestContract, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });

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
                        ComplexContract returnedComplex = JsonConvert.DeserializeObject<ComplexContract>(responseString);

                        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
                        Assert.IsFalse(string.IsNullOrEmpty(responseString));
                        Console.WriteLine(responseString);
                        Assert.IsNotNull(returnedComplex);
                        var returnedContractList = returnedComplex.parmContractList;
                        Assert.IsNotNull(returnedContractList);
                        Assert.AreEqual(returnedContractList.Length, inputContractList.Length);
                        Assert.AreEqual(returnedContractList[0].parmStringMember, inputContractList[0].parmStringMember);
                        Assert.AreEqual(returnedContractList[1].parmStringMember, inputContractList[1].parmStringMember);
                    }
                }
            }
        }

        public class ComplexContract { public Contract1[] parmContractList { get; set; } }
        public class ComplexContractRequest { public ComplexContract input { get; set; } }
    }
}
