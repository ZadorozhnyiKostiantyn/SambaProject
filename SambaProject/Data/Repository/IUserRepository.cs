using SambaProject.Data.Models;

namespace SambaProject.Data.Repository
{
    public interface IUserRepository
    {
        public Task AddUserAsync(User user);
        public Task<User?> GetUserByUsernameAsync(string username);
        public Task<User?> GetUserByIdAsync(int userId);
        public Task<List<User>> GetAllUserAsync();
        public Task UpdateUserAsync(User newData);
        public Task DeleteUserAsync(int userId);
        public Task<List<User>> SearchUsersAsync(string searchString);
         
    }
}
