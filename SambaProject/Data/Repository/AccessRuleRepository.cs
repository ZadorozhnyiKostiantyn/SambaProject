using Microsoft.EntityFrameworkCore;
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

        public async Task<List<AccessRuleRoles>> GetAllRuleAsync()
        {
            return await _context.AccessRules.ToListAsync();
        }
    }
}
