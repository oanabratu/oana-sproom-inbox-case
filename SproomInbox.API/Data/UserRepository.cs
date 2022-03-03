using Microsoft.EntityFrameworkCore;
using SproomInbox.API.Data.Entities;

namespace SproomInbox.API.Data
{
    /// <summary>
    /// UserRepository is the implementation of IUserRepository
    /// UserRepository makes User CRUD operations using Entity Framework
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _ctx;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ctx"></param>
        public UserRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        /// <summary>
        /// Creates a new user in the DB
        /// </summary>
        /// <param name="user"></param>
        /// <returns>The newly created user</returns>
        public async Task<User> CreateUserAsync(User user)
        {
            var addedEntity = _ctx.Users.Add(user);
            await _ctx.SaveChangesAsync();
            return addedEntity.Entity;
        }

        /// <summary>
        /// Retrieves all users from the DB
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _ctx.Users.ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _ctx.Users
                .FirstOrDefaultAsync(d => d.Username == id);
        }
    }
}
