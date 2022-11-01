using Microsoft.AspNetCore.Mvc;
using SambaProject.Data.Models;
using SambaProject.Service.Authentication;

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