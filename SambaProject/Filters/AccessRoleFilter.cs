using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SambaProject.Models;
using System.Security.Claims;

namespace SambaProject.Filters
{
    public class AccessRoleFilter : IAuthorizationFilter
    {
        private readonly AccessRoleClaim _claim;

        public AccessRoleFilter(AccessRoleClaim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool hasCalim = false;
            foreach (var value in _claim.Value)
            {
                hasCalim = context.HttpContext.User.Claims.Any(c => c.Type == _claim.Type && c.Value == value);
                if (hasCalim)
                {
                    break;
                }
            }

            if (!hasCalim)
            {
                //context.Result = new ForbidResult();
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        { "controller", "FileManager"}, { "action", "Index"}
                    });
            }
        }
    }
}
