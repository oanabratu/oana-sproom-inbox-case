using Microsoft.AspNetCore.Mvc;
using SproomInbox.Shared;

namespace SproomInbox.WebApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {

        private static HttpClient _httpClient = new HttpClient();

        private readonly ILogger<DocumentController> _logger;

        public DocumentController(ILogger<DocumentController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<DocumentModel>> GetDocuments()
        {
            var result = await _httpClient.GetAsync("http://localhost:6170/Document");
            return await result.Content.ReadFromJsonAsync<IEnumerable<DocumentModel>>() ?? Enumerable.Empty<DocumentModel>();
        }


        [HttpGet("{id}/history")]
        public async Task<IEnumerable<StateHistoryModel>> GetDocumentHistory(Guid id)
        {
            var result = await _httpClient.GetAsync($"http://localhost:6170/Document/{id}/history");
            return await result.Content.ReadFromJsonAsync<IEnumerable<StateHistoryModel>>() ?? Enumerable.Empty<StateHistoryModel>();
        }
    }
}