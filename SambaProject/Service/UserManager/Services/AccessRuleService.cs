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
        private readonly IParseService _parseService;


        public AccessRuleService(
            IRepository<AccessRuleRoles> accessRuleRepository,
            IRepository<AccessRole> accessRoleRepository,
            IParseService parseService)
        {
            _accessRuleRepository = accessRuleRepository;
            _accessRoleRepository = accessRoleRepository;
            _parseService = parseService;
        }

        public List<AccessRule> GetAccessRules()
        {
            return _parseService.
                ParseAccessRuleRolesToAccessRule(_accessRuleRepository.GetAll());

        }
    }
}
