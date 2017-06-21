using Newtonsoft.Json;
using ODataUtility.Microsoft.Dynamics.DataEntities;
using System;
using System.Linq;

namespace ODataConsoleApplication
{
    class SimpleCRUDExamples
    {
        public static void SimpleCRUD(Resources d365)
        {
            string assetMajorTypeId = "Test01";
            string company = "USMF";

            // Create
            d365.AddToAssetMajorTypes(new AssetMajorType
            {
                DataAreaId = company,
                MajorTypeId = assetMajorTypeId,
                Description = "Description of Test01"
            });

            d365.SaveChanges();

            // Read
            var assetMajorType = d365.AssetMajorTypes.Where(x => x.DataAreaId == company && x.MajorTypeId == assetMajorTypeId).First();
            Console.WriteLine(JsonConvert.SerializeObject(assetMajorType));

            Console.WriteLine("Asset Major type of ID {0} successfully created and read.", assetMajorType.MajorTypeId);

            // Update
            assetMajorType.Description = "Updated description";
            d365.UpdateObject(assetMajorType);
            d365.SaveChanges();

            var assetMajorTypeAfterUpdate = d365.AssetMajorTypes.Where(x => x.DataAreaId == company && x.MajorTypeId == assetMajorTypeId).First();
            Console.WriteLine(JsonConvert.SerializeObject(assetMajorTypeAfterUpdate));

            Console.WriteLine("Asset Major type of ID {0} successfully updated.", assetMajorTypeAfterUpdate.MajorTypeId);

            // Delete
            d365.DeleteObject(assetMajorTypeAfterUpdate);
            d365.SaveChanges();

            var assetMajorTypeAfterDelete = d365.AssetMajorTypes.Where(x => x.DataAreaId == company && x.MajorTypeId == assetMajorTypeId);

            Console.WriteLine("Records found = {0}", assetMajorTypeAfterDelete.Count());
            Console.WriteLine("Asset Major type of ID {0} successfully deleted.", assetMajorTypeAfterUpdate.MajorTypeId);
        }
    }
}
