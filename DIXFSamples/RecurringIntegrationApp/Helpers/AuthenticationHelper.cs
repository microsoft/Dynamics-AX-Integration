using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Configuration;
using System.Net.Http.Headers;

namespace RecurringIntegrationApp
{
    /// <summary>
    /// Helper class that enables generating the
    /// auth header for Enqueue and JobStatus requests
    /// </summary>
    class AuthenticationHelper
    {
        public static string authorizationHeader;
        public static AuthenticationResult AuthenticationResult { get; private set; }

        /// <summary>
        /// Property that gets the AuthorizationHeader
        /// </summary>
        public static string AuthorizationHeader
        {
            get
            {
                if (string.IsNullOrEmpty(authorizationHeader) || DateTime.UtcNow.AddSeconds(60) >= AuthenticationResult.ExpiresOn)
                {
                    UriBuilder uri = new UriBuilder(ConfigurationManager.AppSettings["Azure Auth Endpoint"]);
                    uri.Path = ConfigurationManager.AppSettings["Aad Tenant"];

                    AuthenticationContext authenticationContext = new AuthenticationContext(uri.ToString());

                    string aadClientAppSecret = "Client Secret from Azure App registration";

                    var credential = new ClientCredential("Application Id from Azure App registration", aadClientAppSecret);

                    AuthenticationResult authenticationResult = authenticationContext.AcquireTokenAsync("Dynamics 365 for operations URL", credential).Result;

                    return authenticationResult.CreateAuthorizationHeader();

                }

                return authorizationHeader;
            }
        }

        /// <summary>
        /// Get a valid authentication header
        /// </summary>
        /// <returns>AuthenticationHeaderValue object</returns>
        public static AuthenticationHeaderValue GetValidAuthenticationHeader()
        {
            return AuthenticationHelper.ParseAuthenticationHeader(AuthenticationHelper.AuthorizationHeader);
        }

        static AuthenticationHeaderValue ParseAuthenticationHeader(string authorizationHeader)
        {
            string[] split = authorizationHeader.Split(' ');
            string scheme = split[0];
            string parameter = split[1];
            return new AuthenticationHeaderValue(scheme, parameter);
        }
    }
}
