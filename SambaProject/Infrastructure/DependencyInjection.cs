using Microsoft.AspNetCore.Identity;
using SambaProject.Application.Common.Interfaces.Persistence;
using SambaProject.Infrastructure.Persistence;
using SambaProject.Models;

namespace SambaProject.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            return services;
        }
    }
}
