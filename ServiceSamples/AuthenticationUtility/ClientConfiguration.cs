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
            UserName = "qipengwu@pwax7.onmicrosoft.com",
            Password = "Pass1Word",
            ActiveDirectoryResource = "https://paulwuax7-ctp8aos.cloudax.dynamics.com",
            ActiveDirectoryTenant = "https://login.windows.net/pwax7.onmicrosoft.com",
            ActiveDirectoryClientAppId = "210435a7-7254-460d-b339-6a53d724cf76",
        };

        public string UriString { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ActiveDirectoryResource { get; set; }
        public String ActiveDirectoryTenant { get; set; }
        public String ActiveDirectoryClientAppId { get; set; }
    }
}
