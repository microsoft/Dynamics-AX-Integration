using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationHelper
{
    public class AuthorizationHelper
    {
        const string aadTenant = "https://login.windows.net/<your-tenant>";
        public const string aadResource = "https://<yourAOS>.cloudax.dynamics.com";
        const string aadClientAppId = "<client id>";        
        const string aadClientAppSecret = "<client secret>";

        /// <summary>
        /// Retrieves an authentication header from the service.
        /// </summary>
        /// <returns>The authentication header for the Web API call.</returns>
        public static string GetAuthenticationHeader()
        {
            AuthenticationContext authenticationContext = new AuthenticationContext(aadTenant);
            AuthenticationResult authenticationResult;
            
            var creadential = new ClientCredential(aadClientAppId, aadClientAppSecret);
            authenticationResult = authenticationContext.AcquireTokenAsync(aadResource, creadential).Result;
            
            // Create and get JWT token
            return authenticationResult.CreateAuthorizationHeader();
        }
    }
}

