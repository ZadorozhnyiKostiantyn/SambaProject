using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SambaProject.Data.Models;
using SambaProject.Data.Repository;
using SambaProject.Models;
using SambaProject.Service.Administration;
using SambaProject.Service.Authentication;

namespace SambaProject.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;

        public AdministrationController(IAuthenticationService authenticationService, IUserService userService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }

        [Route("AdminPanel", Name = "AdminPanel")]
        public async Task<IActionResult> AdminPanel()
        {
            return View(
                new AdminModel 
                { 
                    Users = await _userService.GetAllUsersAsync()
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

        // DELETE: Users/Delete/5
        [HttpPost]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return RedirectToAction("AdminPanel");
        }
    }
}
