using SambaProject.Data.Models;
using SambaProject.Data.Repository;
using Syncfusion.EJ2.FileManager.Base;

namespace SambaProject.Service.UserManager
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
            var listRules = _accessRuleRepository.GetAll();
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
