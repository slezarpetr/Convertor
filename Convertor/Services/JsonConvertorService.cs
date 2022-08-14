using Convertor.Models;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace Convertor.Services
{
    public class JsonConvertorService : ConvertorService
    {
        public override JsonDoc ConvertToJson(Document originalDocument)
        {
            if (Validate(Encoding.UTF8.GetString(originalDocument.Data)))
                return new JsonDoc()
                {
                    CreatedDate = DateTime.Now,
                    Data = originalDocument.Data,
                    Name = originalDocument.Name,

                };
            throw new ArgumentException("Data provided by the input of JsonDoc are not valid");
        }

        public override XmlDoc ConvertToXml(Document originalDocument)
        {
            var json = Encoding.UTF8.GetString(originalDocument.Data);
            if (Validate(json))
            {
                var node = JsonConvert.DeserializeXNode(json)?.ToString();
                if (node != null)
                {
                    XmlDoc newDoc = new()
                    {
                        CreatedDate = DateTime.Now,
                        Data = Encoding.UTF8.GetBytes(node),
                        Name = originalDocument.Name
                    };

                    return newDoc;
                }

            }

            throw new ArgumentException("Data provided by the input of JsonDoc are not valid");
        }


        //Not the best way possible (validation by catching exceptions), but the quickest and with least code
        public override bool Validate(string jsonString)
        {
            try
            {
                JsonDocument.Parse(jsonString);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
