using Convertor.Models;
using Convertor.Services;
using Microsoft.AspNetCore.Mvc;

namespace Convertor.Controllers
{
    [Route("api/[controller]")]
    public class JsonConvertorController : BaseConvertorController<JsonDoc>
    {
        public JsonConvertorController(IDocumentService documentService, IConvertorFactory convertorFactory, ILogger<BaseConvertorController<JsonDoc>> logger) 
            : base(documentService, convertorFactory, logger)
        {
        }
    }
}
