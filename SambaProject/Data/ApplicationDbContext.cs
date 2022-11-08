using Microsoft.EntityFrameworkCore;
using SambaProject.Data.Models;

namespace SambaProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        }

        public DbSet<User> Users { get; set; }
        public DbSet<AccessRole> AccessRoles { get; set; }
        public DbSet<AccessRuleRoles> AccessRules { get; set; }
    }
}
