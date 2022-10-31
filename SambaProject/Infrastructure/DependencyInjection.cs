
using SambaProject.Application.Common.Interfaces.Persistence;
using SambaProject.Infrastructure.Persistence;


namespace SambaProject.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
