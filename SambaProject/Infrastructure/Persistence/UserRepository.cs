using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SambaProject.Application.Common.Interfaces.Persistence;
using SambaProject.Data;
using SambaProject.Models;

namespace SambaProject.Infrastructure.Persistence
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

        public async Task<List<User>> GetAllUserAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User?> GetUserByUserNameAsync(string username)
        {
            if (username is null)
            {
                throw new ArgumentNullException("This user is not exist");
            }

            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        }
    }
}
