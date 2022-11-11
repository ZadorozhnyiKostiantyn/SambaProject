using Microsoft.AspNetCore.Authorization;
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

            Console.WriteLine(authResult);

            

            if (authResult == null)
            {
                return RedirectToAction("Login");
            }

            HttpContext.Session.SetString("Token", authResult.Token);
            return RedirectToAction("Index", "Home");
        }
    }
}