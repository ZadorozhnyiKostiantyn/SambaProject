using Microsoft.EntityFrameworkCore;
using SambaProject.Data.Repository;

namespace SambaProject.Data
{
    public static class DiExtensions
    {
        public static IServiceCollection AddData(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddDbContextPool<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            );

            return services;
        }
    }
}
