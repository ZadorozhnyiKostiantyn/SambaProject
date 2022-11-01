using Microsoft.AspNetCore.Identity;
using SambaProject.Data.Models;
using SambaProject.Service.Authentication;

namespace SambaProject.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
