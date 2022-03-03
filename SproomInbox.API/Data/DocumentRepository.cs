using Microsoft.EntityFrameworkCore;
using SproomInbox.API.Data.Entities;
using SproomInbox.API.Services;
using SproomInbox.Shared;

namespace SproomInbox.API.Data
{
    /// <summary>
    /// DocumentRepository is the implementation of IDocumentRepository
    /// DocumentRepository makes Document CRUD operations using Entity Framework
    /// </summary>
    public class DocumentRepository : IDocumentRepository
    {
        private readonly AppDbContext _ctx;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctx"></param>
        public DocumentRepository(AppDbContext ctx)
        {
            _ctx = ctx;           
        }

        /// <summary>
        /// Create document
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public async Task<Document> CreateDocumentAsync(Document document)
        {
            var addedEntity = await _ctx.Documents.AddAsync(document);
            await _ctx.SaveChangesAsync();
            return addedEntity.Entity;
        }

        /// <summary>
        /// Get all documents
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Document>> GetAllDocumentsAsync()
        {
            return await _ctx.Documents.ToListAsync();
        }

        /// <summary>
        /// Get all documents with filter
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Document>> GetAllDocumentsAsync(DocumentQueryParams queryParams)
        {
            return await _ctx.Documents
                .Include("DocumentStates")
                .Where(p =>
                (queryParams.Type == null || (int)p.DocumentType == queryParams.Type) &&
                (queryParams.State == null || (int)p.State == queryParams.State) &&
                (queryParams.Username == null || p.AssignedToUser == queryParams.Username)).ToListAsync();
        }

        /// <summary>
        /// Get document by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Document?> GetDocumentByIdAsync(Guid id)
        {
            return await _ctx.Documents
                .Include("DocumentStates")
                .FirstOrDefaultAsync(d => d.Id == id);
        }



        /// <summary>
        /// save changes
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<StateHistory>> GetDocumentHistory(Guid id)
        {
            var foundDocument = await _ctx.Documents
                .Include("DocumentStates")
                .FirstOrDefaultAsync(d => d.Id == id);

            if(foundDocument != null)
            {
                return foundDocument.DocumentStates.ToList();
            }

            return new List<StateHistory>();
        }
    }
}

