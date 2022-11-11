using SambaProject.Data.Repository;
using Syncfusion.EJ2.FileManager.Base;

namespace SambaProject.Service.Administration
{
    public class AccessRuleService : IAccessRuleService
    {
        private readonly IAccessRuleRepository _accessRuleRepository;
        private readonly IAccessRoleRepository _accessRoleRepository;


        public AccessRuleService(
            IAccessRuleRepository accessRuleRepository,
            IAccessRoleRepository accessRoleRepository)
        {
            _accessRuleRepository = accessRuleRepository;
            _accessRoleRepository = accessRoleRepository;
        }

        public List<AccessRule> GetAccessRules()
        {
            List<AccessRule> accessRules = new List<AccessRule>();
            var listRules = _accessRuleRepository.GetAllRule();
            var listRoles = _accessRoleRepository.GetAllAccessRole();
            
            foreach (var rule in listRules)
            {
                var role = listRoles.SingleOrDefault(r => r?.AccessRoleId == rule.AccessRoleId)?.Role;
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
