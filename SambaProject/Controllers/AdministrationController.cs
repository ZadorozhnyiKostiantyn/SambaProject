using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SambaProject.Data.Models;
using SambaProject.Models;
using SambaProject.Service.Administration;
using SambaProject.Service.Authentication;

namespace SambaProject.Controllers
{
    [Authorize(Roles = "owner, admin")]
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
            await _authenticationService.Register(user.Username,
                                                  user.Password,
                                                  user.AccessRoleId);
            return RedirectToAction("AdminPanel");
        }

        [HttpPost]
        public async Task<IActionResult> SearchUsers(string username)
        {

            if (String.IsNullOrEmpty(username))
            {
                return Json(await _userService.GetAllUsersAsync());
            }

            return Json(await _userService.SearchAsync(username));
            
        }

        [HttpPut]
        public async Task<IActionResult> EditUser(IFormCollection value)
        {
            Console.WriteLine(Int32.Parse(value["id"]));
            Console.WriteLine(value["username"]);
            Console.WriteLine(value["accessRoleId"]);
            var user = new User
            {
                UserId = Int32.Parse(value["id"]),
                Username = value["username"],
                AccessRoleId = Int16.Parse(value["accessRoleId"])
            };
            await _userService.UpdateUserAsync(user);

            return Json(user);
        }

        // DELETE: Users/Delete/5
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return RedirectToAction("AdminPanel");
        }
    }
}
