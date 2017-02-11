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
            Uri oDataUri = new Uri(ODataEntityPath, UriKind.Absolute);
            var context = new Resources(oDataUri);

            context.SendingRequest2 += new EventHandler<SendingRequest2EventArgs>(delegate (object sender, SendingRequest2EventArgs e)
            {
                var authenticationHeader = OAuthHelper.GetAuthenticationHeader();
                e.RequestMessage.SetHeader(OAuthHelper.OAuthHeader, authenticationHeader);
            });

            ReadLegalEntities(context);
            CreateSalesOrderSuccess(context);
            CreateSalesOrderFailureWithRollBack(context);
            CreateSalesOrderFailureWithNoRollBack(context);
            Console.ReadLine();
        }
        
        public static void ReadLegalEntities(Resources context)
        {
            Console.WriteLine();
            foreach (var legalEntity in context.LegalEntities.AsEnumerable())
            {
                Console.WriteLine("Name: {0}", legalEntity.Name);
            }            
        }

        public static void CreateSalesOrderSuccess(Resources context)
        {
            string salesOrderNumber = "100005";
            try
            {
                SalesOrderHeader salesOrder = new SalesOrderHeader();
                DataServiceCollection<SalesOrderHeader> salesOrderCollection = new DataServiceCollection<SalesOrderHeader>(context);
                salesOrderCollection.Add(salesOrder);

                salesOrder.SalesOrderNumber = salesOrderNumber; // Change number sequence setting in AX to allow user to set values.
                salesOrder.CurrencyCode = "USD";
                salesOrder.InvoiceCustomerAccountNumber = "US-001";
                salesOrder.OrderingCustomerAccountNumber = "US-001";
                salesOrder.LanguageId = "en-us";
                salesOrder.DataAreaId = "USMF";

                SalesOrderLine salesOrderLine = new SalesOrderLine();
                DataServiceCollection<SalesOrderLine> salesOrderLineCollection = new DataServiceCollection<SalesOrderLine>(context);
                salesOrderLineCollection.Add(salesOrderLine);

                salesOrderLine.SalesOrderNumber = salesOrder.SalesOrderNumber;
                salesOrderLine.ItemNumber = "1000";
                salesOrderLine.OrderedSalesQuantity = 1;
                salesOrderLine.ShippingSiteId = "2";
                salesOrderLine.ShippingWarehouseId = "24";
                salesOrderLine.DataAreaId = "USMF";

                context.SaveChanges(SaveChangesOptions.PostOnlySetProperties | SaveChangesOptions.BatchWithSingleChangeset);
                Console.WriteLine(string.Format("Invoice {0} - Saved !", salesOrderNumber));
            }
            catch (DataServiceRequestException e)
            {
                Console.WriteLine(string.Format("Invoice {0} - Save Failed !", salesOrderNumber));
            }
        }

        public static void CreateSalesOrderFailureWithRollBack(Resources context)
        {
            string salesOrderNumber = "100006";

            try
            {
                SalesOrderHeader salesOrder = new SalesOrderHeader();
                DataServiceCollection<SalesOrderHeader> salesOrderCollection = new DataServiceCollection<SalesOrderHeader>(context);
                salesOrderCollection.Add(salesOrder);

                salesOrder.SalesOrderNumber = salesOrderNumber; // Change number sequence setting in AX to allow user to set values.
                salesOrder.CurrencyCode = "USD";
                salesOrder.InvoiceCustomerAccountNumber = "US-001";
                salesOrder.OrderingCustomerAccountNumber = "US-001";
                salesOrder.LanguageId = "en-us";
                salesOrder.DataAreaId = "USMF";

                SalesOrderLine salesOrderLine = new SalesOrderLine();
                DataServiceCollection<SalesOrderLine> salesOrderLineCollection = new DataServiceCollection<SalesOrderLine>(context);
                salesOrderLineCollection.Add(salesOrderLine);

                salesOrderLine.SalesOrderNumber = salesOrder.SalesOrderNumber;
                salesOrderLine.ItemNumber = "1000";
                salesOrderLine.OrderedSalesQuantity = 1;
                salesOrderLine.ShippingSiteId = "ABC"; // Error in line.
                salesOrderLine.ShippingWarehouseId = "24";
                salesOrderLine.DataAreaId = "USMF";

                context.SaveChanges(SaveChangesOptions.PostOnlySetProperties | SaveChangesOptions.BatchWithSingleChangeset); // Batch with Single Changeset ensure the saved changed runs in all-or-nothing mode.
                Console.WriteLine(string.Format("Invoice {0} - Save Failed !", salesOrderNumber));
            }
            catch (DataServiceRequestException e)
            {
                Console.WriteLine(string.Format("Invoice {0} - Save Failed !", salesOrderNumber));
            }
        }

        public static void CreateSalesOrderFailureWithNoRollBack(Resources context)
        {
            string salesOrderNumber = "100007";

            try
            {
                SalesOrderHeader salesOrder = new SalesOrderHeader();
                DataServiceCollection<SalesOrderHeader> salesOrderCollection = new DataServiceCollection<SalesOrderHeader>(context);
                salesOrderCollection.Add(salesOrder);

                salesOrder.SalesOrderNumber = salesOrderNumber; // Change number sequence setting in AX to allow user to set values.
                salesOrder.CurrencyCode = "USD";
                salesOrder.InvoiceCustomerAccountNumber = "US-001";
                salesOrder.OrderingCustomerAccountNumber = "US-001";
                salesOrder.LanguageId = "en-us";
                salesOrder.DataAreaId = "USMF";

                SalesOrderLine salesOrderLine = new SalesOrderLine();
                DataServiceCollection<SalesOrderLine> salesOrderLineCollection = new DataServiceCollection<SalesOrderLine>(context);
                salesOrderLineCollection.Add(salesOrderLine);

                salesOrderLine.SalesOrderNumber = salesOrder.SalesOrderNumber;
                salesOrderLine.ItemNumber = "1000";
                salesOrderLine.OrderedSalesQuantity = 1;
                salesOrderLine.ShippingSiteId = "ABC"; // Error in line.
                salesOrderLine.ShippingWarehouseId = "24";
                salesOrderLine.DataAreaId = "USMF";

                context.SaveChanges(SaveChangesOptions.PostOnlySetProperties | SaveChangesOptions.BatchWithIndependentOperations); // Batch with Independent Operations will result in header being saved but not the line as line has an error.
                Console.WriteLine(string.Format("Invoice {0} - Save Failed !", salesOrderNumber));
            }
            catch (DataServiceRequestException e)
            {
                Console.WriteLine(string.Format("Invoice {0} - Save Failed !", salesOrderNumber));
            }
        }
    }
}
