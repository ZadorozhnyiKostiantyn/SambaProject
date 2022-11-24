using SambaProject.Data.Models;

namespace SambaProject.Data.Repository
{
    public class AccessRuleRepository : IAccessRuleRepository
    {
        private readonly ApplicationDbContext _context;

        public AccessRuleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<AccessRuleRoles> GetAllRule()
        {
            return _context.AccessRules.ToList();
        }
    }
}
