using Microsoft.EntityFrameworkCore;

namespace SambaProject.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddData(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<ApplicationDbContext>();
            
            services.AddDbContextPool<ApplicationDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            );

            return services;
        }
    }
}
