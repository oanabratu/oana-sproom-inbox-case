
using SproomInbox.API.Data.Entities;
using SproomInbox.Shared;

namespace SproomInbox.API.Services
{
    public interface IUserService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        ServiceResult<User> CreateUser(UserModel newUser);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ServiceResult<IEnumerable<User>> GetAllUsers();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ServiceResult<User> GetUserById(string id);
    }
}