
using SproomInbox.API.Data.Entities;
using SproomInbox.Shared;

namespace SproomInbox.API.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        Task<ServiceResult<User>> CreateUserAsync(UserModel newUser);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult<IEnumerable<User>>> GetAllUsersAsync();
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ServiceResult<User>> GetUserByIdAsync(string id);
    }
}