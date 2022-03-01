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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserModel newUser)
        {
            // TODO Model Validation 

            var result = await _userService.CreateUserAsync(newUser);

            if (result.IsSuccessful == false)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Created("user", result.Data);
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var getAllDocumentsResult = await _userService.GetAllUsersAsync();

            return Ok(getAllDocumentsResult.Data);
        }
    }
}