using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace SoapUtility
{
    public class SoapHelper
    {
        public static string GetSoapServiceUriString(string serviceName, string aosUriString)
        {
            var soapServiceUriStringTemplate = "{0}/soap/services/{1}";
            var soapServiceUriString = string.Format(soapServiceUriStringTemplate, aosUriString.TrimEnd('/'), serviceName);
            return soapServiceUriString;
        }

        public static Binding GetBinding()
        {
            var binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);

            return binding;
        }
    }
}
