using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SambaProject.Application.Service.Authentication;
using SambaProject.Data.Enum;
using SambaProject.Infrastructure.Persistence;
using SambaProject.Models;

namespace SambaProject.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CheckLogin(User user)
        {
            var authResult = await _authenticationService.Login(
                user.Username,
                user.Password);

            if (authResult == null)
            {
                return RedirectToAction("Login");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}