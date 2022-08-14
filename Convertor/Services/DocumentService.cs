using Convertor.Models;
using Microsoft.EntityFrameworkCore;

namespace Convertor.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly ConvertorContext _convertorContext;

        public DocumentService(ConvertorContext convertorContext)
        {
            _convertorContext = convertorContext;
        }

        public async Task<IEnumerable<Document?>> GetAllAsync()
        {
            return await _convertorContext.Documents.ToListAsync();
        }

        public async Task<Document?> GetDocumentByIdAsync(long id)
        {
            return await _convertorContext.Documents.FindAsync(id);

        }

        public async Task<long> UploadDocumentAsync(Document document)
        { 
            await _convertorContext.Documents.AddAsync(document);
            await _convertorContext.SaveChangesAsync();
            return document.Id;
        }

        public async void DeleteDocumentAsync(long id)
        {
            var toBeDeleted = await GetDocumentByIdAsync(id);
            if(toBeDeleted is null) throw new KeyNotFoundException($"document with id {id} does not exist"); 
            _convertorContext.Remove(toBeDeleted);
            await _convertorContext.SaveChangesAsync();

        }
    }
}
