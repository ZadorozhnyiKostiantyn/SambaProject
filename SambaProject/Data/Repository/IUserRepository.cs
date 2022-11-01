using SambaProject.Data.Models;

namespace SambaProject.Data.Repository
{
    public interface IUserRepository
    {
        public Task AddUserAsync(User user);
        public Task<User?> GetUserByUserNameAsync(string username);
        public Task<List<User>> GetAllUserAsync();
        public Task UpdateUserAsync(int userId, User newData);
        public Task DeleteUserAsync(int userId);
        
    }
}
