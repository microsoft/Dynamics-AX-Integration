using AuthenticationUtility;
using Microsoft.OData.Client;
using ODataUtility.Microsoft.Dynamics.DataEntities;
using System;
using System.Linq;
using System.Net;

namespace ODataConsoleApplication
{
    class Program
    {
        public static string ODataEntityPath = ClientConfiguration.Default.UriString + "data";

        static void Main(string[] args)
        {
            /* When making service requets to Sandbox or Prod AX environemnts it must be ensured that TLS version is 1.2
             * .NET 4.5 supports TLS 1.2 but it is not the default protocol. The below statement can set TLS version explicity.
             * Note that this statement may not work in .NET 4.0, 3.0 or below.
             * Also note that in .NET 4.6 and above TLS 1.2 is the default protocol.
             */

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // To test custom entities, regenerate "ODataClient.tt" file.
            // https://blogs.msdn.microsoft.com/odatateam/2014/03/11/tutorial-sample-how-to-use-odata-client-code-generator-to-generate-client-side-proxy-class/

            Uri oDataUri = new Uri(ODataEntityPath, UriKind.Absolute);
            var context = new Resources(oDataUri);

            context.SendingRequest2 += new EventHandler<SendingRequest2EventArgs>(delegate (object sender, SendingRequest2EventArgs e)
            {
                var authenticationHeader = OAuthHelper.GetAuthenticationHeader(useWebAppAuthentication: true);
                e.RequestMessage.SetHeader(OAuthHelper.OAuthHeader, authenticationHeader);
            });

            // Uncomment below to run specific examples

            // 1. Simple query examples

            QueryExamples.ReadLegalEntities(context);
            // QueryExamples.GetInlineQueryCount(context);
            // QueryExamples.GetTopRecords(context);
            // QueryExamples.FilterSyntax(context);
            // QueryExamples.FilterLinqSyntax(context);
            // QueryExamples.SortSyntax(context);
            // QueryExamples.FilterByCompany(context);
            // QueryExamples.ExpandNavigationalProperty(context);
            

            // 2. Simple CRUD examples

            // SimpleCRUDExamples.SimpleCRUD(context);

            // 2. Changeset examples

            // ODataChangesetsExample.CreateSalesOrderInSingleChangeset(context);
            // ODataChangesetsExample.CreateSalesOrderWithoutChangeset(context);

            Console.ReadLine();
        }

       
    }
}
