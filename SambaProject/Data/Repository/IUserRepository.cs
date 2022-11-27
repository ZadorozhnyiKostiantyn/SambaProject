using SambaProject.Data.Models;

namespace SambaProject.Data.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User?> GetUserByUsernameAsync(string username);

        public Task<List<User>> SearchUsersAsync(string searchString);

        //public Task UpdateUserAsync(User newData);
        
    }
}
