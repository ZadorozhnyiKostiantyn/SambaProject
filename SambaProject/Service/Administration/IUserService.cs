using SambaProject.Data.Models;

namespace SambaProject.Service.Administration
{
    public interface IUserService
    {
        public Task CreateUserAsync(string username, string password, int roleId);
        public Task<List<User>> GetAllUsersAsync();
        public Task UpdateUserAsync(User newData);
        public Task DeleteUserAsync(int userId);
        public Task<List<User>> SearchAsync(string query);
        public Task<User> GetUserByIdAsync(int userId);
    }
}
