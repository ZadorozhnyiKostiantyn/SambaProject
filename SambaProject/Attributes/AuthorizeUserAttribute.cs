using Microsoft.AspNetCore.Mvc;
using SambaProject.Filters;
using System.Reflection;

namespace SambaProject.Attributes
{
    public class AuthorizeUserAttribute : TypeFilterAttribute
    {
        public AuthorizeUserAttribute() : base(typeof(AuthorizeUserFilter))
        {
        }
    }
}
