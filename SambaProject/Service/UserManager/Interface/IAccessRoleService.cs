using SambaProject.Data.Models;
using SambaProject.Models;
using Syncfusion.EJ2.FileManager.Base;

namespace SambaProject.Service.UserManager.Interface
{
    public interface IAccessRoleService
    {
        public AccessRole? GetRoleById(int id);
        public List<AccessRole> GetAllRoles();
        public AccessDetails GetAccessDetails();

    }
}
