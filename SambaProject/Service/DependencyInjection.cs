using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SambaProject.Data.Models;
using SambaProject.Models;
using SambaProject.Service.Administration;
using SambaProject.Service.Authentication;
using Syncfusion.EJ2.Diagrams;
using System.Text;

namespace SambaProject.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddService(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services.AddAuth(configuration);

            var networkSettings = new NetworkSettings();
            configuration.Bind(NetworkSettings.SectionName, networkSettings);
            services.AddSingleton(networkSettings);
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }

        public static IServiceCollection AddAuth(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings);
            services.AddSingleton(Options.Create(jwtSettings));

            services.AddSingleton<IJwtTokenGeneratorService, JwtTokenGeneratorService>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret))
                    };
                });

            return services;
        }
    }
}
