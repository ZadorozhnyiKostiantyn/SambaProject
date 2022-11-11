using SambaProject.Data.Models;

namespace SambaProject.Data.Repository
{
    public interface IAccessRuleRepository
    {
        public Task<List<AccessRuleRoles>> GetAllRuleAsync();
    }
}
