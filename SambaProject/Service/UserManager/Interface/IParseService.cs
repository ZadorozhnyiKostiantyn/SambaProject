using SambaProject.Data.Models;
using SambaProject.Models;
using Syncfusion.EJ2.FileManager.Base;

namespace SambaProject.Service.UserManager.Interface
{
    public interface IParseService
    {
        public List<UserModel> ParseUserToUserModel(List<User> users);
        public UserModel ParseUserToUserModel(User user);
        public List<AccessRule> ParseAccessRuleRolesToAccessRule(List<AccessRuleRoles> listRules);
    }
}
