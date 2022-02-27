using SproomInbox.API.Data.Entities;

namespace SproomInbox.API.Data
{
    public interface IUserRepository
    {
        User CreateUser(User user);
        IEnumerable<User> GetAllUsers();
        User GetUserById(string id);
        void Save();
    }
}