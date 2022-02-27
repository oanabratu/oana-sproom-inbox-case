using Microsoft.AspNetCore.Mvc;
using SproomInbox.Shared;
using SproomInbox.API.Services;

namespace SproomInbox.API
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;


        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }


        /// <summary>
        /// Create new document
        /// </summary>
        /// <param name="newDocument"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateDocument([FromBody] DocumentModel newDocument)
        {
            // TODO Model Validation 

            var result = await _documentService.CreateDocumentAsync(newDocument);

            if (result.IsSuccessful == false)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Created("document", result.Data);
        }


        /// <summary>
        /// Get all documents filtered by query parameters
        /// </summary>
        /// <param name="queryParams"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllDocuments([FromQuery] DocumentQueryParams queryParams)
        {
            var getAllDocumentsResult = await _documentService.GetAllDocumentsAsync(queryParams);

            return Ok(getAllDocumentsResult.Data);
        }


        /// <summary>
        /// Get document by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetDocumentHistory(Guid id)
        {
            var documentStatesResult = await _documentService.GetDocumentHistoryAsync(id);

            return Ok(documentStatesResult.Data);
        }


        /// <summary>
        /// Approve a document
        /// </summary>
        /// <param name="id"></param>
        /// <param name="changeStateParams"></param>
        /// <returns></returns>

        [HttpPut("{id}/approve")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ApproveDocument(Guid id, [FromBody] ChangeStateParams changeStateParams)
        {
            var result = await _documentService.ApproveDocumentAsync(id, changeStateParams);

            if (result.IsSuccessful == false)
            {
                return BadRequest(result.ErrorMessage);
            }

            return NoContent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="changeStateParams"></param>
        /// <returns></returns>
        [HttpPut("{id}/reject")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RejectDocument(Guid id, [FromBody] ChangeStateParams changeStateParams)
        {
            var result = await _documentService.RejectDocumentAsync(id, changeStateParams);

            if (result.IsSuccessful == false)
            {
                return BadRequest(result.ErrorMessage);
            }

            return NoContent();
        }
    }
}