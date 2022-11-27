using Microsoft.EntityFrameworkCore;
using SambaProject.Data.Models;

namespace SambaProject.Data.Repository
{
    public class UserRepository : EFRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            if (username is null)
            {
                throw new ArgumentNullException("This user is not exist");
            }

            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task<List<User>> SearchUsersAsync(string searchString)
        {
            var users = from u in _context.Users select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.Username!.Contains(searchString));
            }
            else
            {
                return await _context.Users.ToListAsync();
            }

            return await users.ToListAsync();
        }

        //public async Task UpdateUserAsync(User newData)
        //{
        //    if (newData == null)
        //    {
        //        throw new Exception("New data is null");
        //    }

        //    var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == newData.Id);

        //    if (user == null)
        //    {
        //        throw new Exception("This user is not exist");
        //    }

        //    if (user.Username != newData.Username)
        //    {
        //        user.Username = newData.Username;
        //    }

        //    if (user.AccessRoleId != newData.AccessRoleId)
        //    {
        //        user.AccessRoleId = newData.AccessRoleId;
        //    }

        //    await _context.SaveChangesAsync();
        //}
    }
}
