using Microsoft.AspNetCore.Mvc;
using SambaProject.Helpers.Filters;
using System.Reflection;

namespace SambaProject.Helpers.Attribute
{
    public class AuthorizeUserAttribute : TypeFilterAttribute
    {
        public AuthorizeUserAttribute() : base(typeof(AuthorizeUserFilter))
        {
        }
    }
}
