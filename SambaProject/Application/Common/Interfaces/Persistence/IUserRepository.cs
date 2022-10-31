using SambaProject.Models;

namespace SambaProject.Application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        public Task AddUserAsync(User user);
        public Task<User?> GetUserByUserNameAsync(string username);
        public Task<List<User>> GetAllUserAsync();

    }
}
