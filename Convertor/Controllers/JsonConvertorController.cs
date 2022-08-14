using Convertor.Models;
using Convertor.Services;
using Microsoft.AspNetCore.Mvc;

namespace Convertor.Controllers
{
    [Route("api/[controller]")]
    public class XmlConvertorController : BaseConvertorController<XmlDoc>
    {
        public XmlConvertorController(IDocumentService documentService, IConvertorFactory convertorFactory, ILogger<BaseConvertorController<XmlDoc>> logger) 
            : base(documentService, convertorFactory, logger)
        {
        }
    }
}
