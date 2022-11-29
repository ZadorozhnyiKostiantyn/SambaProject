using SambaProject.Data.Models;
using SambaProject.Data.Repository;
using SambaProject.Service.UserManager.Interface;
using Syncfusion.EJ2.FileManager.Base;

namespace SambaProject.Service.UserManager.Services
{
    public class AccessRuleService : IAccessRuleService
    {
        private readonly IRepository<AccessRuleRoles> _accessRuleRepository;
        private readonly IRepository<AccessRole> _accessRoleRepository;


        public AccessRuleService(
            IRepository<AccessRuleRoles> accessRuleRepository,
            IRepository<AccessRole> accessRoleRepository)
        {
            _accessRuleRepository = accessRuleRepository;
            _accessRoleRepository = accessRoleRepository;
        }

        public List<AccessRule> GetAccessRules()
        {
            List<AccessRule> accessRules = new List<AccessRule>();
            // Get all access rules
            var listRules = _accessRuleRepository.GetAll();

            // Get all access roles
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
    }
}
