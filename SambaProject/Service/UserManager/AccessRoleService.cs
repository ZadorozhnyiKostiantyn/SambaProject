using SambaProject.Data.Models;
using SambaProject.Data.Repository;
using SambaProject.Service.Authentication;
using Syncfusion.EJ2.FileManager.Base;

namespace SambaProject.Service.UserManager
{
    public class AccessRoleService : IAccessRoleService
    {
        private readonly IAccessRoleRepository _accessRoleRepository;
        private readonly IAccessRuleService _accessRuleService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtDecodingService _jwtDecodingService;
        public AccessRoleService(
            IAccessRoleRepository accessRoleRepository,
            IAccessRuleService accessRuleService,
            IHttpContextAccessor httpContextAccessor,
            IJwtDecodingService jwtDecodingService)
        {
            _accessRoleRepository = accessRoleRepository;
            _accessRuleService = accessRuleService;
            _httpContextAccessor = httpContextAccessor;
            _jwtDecodingService = jwtDecodingService;
        }

        public AccessDetails GetAccessDetails()
        {
            return new AccessDetails 
            { 
                AccessRules = _accessRuleService.GetAccessRules(),
                Role = _jwtDecodingService.DecodeToken(_httpContextAccessor.HttpContext.Session.GetString("Token")).AccessRole
            };
        }

        public List<AccessRole> GetAllRoles()
        {
            return _accessRoleRepository.GetAllAccessRole();
        }

        public AccessRole? GetRoleById(int id)
        {
            return _accessRoleRepository.GetAccessRoleById(id);
        }
    }
}
