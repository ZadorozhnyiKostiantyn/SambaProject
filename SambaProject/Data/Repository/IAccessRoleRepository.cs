using SambaProject.Data.Models;

namespace SambaProject.Data.Repository
{
    public interface IAccessRoleRepository
    {
        public AccessRole? GetAccessRoleById(int id);
        public List<AccessRole> GetAllAccessRole();
    }
}
