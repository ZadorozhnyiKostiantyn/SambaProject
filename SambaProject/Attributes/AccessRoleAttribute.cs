using Microsoft.AspNetCore.Mvc;
using SambaProject.Filters;
using SambaProject.Models;

namespace SambaProject.Attributes
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
