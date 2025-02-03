using AutoMapper;
using hotelier_core_app.Core.Constants;
using hotelier_core_app.Domain.Commands.Interface;
using hotelier_core_app.Domain.Queries.Interface;
using hotelier_core_app.Model.DTOs.Request;
using hotelier_core_app.Model.DTOs.Response;
using hotelier_core_app.Model.Entities;
using hotelier_core_app.Service.Interface;
using Microsoft.AspNetCore.Identity;

namespace hotelier_core_app.Service.Implementation
{
    public class PolicyGroupService : IPolicyGroupService
    {
        private readonly IDBCommandRepository<PolicyGroup> _policyGroupCommandRepository;
        private readonly IDBQueryRepository<PolicyGroup> _policyGroupQueryRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDBCommandRepository<AuditLog> _auditLogCommandRepository;
        private readonly IDBCommandRepository<ApplicationUserPolicyGroup> _userPolicyCommandRepository;
        private readonly IDBQueryRepository<ApplicationUserPolicyGroup> _userPolicyQueryRepository;
        private readonly IDBQueryRepository<ModuleGroup> _moduleGroupQueryRepository;
        private readonly IDBQueryRepository<Permission> _permissionQueryRepository;
        private readonly IDBQueryRepository<PolicyModulePermission> _pmpQueryRepository;
        private readonly IDBCommandRepository<PolicyModulePermission> _pmpCommandRepository;
        private readonly IMapper _mapper;

