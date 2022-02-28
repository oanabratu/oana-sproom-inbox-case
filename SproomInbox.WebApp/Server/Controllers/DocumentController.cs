using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using SproomInbox.Shared;
using System.Text;

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
        public async Task<IEnumerable<DocumentModel>> GetDocuments([FromQuery] DocumentQueryParams queryParams)
        {
            var param = new Dictionary<string, string>();


            if (queryParams != null)
            {
                if(queryParams.Username != null)
                    param.Add("username", queryParams.Username);

                if (queryParams.State != null)
                    param.Add("state", queryParams.State.Value.ToString());

                if (queryParams.Type != null)
                    param.Add("type", queryParams.Type.Value.ToString());
            }

            

            var requestUri = QueryHelpers.AddQueryString("http://localhost:6170/Document", param);



            var result = await _httpClient.GetAsync(requestUri);
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