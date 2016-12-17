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
            UriString = "https://typeUriHere/",
            UserName = "typeUserNameHere",
            Password = "PASSWORD",
            ActiveDirectoryResource = "https://typeUriHere",
            ActiveDirectoryTenant = "https://typeADTenantUriHere",
            ActiveDirectoryClientAppId = "typeGuidHere",
        };

        public string UriString { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ActiveDirectoryResource { get; set; }
        public String ActiveDirectoryTenant { get; set; }
        public String ActiveDirectoryClientAppId { get; set; }
    }
}
