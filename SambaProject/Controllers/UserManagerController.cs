using Microsoft.AspNetCore.Mvc;
using SambaProject.Data.Models;
using SambaProject.Models;
using SambaProject.Attributes;
using SambaProject.Error;
using SambaProject.Service.Authentication.Interface;
using SambaProject.Service.UserManager.Interface;
using SambaProject.Models.ViewModel;

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
            var authResult = await _authenticationService.Register(
                    user.Username,
                    user.Password,
                    user.AccessRoleId);

            if (authResult.IsError && authResult.FirstError == Errors.User.DuplicateUsername)
            {
                return Json(new { success = false, ErrorMessage = Errors.User.DuplicateUsername.Description});
            }

            var newUser = await _userService.GetUserByUsernameAsync(user.Username);

            return Json(
                new
                {
                    success = true, 
                    user = new UserModel
                    {
                        Id = newUser.Id,
                        Username = user.Username,
                        AccessRole = _accessRoleService.GetRoleById(user.AccessRoleId).Role
                    }
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
            var user = await _userService.GetUserByIdAsync(Int32.Parse(value["id"]));
            var newData = new User
            {
                Id = Int32.Parse(value["id"]),
                Username = value["username"],
                Password = user.Password,
                AccessRoleId = Int16.Parse(value["accessRoleId"])
            };
            await _userService.UpdateUserAsync(newData);

            var role = _accessRoleService.GetRoleById(Int16.Parse(value["accessRoleId"]));

            UserModel newUser = new UserModel
            {
                Id = newData.Id,
                Username = newData.Username,
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
