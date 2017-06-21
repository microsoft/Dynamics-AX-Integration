using Microsoft.OData.Client;
using ODataUtility.Microsoft.Dynamics.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODataConsoleApplication
{
    class ODataChangesetsExample
    {
        public static void CreateSalesOrderInSingleChangeset(Resources context)
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

                context.SaveChanges(SaveChangesOptions.PostOnlySetProperties | SaveChangesOptions.BatchWithSingleChangeset); // Batch with Single Changeset ensure the saved changed runs in all-or-nothing mode.
                Console.WriteLine(string.Format("Invoice {0} - Saved !", salesOrderNumber));
            }
            catch (DataServiceRequestException e)
            {
                Console.WriteLine(string.Format("Invoice {0} - Save Failed !", salesOrderNumber));
            }
        }

        public static void CreateSalesOrderWithoutChangeset(Resources context)
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

                context.SaveChanges(SaveChangesOptions.PostOnlySetProperties | SaveChangesOptions.BatchWithIndependentOperations); // The changes within the branch are treated as independent operation. Failure in one will not roll back others.
                Console.WriteLine(string.Format("Invoice {0} - Save Failed !", salesOrderNumber));
            }
            catch (DataServiceRequestException e)
            {
                Console.WriteLine(string.Format("Invoice {0} - Save Failed !", salesOrderNumber));
            }
        }
    }
}
