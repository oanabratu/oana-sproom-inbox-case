using Microsoft.AspNetCore.Mvc;
using SproomInbox.Shared;

namespace SproomInbox.WebApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        private static HttpClient _httpClient = new HttpClient();

        private readonly ILogger<DocumentController> _logger;

        public UserController(ILogger<DocumentController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            var result = await _httpClient.GetAsync($"http://localhost:6170/User");
            return await result.Content.ReadFromJsonAsync<IEnumerable<UserModel>>() ?? Enumerable.Empty<UserModel>();
        }


        [HttpPost]
        public async Task<UserModel?> CreateUser([FromBody] UserModel user)
        {
            HttpResponseMessage result = await _httpClient.PostAsJsonAsync("http://localhost:6170/User", user);
            if (result.IsSuccessStatusCode == false)
                return null;
            return await result.Content.ReadFromJsonAsync<UserModel>();
        }
    }
}