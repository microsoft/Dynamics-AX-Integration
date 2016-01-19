using AuthenticationUtility;
using Microsoft.OData.Client;
using ODataUtility.Microsoft.Dynamics.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ODataConsoleApplication
{
    class Program
    {
        public static string ODataEntityPath = ClientConfiguration.Default.UriString + "data";

        static void Main(string[] args)
        {
            Uri oDataUri = new Uri(ODataEntityPath, UriKind.Absolute);
            var context = new Resources(oDataUri);

            context.SendingRequest2 += new EventHandler<SendingRequest2EventArgs>(delegate (object sender, SendingRequest2EventArgs e)
            {
                var authenticationHeader = OAuthHelper.GetAuthenticationHeader();
                e.RequestMessage.SetHeader(OAuthHelper.OAuthHeader, authenticationHeader);
            });

            Console.WriteLine();

            foreach (var fmcust in context.FleetCustomers.AsEnumerable())
            {
                Console.WriteLine("FMCustomer name: {0}", fmcust.FirstName);
            }
            FleetCustomer fleetCustomer = FleetCustomer.CreateFleetCustomer("123456", "Paul",11111,"Wu");

            try
            {
                context.AddToFleetCustomers(fleetCustomer);

                // Send updates to the data service.
                DataServiceResponse response = context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(
                    "An error occurred when saving changes.", ex);
            }
            
            foreach (var fmrental in context.FleetRentals.AsEnumerable())
            {
                Console.WriteLine("FMRental name: {0}", fmrental.CustomerFirstName);
            }
            Console.ReadLine();
        }
    }
}
