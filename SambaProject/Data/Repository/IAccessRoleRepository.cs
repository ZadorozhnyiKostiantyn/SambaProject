using SambaProject.Data.Models;

namespace SambaProject.Data.Repository
{
    public interface IAccessRoleRepository
    {
        public Task<AccessRole> GetAccessRoleById(int id);
    }
}
