using Convertor.Services;
using Microsoft.AspNetCore.Mvc;
using Convertor.Models;
using IMailService = Convertor.Services.IMailService;

namespace Convertor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly IMailService _mailService;
        private readonly ILogger<DocumentController> _logger;


        public DocumentController(IDocumentService documentService, IMailService mailService, ILogger<DocumentController> logger)
        {
            _documentService = documentService;
            _mailService = mailService;
            _logger = logger;   
        }

        [HttpGet("download/{id}",Name="DownloadDocument")]
        public async Task<IActionResult> DownloadDocument(long id)
        {
            var res = await _documentService.GetDocumentByIdAsync(id);
            if (res == null)
            {
                return BadRequest(new { Error = "Invalid id" });
            }

            return File(res.Data, $"application/{res.Type}", $"{res.Name}.{res.Type}");
        }

        //Not sure if this is the desired behavior for "načíst data z HTTP URL (nelze ukládat)", but here it is
        [HttpGet("get-from-url")]
        public async Task<IActionResult> GetFromUrl(string urlSource,string resultDocumentName)
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync($"{urlSource}");
                var byteArray = await response.Content.ReadAsByteArrayAsync();

                return File(byteArray, response.Content.Headers.ContentType.MediaType,resultDocumentName);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("send-email")]
        public async Task<IActionResult> SendAsEmail(string toEmailAddress, long documentId)
        {
            try
            {
                var res = await _documentService.GetDocumentByIdAsync(documentId);
                using var stream = new MemoryStream(res.Data);
                var formFile = new FormFile(stream, 0, res.Data.Length, "Conversion Result", $"{res.Name}.{res.Type}")
                {
                    Headers = new HeaderDictionary(),
                    ContentType = $"application/{res.Type}"
                };
                var request = new MailRequest()
                {
                    Attachment = formFile,
                    Body = "Your file conversion is complete!",
                    Subject = "Conversion Complete",
                    ToEmail = toEmailAddress
                };
                await _mailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }



    }
}
