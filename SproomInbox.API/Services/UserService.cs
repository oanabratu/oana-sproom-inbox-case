using SproomInbox.API.Data;
using SproomInbox.API.Data.Entities;
using SproomInbox.Shared;

namespace SproomInbox.API.Services
{
    /// <summary>
    /// Service that handles Users
    /// </summary>
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userRepository"></param>
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Create user service
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public async Task<ServiceResult<User>> CreateUserAsync(UserModel newUser)
        {
            var result = new ServiceResult<User>
            {
                IsSuccessful = false
            };

            // Check if username has a value
            if (string.IsNullOrEmpty(newUser.Username))
            {
                result.ErrorMessage = "Can not create user, username is missing.";
                return result;
            }
            
            // Check if a user with this id already exists in the database
            var foundUser = await _userRepository.GetUserByIdAsync(newUser.Username);

            if (foundUser != null)
            {
                result.ErrorMessage = $"A user with {newUser.Username} username alreaady exists.";
                return result;
            }

            var user = new User
            {
                Username = newUser.Username,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName
            };

            User createdUser;
            try
            {
                createdUser = await _userRepository.CreateUserAsync(user);
            }
            catch(Exception e)
            {
                result.ErrorMessage = "Unable to create user. Call to database failed because " + e.Message;
                return result;
            }

            if(createdUser != null)
            {
                result.IsSuccessful = true;
                result.Data = createdUser;
            }
            else
            {
                result.IsSuccessful = false;
            }

            return result;
        }

        /// <summary>
        /// Get all users service
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<User>>> GetAllUsersAsync()
        {
            var result = new ServiceResult<IEnumerable<User>>();

            var getAllUsers = await _userRepository.GetAllUsersAsync();
            
            result.Data = getAllUsers;
            result.IsSuccessful = true;
            
            return result;
        }

        /// <summary>
        /// Get user by id service
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ServiceResult<User>> GetUserByIdAsync(string id)
        {
            var result = new ServiceResult<User>();

            var user = await _userRepository.GetUserByIdAsync(id);

            if(user != null)
            {
                result.Data = user;
                result.IsSuccessful = true;
            }
            else
            {
                result.IsSuccessful= false;
            }

            return result;
        }
    }
}

