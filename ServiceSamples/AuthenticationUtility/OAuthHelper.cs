using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationUtility
{
    public class OAuthHelper
    {
        /// <summary>
        /// The header to use for OAuth.
        /// </summary>
        public const string OAuthHeader = "Authorization";

        /// <summary>
        /// Retrieves an authentication header from the service.
        /// </summary>
        /// <returns>The authentication header for the Web API call.</returns>
        public static string GetAuthenticationHeader()
        {
            string aadTenant = ClientConfiguration.Default.ActiveDirectoryTenant;
            string aadClientAppId = ClientConfiguration.Default.ActiveDirectoryClientAppId;
            string aadResource = ClientConfiguration.Default.ActiveDirectoryResource;

            AuthenticationContext authenticationContext = new AuthenticationContext(aadTenant);

            // OAuth through username and password.
            string username = ClientConfiguration.Default.UserName;
            string password = ClientConfiguration.Default.Password;

            // Get token object
            AuthenticationResult authenticationResult = authenticationContext.AcquireToken(aadResource, aadClientAppId, new UserCredential(username, password));

            // Create and get JWT token
            return authenticationResult.CreateAuthorizationHeader();
        }


        public static string GetAuthenticationHeader(string userAssertionStr)
        {
            string clientId = "91837a0e-b419-472b-b7f4-f4a4c84f0bf1";
            string clientSecret = "fk3KOqaXYi1v3QdJZzYfRsO+BO7rZFEh6kivNYb8yPk=";
            string aadTenant = ClientConfiguration.Default.ActiveDirectoryTenant;
            string aadClientAppId = ClientConfiguration.Default.ActiveDirectoryClientAppId;
            string aadResource = ClientConfiguration.Default.ActiveDirectoryResource;

            AuthenticationContext authenticationContext = new AuthenticationContext(aadTenant);

            // OAuth through username and password.
            string username = ClientConfiguration.Default.UserName;
            string password = ClientConfiguration.Default.Password;

            // Get token object
            ClientCredential clientCredential = new ClientCredential(clientId, clientSecret);
            UserAssertion userAssertion = new UserAssertion(userAssertionStr);
            AuthenticationResult authenticationResult = authenticationContext.AcquireToken(aadResource, clientCredential, userAssertion);

            // Create and get JWT token
            return authenticationResult.CreateAuthorizationHeader();
        }

        public static string GetAuthenticationHeaderWithKey()
        {
            string clientId = "91837a0e-b419-472b-b7f4-f4a4c84f0bf1";
            string clientSecret = "fk3KOqaXYi1v3QdJZzYfRsO+BO7rZFEh6kivNYb8yPk=";
            string aadTenant = ClientConfiguration.Default.ActiveDirectoryTenant;
            string aadClientAppId = ClientConfiguration.Default.ActiveDirectoryClientAppId;
            string aadResource = ClientConfiguration.Default.ActiveDirectoryResource;

            AuthenticationContext authenticationContext = new AuthenticationContext(aadTenant);

            // OAuth through username and password.
            string username = ClientConfiguration.Default.UserName;
            string password = ClientConfiguration.Default.Password;

            // Get token object
            ClientCredential clientCredential = new ClientCredential(clientId, clientSecret);
            AuthenticationResult authenticationResult = authenticationContext.AcquireToken(aadResource, clientCredential);

            // Create and get JWT token
            return authenticationResult.CreateAuthorizationHeader();
        }
    }
}
