using Microsoft.EntityFrameworkCore;
using SambaProject.Data.Models;

namespace SambaProject.Data.Repository
{
    public class AccessRoleRepository : IAccessRoleRepository
    {
        ApplicationDbContext _context;

        public AccessRoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AccessRole> GetAccessRoleById(int id)
        {
            return await _context.AccessRoles.SingleOrDefaultAsync(r => r.AccessRoleId == id);
        }
    }
}
