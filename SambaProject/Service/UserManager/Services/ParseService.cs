using SambaProject.Data.Models;
using SambaProject.Data.Repository;
using SambaProject.Models;
using SambaProject.Service.UserManager.Interface;
using Syncfusion.EJ2.FileManager.Base;

namespace SambaProject.Service.UserManager.Services
{
    public class ParseService : IParseService
    {
        private readonly IRepository<AccessRole> _accessRoleRepository;

        public ParseService(IRepository<AccessRole> accessRoleService)
        {
            _accessRoleRepository = accessRoleService;
        }

        public List<AccessRule> ParseAccessRuleRolesToAccessRule(List<AccessRuleRoles> listRules)
        {
            List<AccessRule> accessRules = new List<AccessRule>();
            var listRoles = _accessRoleRepository.GetAll();

            foreach (var rule in listRules)
            {
                var role = listRoles.SingleOrDefault(r => r?.Id == rule.AccessRoleId)?.Role;
                accessRules.Add(
                    new AccessRule
                    {
                        Copy = rule.Copy,
                        Download = rule.Download,
                        Write = rule.Write,
                        Path = rule.Path,
                        Read = rule.Read,
                        Role = role,
                        WriteContents = rule.WriteContents,
                        Upload = rule.Upload,
                        IsFile = rule.IsFile

                    });
            }

            return accessRules;
        }

        public List<UserModel> ParseUserToUserModel(List<User> users)
        {
            List<UserModel> result = new List<UserModel>();

            foreach (var user in users)
            {
                result.Add(
                    new UserModel
                    {
                        Id = user.Id,
                        Username = user.Username,
                        AccessRole = _accessRoleRepository.GetById(user.AccessRoleId).Role
                    }
                );
            }

            return result;
        }

        public UserModel ParseUserToUserModel(User user)
        {
            return new UserModel
            {
                Id = user.Id,
                Username = user.Username,
                AccessRole = _accessRoleRepository.GetById(user.AccessRoleId).Role
            };
        }
    }
}
