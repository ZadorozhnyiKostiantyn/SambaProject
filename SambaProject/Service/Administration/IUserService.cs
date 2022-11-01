using SambaProject.Data.Models;

namespace SambaProject.Service.Administration
{
    public interface IUserService
    {
        public Task CreateUserAsync(string username, string password, int roleId);
        public Task<List<User>> GetAllUsersAsync();
        public Task UpdateUserAsync(int userId, User newData);
        public Task DeleteUserAsync(int userId);
    }
}