        public PolicyGroupService(IDBCommandRepository<PolicyGroup> policyGroupCommandRepository,
            IDBQueryRepository<PolicyGroup> policyGroupQueryRepository,
            UserManager<ApplicationUser> userManager,
            IDBCommandRepository<AuditLog> auditLogCommandRepository,
            IDBCommandRepository<ApplicationUserPolicyGroup> userPolicyCommandRepository,
            IDBQueryRepository<ApplicationUserPolicyGroup> userPolicyQueryRepository,
            IDBQueryRepository<ModuleGroup> moduleGroupQueryRepository,
            IDBQueryRepository<Permission> permissionQueryRepository,
            IDBQueryRepository<PolicyModulePermission> pmpQueryRepository,
            IDBCommandRepository<PolicyModulePermission> pmpCommandRepository,
            IMapper mapper)
        {
            _policyGroupCommandRepository = policyGroupCommandRepository;
            _policyGroupQueryRepository = policyGroupQueryRepository;
            _userManager = userManager;
            _auditLogCommandRepository = auditLogCommandRepository;
            _userPolicyCommandRepository = userPolicyCommandRepository;
            _userPolicyQueryRepository = userPolicyQueryRepository;
            _moduleGroupQueryRepository = moduleGroupQueryRepository;
            _permissionQueryRepository = permissionQueryRepository;
            _pmpQueryRepository = pmpQueryRepository;
            _pmpCommandRepository = pmpCommandRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse> AddPolicyGroup(AddPolicyGroupDTO request, AuditLog auditLog)
        {
            // confirm policy with same name does not exist for tenant
            // confirm user can perform action
            var policyGroup = _policyGroupQueryRepository.GetByDefault(p => p.Name == request.Name && p.TenantId == request.TenantId);
            if (policyGroup != null)
            {
                return BaseResponse.Failure(ResponseMessages.PolicyGroupExists, ResponseStatusCode.PolicyGroupExists);
            }

            var currentUser = await _userManager.FindByEmailAsync(auditLog.PerformerEmail);
            if (currentUser == null) return BaseResponse.Failure(ResponseMessages.UserDoesNotExist);

            policyGroup = new PolicyGroup();
            policyGroup.Name = request.Name;
            policyGroup.Description = request.Description;
            policyGroup.TenantId = request.TenantId;
            policyGroup.CreatedBy = auditLog.PerformedBy;
            policyGroup.CreationDate = DateTime.Now;

            _auditLogCommandRepository.Add(auditLog);
            _policyGroupCommandRepository.Add(policyGroup);
            _policyGroupCommandRepository.Save();

            return BaseResponse.Success();
        }

        public async Task<BaseResponse> UpdatePolicyGroup(UpdatePolicyGroupDTO request, AuditLog auditLog)
        {
            PolicyGroup policyGroup = await _policyGroupQueryRepository.FindAsync(request.Id);
            if (policyGroup == null) return BaseResponse.Failure(ResponseMessages.PolicyGroupDoesNotExist);
            
            policyGroup.Name = request.Name;
            policyGroup.Description = request.Description;
            policyGroup.TenantId= request.TenantId;
            policyGroup.ModifiedBy = auditLog.PerformerEmail;
            policyGroup.LastModifiedDate = DateTime.Now;

            _auditLogCommandRepository.Add(auditLog);
            _policyGroupCommandRepository.Update(policyGroup);
            _policyGroupCommandRepository.Save();

            return BaseResponse.Success();
        }

        public async Task<BaseResponse> AddUserToPolicyGroup(long userId, long policyGroupId, AuditLog auditLog)
        {
            PolicyGroup policyGroup = await _policyGroupQueryRepository.FindAsync(policyGroupId);
            if (policyGroup == null) return BaseResponse.Failure(ResponseMessages.PolicyGroupDoesNotExist);

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return BaseResponse.Failure(ResponseMessages.UserDoesNotExist);

            var userPolicy = new ApplicationUserPolicyGroup();
            userPolicy.UserId = userId;
            userPolicy.PolicyGroupId = policyGroupId;
            userPolicy.CreatedBy = auditLog.PerformerEmail;
            userPolicy.CreationDate = DateTime.Now;

            _auditLogCommandRepository.Add(auditLog);
            _userPolicyCommandRepository.Add(userPolicy);

            return BaseResponse.Success();
        }

        public async Task<BaseResponse> RemoveUserFromPolicyGroup(long userId, long policyGroupId, AuditLog auditLog)
        {
            var userPolicy = await _userPolicyQueryRepository.GetByDefaultAsync(u => u.UserId == userId && u.PolicyGroupId == policyGroupId);
            if(userPolicy == null) return BaseResponse.Failure(ResponseMessages.UserNotInPolicyGroup);

            _userPolicyCommandRepository.Delete(userPolicy);
            _auditLogCommandRepository.Add(auditLog);
            _auditLogCommandRepository.Save();

            return BaseResponse.Success();
        }

        public async Task<BaseResponse> AddPermissionToPolicyGroup(long policyGroupId, long moduleGroupId, long permissionId, AuditLog auditLog)
        {
            PolicyGroup policyGroup = await _policyGroupQueryRepository.FindAsync(policyGroupId);
            if (policyGroup == null) return BaseResponse.Failure(ResponseMessages.PolicyGroupDoesNotExist);

            var moduleGroup = _moduleGroupQueryRepository.FindAsync(moduleGroupId);
            if(moduleGroup == null) return BaseResponse.Failure(ResponseMessages.ModuleGroupNotExist);

            var permission = _permissionQueryRepository.FindAsync(permissionId);
            if(permission == null) return BaseResponse.Failure(ResponseMessages.PermissionDoesNotExist);

            var pmp = new PolicyModulePermission();
            pmp.PermissionId = permissionId;
            pmp.PolicyGroupId = policyGroupId;
            pmp.ModuleGroupId = moduleGroupId;
            pmp.CreatedBy = auditLog.PerformerEmail;
            pmp.CreationDate = DateTime.Now;

            _auditLogCommandRepository.Add(auditLog);
            _pmpCommandRepository.Add(pmp);
            _pmpCommandRepository.Save();

            return BaseResponse.Success();
        }

        public async Task<BaseResponse> RemovePermissionFromPolicyGroup(long policyGroupId, long moduleGroupId, long permissionId, AuditLog auditLog)
        {
            var pmp = _pmpQueryRepository.GetByDefaultAsync(p => p.PermissionId == permissionId && p.PolicyGroupId == policyGroupId && p.ModuleGroupId == moduleGroupId);
            if (pmp == null) return BaseResponse.Failure(ResponseMessages.PermissionDoesNotExist);

            _auditLogCommandRepository.Add(auditLog);
            _pmpCommandRepository.Delete(pmp);
            _pmpCommandRepository.Save();

            return BaseResponse.Success();
        }

        public async Task<BaseResponse<List<GetPolicyGroupsResponseDTO>>> GetPolicyGroups(GetPolicyGroupsRequestDTO request)
        {
            var policyGroups = _policyGroupQueryRepository.GetAllIncluding(p => p.TenantId == request.TenantId, p => p.ModulePermissions);
            var response = _mapper.Map<List<GetPolicyGroupsResponseDTO>>(policyGroups.ToList());
            return BaseResponse<List<GetPolicyGroupsResponseDTO>>.Success(response);
        }

        public async Task<BaseResponse<GetPolicyGroupsResponseDTO>> GetSinglePolicyGroup(long id)
        {
            PolicyGroup policyGroup = await _policyGroupQueryRepository.FindAsync(id);
            if (policyGroup == null) return BaseResponse<GetPolicyGroupsResponseDTO>.Failure(null, ResponseMessages.PolicyGroupDoesNotExist);
            return BaseResponse<GetPolicyGroupsResponseDTO>.Success(_mapper.Map<GetPolicyGroupsResponseDTO>(policyGroup));
        }
    }
}
