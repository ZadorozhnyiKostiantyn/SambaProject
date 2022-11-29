using SambaProject.Data.Models;
using SambaProject.Data.Repository;
using SambaProject.Service.Authentication.Interface;
using SambaProject.Service.UserManager.Interface;
using Syncfusion.EJ2.FileManager.Base;

namespace SambaProject.Service.UserManager.Services
{
    public class AccessRoleService : IAccessRoleService
    {
        private readonly IRepository<AccessRole> _accessRoleRepository;
        private readonly IAccessRuleService _accessRuleService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtDecodingService _jwtDecodingService;
        public AccessRoleService(
            IRepository<AccessRole> accessRoleRepository,
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
            return _accessRoleRepository.GetAll();
        }

        public AccessRole? GetRoleById(int id)
        {
            return _accessRoleRepository.GetById(id);
        }
    }
}
