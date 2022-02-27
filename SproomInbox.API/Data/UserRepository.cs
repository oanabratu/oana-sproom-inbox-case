using SproomInbox.API.Data.Entities;

namespace SproomInbox.API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _ctx;
        public UserRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public User CreateUser(User user)
        {
            var addedEntity = _ctx.Users.Add(user);
            _ctx.SaveChanges();
            return addedEntity.Entity;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _ctx.Users;
        }

        public User GetUserById(string id)
        {
            return _ctx.Users
                .FirstOrDefault(d => d.Username == id);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
