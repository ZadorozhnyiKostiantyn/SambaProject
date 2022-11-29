using Microsoft.EntityFrameworkCore;
using SambaProject.Data.Models;
using SambaProject.Data.Configuration;

namespace SambaProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AccessRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AccessRuleConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<AccessRole> AccessRoles { get; set; }
        public DbSet<AccessRuleRoles> AccessRules { get; set; }
    }
}
