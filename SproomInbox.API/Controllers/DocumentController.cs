using Microsoft.AspNetCore.Mvc;
using SproomInbox.Shared;
using SproomInbox.API.Services;

namespace SproomInbox.API
{
    /// <summary>
    /// Document controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="documentService"></param>
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
        [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateDocument([FromBody] DocumentModel newDocument)
        {
            if (newDocument == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _documentService.CreateDocumentAsync(newDocument);

            if (result.IsSuccessful == false)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Created("document", result.Data);
        }


        /// <summary>
        /// Get all documents
        /// </summary>
        /// <param name="queryParams">Document Query Parameters</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllDocuments([FromQuery] DocumentQueryParams queryParams)
        {

            var getAllDocumentsResult = await _documentService.GetAllDocumentsAsync(queryParams);

            return Ok(getAllDocumentsResult.Data);
        }


        /// <summary>
        /// Get document history
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/history")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDocumentHistory(Guid id)
        {
            var documentStatesResult = await _documentService.GetDocumentHistoryAsync(id);

            return Ok(documentStatesResult.Data);
        }


        /// <summary>
        /// Approve document 
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
        /// Aprove multiple documents
        /// </summary>
        /// <param name="approveAllDocumentsParams"></param>
        /// <returns></returns>
        [HttpPut("approveAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ApproveAllDocuments([FromBody] ApproveAllDocumentsParams approveAllDocumentsParams)
        {
            var operationResults = new List<OperationResultModel>();   

            foreach(var documentId in approveAllDocumentsParams.DocumentIds)
            {
                var result = await _documentService.ApproveDocumentAsync(documentId, new ChangeStateParams { Username = approveAllDocumentsParams.Username });

                operationResults.Add(new OperationResultModel
                {
                    Id = documentId,
                    IsSuccessful = result.IsSuccessful
                });
            }

            return Ok(operationResults);
        }

        /// <summary>
        /// Reject document
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

        /// <summary>
        /// Reject multiple documents
        /// </summary>
        /// <param name="approveAllDocumentsParams"></param>
        /// <returns></returns>
        [HttpPut("rejectAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RejectAllDocuments([FromBody] RejectAllDocumentsParams approveAllDocumentsParams)
        {
            var operationResults = new List<OperationResultModel>();

            foreach (var documentId in approveAllDocumentsParams.DocumentIds)
            {
                var result = await _documentService.RejectDocumentAsync(documentId, new ChangeStateParams { Username = approveAllDocumentsParams.Username });

                operationResults.Add(new OperationResultModel
                {
                    Id = documentId,
                    IsSuccessful = result.IsSuccessful
                });
            }

            return Ok(operationResults);
        }
    }
}