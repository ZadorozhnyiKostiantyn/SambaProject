using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SambaProject.Data.Models;
using SambaProject.Error;
using SambaProject.Service.Authentication.Interface;

namespace SambaProject.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CheckLogin(User user)
        {
            var authResult = await _authenticationService.Login(
                user.Username,
                user.Password);

            if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
            {
                TempData["ErrorMessage"] = Errors.Authentication.InvalidCredentials.Description;
                return RedirectToAction("Login");
            }

            HttpContext.Session.SetString("Token", authResult.Value.Token);
            return RedirectToAction("Index", "FileManager");
        }

        public IActionResult DeleteSession()
        {
            HttpContext.Session.Remove("Token");

            return RedirectToAction("Login");
        }
    }
}