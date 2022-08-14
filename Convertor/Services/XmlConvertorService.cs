using Convertor.Models;
using Newtonsoft.Json;
using System.Text;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace Convertor.Services
{
    public class XmlConvertorService : ConvertorService
    {
        public override JsonDoc ConvertToJson(Document originalDocument)
        {

            try
            {
                var xml = Encoding.UTF8.GetString(originalDocument.Data);
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xml);

                var json = JsonConvert.SerializeXmlNode(xmlDoc,Formatting.Indented);
                if (json == null) throw new ArgumentNullException(json);

                JsonDoc newDoc = new()
                {
                    CreatedDate = DateTime.Now,
                    Data = Encoding.UTF8.GetBytes(json),
                    Name = originalDocument.Name
                };

                return newDoc;

            }
            catch (Exception e)
            {
                throw new ArgumentException("Data provided by the input of XmlDoc are not valid", e);
            }
        }

        public override XmlDoc ConvertToXml(Document originalDocument)
        {
            if (Validate(Encoding.UTF8.GetString(originalDocument.Data)))
                return new XmlDoc()
                {
                    CreatedDate = DateTime.Now,
                    Data = originalDocument.Data,
                    Name = originalDocument.Name,

                };

            throw new ArgumentException("Data provided by the input of XmlDoc are not valid");
        }

        //Used for ConvertToXml method, but can be reused for example in controller. 
        //Not used in ConvertToJson due to calling XmlDocument.Load() twice
        public override bool Validate(string xmlString)
        {
            try
            {
                new XmlDocument().Load(xmlString);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


    }
}
