using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ODataConsoleApplication
{
    /// <summary>
    /// Validates metadata document and shows warning messages for entity/enum name collisions.
    /// </summary>
    class MetadataDocumentValidator
    {
        /// <param name="args"></param>
        static void ValidateMetadataDocument(string[] args)
        {
            string filename = @"C:\Users\xyz\Documents\metadata.xml";  // change this path as needed

            XmlReader r = XmlReader.Create(File.Open(filename, FileMode.Open, FileAccess.Read));

            XmlDocument md = new XmlDocument();
            md.Load(r);

            var DefinedNames = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

            var DataServicesEdm = md.ChildNodes[1].ChildNodes[0];
            if (DataServicesEdm.Name.CompareTo("edmx:DataServices") != 0)
                throw new FormatException("Did not get the expected format.");

            var Schema = DataServicesEdm.ChildNodes[0];
            if (Schema.Name.CompareTo("Schema") != 0)
                throw new FormatException("Did not get the expected format.");

            foreach (var node in Schema)
            {
                var n = node as XmlNode;

                var nameAttribute = n.Attributes["Name"];
                if (nameAttribute != null)
                {
                    if (n.Name == "Action")
                        break;

                    if (DefinedNames.Keys.Contains(nameAttribute.Value))
                    {
                        Console.WriteLine($"Warning for {n.Name} \"{nameAttribute.Value}\" : {DefinedNames[nameAttribute.Value]} of name \"{nameAttribute.Value}\" is already defined.");
                    }
                    else
                    {
                        DefinedNames.Add(nameAttribute.Value, n.Name);
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
