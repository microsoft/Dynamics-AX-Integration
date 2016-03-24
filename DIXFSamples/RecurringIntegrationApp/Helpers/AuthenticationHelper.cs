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

                    UserCredential userCred = new UserCredential(ConfigurationManager.AppSettings["User"], ConfigurationManager.AppSettings["Password"]);

                    AuthenticationResult = authenticationContext.AcquireToken(ConfigurationManager.AppSettings["Rainier Uri"], ConfigurationManager.AppSettings["Azure Client Id"], userCred);

                    authorizationHeader = AuthenticationResult.CreateAuthorizationHeader();
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
