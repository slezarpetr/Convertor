using Convertor.Models;
using Convertor.Services;
using Microsoft.AspNetCore.Mvc;

namespace Convertor.Controllers
{
    [ApiController]
    public abstract class BaseConvertorController<T> : ControllerBase where T: Document,new()
    {

        private readonly IDocumentService _documentService;
        private readonly IConvertorFactory _convertorFactory;
        private readonly ILogger<BaseConvertorController<T>> _logger;

        protected BaseConvertorController(IDocumentService documentService,IConvertorFactory convertorFactory,ILogger<BaseConvertorController<T>> logger)
        {
            _documentService = documentService;
            _convertorFactory = convertorFactory;
            _logger = logger;
        }

        [HttpPost("convert-to-json")]
        public async Task<IActionResult> ConvertToJson(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            if (memoryStream.Length >= 10000000) return BadRequest("File too bit (10MB limit)");

            var document = new T
            {
                Data = memoryStream.ToArray(),
                Name = Path.GetRandomFileName()
            };

            try
            {
                var convertorService = _convertorFactory.GetConvertorService(typeof(T));
                var converted = convertorService.ConvertToJson(document);
                var createdId = await _documentService.UploadDocumentAsync(converted);
                var actionName = nameof(DocumentController.DownloadDocument);

                return CreatedAtRoute(actionName, new { controller = "document", id = createdId }, null);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPost("convert-to-xml")]
        public async Task<IActionResult> ConvertToXml(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            if (memoryStream.Length >= 10000000) return BadRequest("Something went wrong");

            var document = new T
            {
                Data = memoryStream.ToArray(),
                Name = Path.GetRandomFileName()
            };

            try
            {
                var converted = _convertorFactory
                    .GetConvertorService(typeof(T))
                    .ConvertToXml(document);
                var createdId = await _documentService.UploadDocumentAsync(converted);
                var actionName = nameof(DocumentController.DownloadDocument);

                return CreatedAtRoute(actionName, new { controller = "document", id = createdId }, null);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

    }
}