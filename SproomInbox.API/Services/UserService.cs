using SproomInbox.API.Data;
using SproomInbox.API.Data.Entities;
using SproomInbox.Shared;

namespace SproomInbox.API.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public ServiceResult<User> CreateUser(UserModel newUser)
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
            var foundUser = _userRepository.GetUserById(newUser.Username);

            if (foundUser == null)
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
                createdUser = _userRepository.CreateUser(user);
            }
            catch(Exception e)
            {
                result.ErrorMessage = "Unable to create user. Call to database failed because " + e.Message;
                return result;
            }

            result.IsSuccessful = true;
            result.Data = createdUser;

            return result;
        }

        //get users
        public ServiceResult<IEnumerable<User>> GetAllUsers()
        {

            var result = new ServiceResult<IEnumerable<User>>();
            var getAllUsers = _userRepository.GetAllUsers();
            result.Data = getAllUsers;
            result.IsSuccessful = true;//TODO needed?
            return result;
        }

        public ServiceResult<User> GetUserById(string id)
        {
            var result = new ServiceResult<User>();

            var user = _userRepository.GetUserById(id);
            result.Data = user;
            return result;
        }
    }
}

