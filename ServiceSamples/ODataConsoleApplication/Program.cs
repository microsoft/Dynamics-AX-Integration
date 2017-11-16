using AuthenticationUtility;
using Microsoft.OData.Client;
using ODataUtility.Microsoft.Dynamics.DataEntities;
using System;
using System.Linq;

namespace ODataConsoleApplication
{
    class Program
    {
        public static string ODataEntityPath = ClientConfiguration.Default.UriString + "data";

        static void Main(string[] args)
        {
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
