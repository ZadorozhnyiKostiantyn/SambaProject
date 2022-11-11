using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using SambaProject.Data.Models;
using SambaProject.Models;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;

namespace SambaProject.Service.Authentication
{
    public class JwtDecodingService : IJwtDecodingService
    {
        public DecodingUserModel DecodeToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;

            return new DecodingUserModel
            {
                Id = Int32.Parse(tokenS.Claims.First(claim => claim.Type == "sub").Value),
                Username = tokenS.Claims.First(claim => claim.Type == "given_name").Value,
                AccessRole = tokenS.Claims.First(claim => claim.Type == "access_role").Value
            };
        }
    }
}
