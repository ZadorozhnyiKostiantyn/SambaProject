using Microsoft.EntityFrameworkCore;
using SambaProject.Data.Repository;

namespace SambaProject.Data
{
    public static class DiExtensions
    {
        public static IServiceCollection AddData(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccessRoleRepository, AccessRoleRepository>();
            services.AddScoped<IAccessRuleRepository, AccessRuleRepository>();

            services.AddDbContextPool<ApplicationDbContext>(options =>
                options.UseMySql(configuration.GetConnectionString("MySqlDatabase"),
                                 ServerVersion.AutoDetect(configuration.GetConnectionString("MySqlDatabase")))
            );

            return services;
        }
    }
}
