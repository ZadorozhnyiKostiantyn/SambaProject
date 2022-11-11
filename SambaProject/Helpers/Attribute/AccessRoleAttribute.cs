using Microsoft.AspNetCore.Mvc;
using SambaProject.Helpers.Filters;
using SambaProject.Models;
using System.Security.Claims;

namespace SambaProject.Helpers.Attribute
{
    public class AccessRoleAttribute : TypeFilterAttribute
    {
        public AccessRoleAttribute(string calinType, params string[] claimValue) : base(typeof(AccessRoleFilter))
        {
            List<string> roles = new List<string>();
            roles.AddRange(claimValue);
            
            Arguments = new object[] 
            { 
                new AccessRoleClaim 
                { 
                    Type = calinType,
                    Value = roles
                }
            };
        }
    }
}
