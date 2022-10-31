using Microsoft.AspNetCore.Identity;
using SambaProject.Application.Common.Interfaces.Authentication;
using SambaProject.Application.Common.Interfaces.Persistence;
using SambaProject.Application.Common.Interfaces.Services;
using SambaProject.Infrastructure.Authentication;
using SambaProject.Infrastructure.Persistence;
using SambaProject.Infrastructure.Services;
using SambaProject.Models;

namespace SambaProject.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            return services;
        }
    }
}
