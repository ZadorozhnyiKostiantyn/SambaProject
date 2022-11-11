using SambaProject.Data.Models;

namespace SambaProject.Data.Repository
{
    public interface IAccessRuleRepository
    {
        public List<AccessRuleRoles> GetAllRule();
    }
}
