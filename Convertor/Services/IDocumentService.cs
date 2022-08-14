using Convertor.Models;

namespace Convertor.Services
{
    public interface IDocumentService
    { 
        Task<IEnumerable<Document?>> GetAllAsync();
        Task<Document?> GetDocumentByIdAsync(long id);
        Task<long> UploadDocumentAsync(Document document);
        void DeleteDocumentAsync(long id);
    }
}
