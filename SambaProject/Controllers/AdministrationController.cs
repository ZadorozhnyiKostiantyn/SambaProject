using Microsoft.AspNetCore.Mvc;
using SambaProject.Application.Common.Interfaces.Persistence;
using SambaProject.Application.Service.Authentication;
using SambaProject.Models;

namespace SambaProject.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserRepository _userRepository;

        public AdministrationController(IAuthenticationService authenticationService, IUserRepository userRepository)
        {
            _authenticationService = authenticationService;
            _userRepository = userRepository;
        }

        [Route("AdminPanel", Name = "AdminPanel")]
        public async Task<IActionResult> AdminPanel()
        {
            return View(
                new AdminModel 
                { 
                    Users = await _userRepository.GetAllUserAsync()
                });
        }

        [HttpPost]
        public async Task<IActionResult> CheckRegister(User user)
        {
            await _authenticationService.Register(
                user.Username,
                user.Password,
                user.AccessRoleId);

            return RedirectToAction("AdminPanel");
        }
    }
}
