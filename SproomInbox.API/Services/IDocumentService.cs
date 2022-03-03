using SproomInbox.API.Data.Entities;
using SproomInbox.Shared;

namespace SproomInbox.API.Services
{
    /// <summary>
    /// Documment service interface
    /// </summary>
    public interface IDocumentService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newDocument"></param>
        /// <returns></returns>
        Task<ServiceResult<Document>> CreateDocumentAsync(DocumentModel newDocument);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="changeStateParams"></param>
        /// <returns></returns>
        Task<ServiceResult<Document>> ApproveDocumentAsync(Guid id, ChangeStateParams changeStateParams);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="changeStateParams"></param>
        /// <returns></returns>
        Task<ServiceResult<Document>> RejectDocumentAsync(Guid id, ChangeStateParams changeStateParams);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        Task<ServiceResult<IEnumerable<Document>>> GetAllDocumentsAsync(DocumentQueryParams queryParams);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ServiceResult<Document>> GetDocumentByIdAsync(Guid id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ServiceResult<IEnumerable<StateHistory>>> GetDocumentHistoryAsync(Guid id);
    }
}
