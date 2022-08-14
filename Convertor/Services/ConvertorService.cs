using Convertor.Models;

namespace Convertor.Services
{
    public abstract class ConvertorService
    {
        public abstract JsonDoc ConvertToJson(Document originalDocument);
        public abstract XmlDoc ConvertToXml(Document originalDocument);
        public abstract bool Validate(string originalDocumentText);
    }
}
