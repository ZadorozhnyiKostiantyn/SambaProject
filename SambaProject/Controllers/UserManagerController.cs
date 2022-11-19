using Microsoft.AspNetCore.Mvc;
using SambaProject.Data.Models;
using SambaProject.Helpers.Attribute;
using SambaProject.Models;
using SambaProject.Service.UserManager;
using SambaProject.Service.Authentication;

namespace SambaProject.Controllers
{
    [AuthorizeUser]
    [AccessRole("access_role", "Owner", "Admin")]
    public class UserManagerController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly IAccessRoleService _accessRoleService;

        public UserManagerController(
            IAuthenticationService authenticationService,
            IUserService userService,
            IAccessRoleService accessRoleService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
            _accessRoleService = accessRoleService;
        }

        [Route("UserManager", Name = "UserManager")]
        public async Task<IActionResult> UserManager()
        {
            return View(
                new UserManagerViewModel 
                { 
                    Users = await _userService.SearchAsync(""),
                    AccessRoles = _accessRoleService.GetAllRoles()
                });
        }

        [HttpPost]
        public async Task<IActionResult> CheckRegister(User user)
        {
            await _authenticationService.Register(
                    user.Username,
                    user.Password,
                    user.AccessRoleId);

            var newUser = await _userService.GetUserByUsernameAsync(user.Username);

            return Json(
                new UserModel
                {
                    Id = newUser.UserId,
                    Username = user.Username,
                    AccessRole = _accessRoleService.GetRoleById(user.AccessRoleId).Role
                }
            );
        }

        [HttpPost]
        public async Task<IActionResult> SearchUsers(string username)
        {
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

            var role = _accessRoleService.GetRoleById(Int16.Parse(value["accessRoleId"]));

            UserModel newUser = new UserModel
            {
                Id = user.UserId,
                Username = user.Username,
                AccessRole = role.Role
            };

            return Json(newUser);
        }

        // DELETE: Users/Delete/5
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return Json(id);
        }

        [HttpGet]
        public IActionResult GetAccessRoles()
        {
            return Json(_accessRoleService.GetAllRoles());
        }
    }
}
