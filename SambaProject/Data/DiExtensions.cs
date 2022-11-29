using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SambaProject.Data.Models;
using SambaProject.Data.Repository;
using SambaProject.Models;
using System.Text;
using SambaProject.Service.Authentication.Interface;
using SambaProject.Service.Authentication.Services;
using SambaProject.Service.UserManager.Interface;
using SambaProject.Service.UserManager.Services;

namespace SambaProject.Data
{
    public static class DiExtensions
    {
        public static IServiceCollection AddData(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

            services.AddDbContextPool<ApplicationDbContext>(options =>
                options.UseMySql(
                    configuration.GetConnectionString("MySqlDatabase"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("MySqlDatabase")))
            );
            return services;
        }

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
            services.AddScoped<IAccessRoleService, AccessRoleService>();
            services.AddScoped<IAccessRuleService, AccessRuleService>();
            services.AddScoped<IJwtDecodingService, JwtDecodingService>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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
