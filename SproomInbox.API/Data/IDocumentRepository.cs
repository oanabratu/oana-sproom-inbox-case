using SproomInbox.API.Data.Entities;
using SproomInbox.Shared;

namespace SproomInbox.API.Data
{
    /// <summary>
    /// Document repository interface
    /// </summary>
    public interface IDocumentRepository
    {
        Task<Document> CreateDocumentAsync(Document document);
        Task<IEnumerable<Document>> GetAllDocumentsAsync();
        Task<IEnumerable<Document?>> GetAllDocumentsAsync(DocumentQueryParams type);
        Task<Document> GetDocumentByIdAsync(Guid id);
        Task<IEnumerable<StateHistory>> GetDocumentHistory(Guid id);
        Task SaveAsync();
    }
}