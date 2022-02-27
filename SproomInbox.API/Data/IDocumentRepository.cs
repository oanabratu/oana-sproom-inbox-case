using SproomInbox.API.Data.Entities;
using SproomInbox.Shared;

namespace SproomInbox.API.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDocumentRepository
    {
        Task<Document> CreateDocumentAsync(Document document);
        Task<IEnumerable<Document>> GetAllDocumentsAsync();
        Task<IEnumerable<Document>> GetAllDocumentsAsync(DocumentQueryParams type);
        Task<Document> GetDocumentById(Guid id);
        Task<IEnumerable<StateHistory>> GetDocumentHistory(Guid id);
        Task SaveAsync();
    }
}