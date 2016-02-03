using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationUtility
{
    public partial class ClientConfiguration
    {
        public static ClientConfiguration Default { get { return ClientConfiguration.OneBox; } }

        public static ClientConfiguration OneBox = new ClientConfiguration()
        {
            UriString = "https://paulwuax7-ctp8aos.cloudax.dynamics.com/",

            #region user credential
            UserName = "qipengwu@pwax7.onmicrosoft.com",
            Password = "Pass1Word",
            #endregion

            ActiveDirectoryResource = "https://paulwuax7-ctp8aos.cloudax.dynamics.com",
            ActiveDirectoryTenant = "https://login.windows.net/pwax7.onmicrosoft.com",
            ActiveDirectoryClientAppId = "f00ced34-2591-41f4-b22d-5005000f05be"            
        };

        public string UriString { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ActiveDirectoryResource { get; set; }
        public String ActiveDirectoryTenant { get; set; }
        public String ActiveDirectoryClientAppId { get; set; }
    }
}
