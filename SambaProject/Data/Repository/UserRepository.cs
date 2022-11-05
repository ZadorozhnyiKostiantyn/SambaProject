using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SambaProject.Data.Models;

namespace SambaProject.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(User user)
        {
            if (user.Username.IsNullOrEmpty())
            {
                throw new ArgumentException("No correctly Data");
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
            {
                throw new ArgumentNullException("This user is not exist");
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<User?> GetUserByUserNameAsync(string username)
        {
            if (username is null)
            {
                throw new ArgumentNullException("This user is not exist");
            }

            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task UpdateUserAsync(User newData)
        {
            if (newData == null)
            {
                throw new ArgumentNullException("New data is null");
            }

            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserId == newData.UserId);

            if (user == null)
            {
                throw new ArgumentNullException("This user is not exist");
            }

            user.Username = newData.Username;
            user.AccessRoleId = newData.AccessRoleId;

            await _context.SaveChangesAsync();  
        }
    }
}
