using Microsoft.AspNetCore.Mvc;

namespace SproomInbox.API
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
       private readonly ILogger<DocumentController> _logger;

        public DocumentController(ILogger<DocumentController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get a list of documents
        /// </summary>
        /// <returns>A list of documents</returns>
        [HttpGet]        
        public Task<IEnumerable<Document>> GetDocuments()
        {
            //TODO: Add implementation instead of this dummy
            Random rnd = new Random();
            return Task.FromResult(Enumerable.Range(1, rnd.Next(3,10)).Select(index => new Document
            {
                CreationDate = DateTime.Now.AddDays(-rnd.Next(365)),
                
            }));
        }
    }
}