using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SambaProject.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SambaProject.Service.Authentication
{
    public class JwtValidatorService : IJwtValidatorService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtValidatorService(IOptions<JwtSettings> jwtOptions)
        {
            _jwtSettings = jwtOptions.Value;
        }

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidAudience = _jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_jwtSettings.Secret))
                }, out SecurityToken validatedToken); ;
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
