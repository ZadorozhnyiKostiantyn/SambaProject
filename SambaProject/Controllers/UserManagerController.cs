using Microsoft.AspNetCore.Mvc;
using SambaProject.Data.Models;
using SambaProject.Attributes;
using SambaProject.Error;
using SambaProject.Service.Authentication.Interface;
using SambaProject.Service.UserManager.Interface;
using SambaProject.Models.ViewModel;

namespace SambaProject.Controllers
{
    [AuthorizeUser]
    [AccessRole("access_role", "Owner")]
    public class UserManagerController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly IAccessRoleService _accessRoleService;
        private readonly IParseService _parseService;

        public UserManagerController(
            IAuthenticationService authenticationService,
            IUserService userService,
            IAccessRoleService accessRoleService,
            IParseService parseService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
            _accessRoleService = accessRoleService;
            _parseService = parseService;
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
                    user = _parseService.ParseUserToUserModel(newUser),
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
            var user = await _userService.GetUserByIdAsync(Int32.Parse(value["id"]));
            //var role = _accessRoleService.GetRoleById(Int32.Parse(value["accessRoleId"]));

            var newData = await _userService.UpdateUserAsync(
                new User
                {
                    Id = Int32.Parse(value["id"]),
                    Username = value["username"],
                    Password = user.Password,
                    AccessRoleId = Int32.Parse(value["accessRoleId"])
                }
            );

            return Json(_parseService.ParseUserToUserModel(newData));
        }

        // DELETE: Users/Delete/5
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleteResult = await _userService.DeleteUserAsync(id);

            if(deleteResult.IsT1)
            {
                return Json(
                    new
                    {
                        editUser = true,
                        deleteId = id,
                        data = deleteResult.AsT1
                    });
            }

            return Json(new { editUser = false, id = deleteResult.AsT0 });
        }

        [HttpGet]
        public IActionResult GetAccessRoles()
        {
            return Json(_accessRoleService.GetAllRoles());
        }
    }
}
