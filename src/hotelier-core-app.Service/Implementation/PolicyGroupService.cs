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

        public async Task<BaseResponse<List<PermissionDTO>>> GetAllPermission()
        {
            var permissions = (await _permissionQueryRepository.GetAllAsync()).ToList();
            List<PermissionDTO> permissionsDTO = _mapper.Map<List<PermissionDTO>>(permissions);

            return BaseResponse<List<PermissionDTO>>.Success(permissionsDTO, ResponseMessages.OperationSuccessful, 
                ResponseStatusCode.OperationSuccessful);
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
            if (currentUser == null) return BaseResponse.Failure(ResponseMessages.UserDoesNotExist, ResponseStatusCode.UserDoesNotExist);

            policyGroup = new PolicyGroup();
            policyGroup.Name = request.Name;
            policyGroup.Description = request.Description;
            policyGroup.TenantId = request.TenantId;
            policyGroup.CreatedBy = auditLog.PerformedBy;
            policyGroup.CreationDate = DateTime.UtcNow;

            _auditLogCommandRepository.Add(auditLog);
            _policyGroupCommandRepository.Add(policyGroup);
            _policyGroupCommandRepository.Save();

            return BaseResponse.Success(ResponseMessages.OperationSuccessful,
                ResponseStatusCode.OperationSuccessful);
        }

        public async Task<BaseResponse> UpdatePolicyGroup(UpdatePolicyGroupDTO request, AuditLog auditLog)
        {
            PolicyGroup policyGroup = await _policyGroupQueryRepository.FindAsync(request.Id);
            if (policyGroup == null) return BaseResponse.Failure(ResponseMessages.PolicyGroupDoesNotExist, ResponseStatusCode.PolicyGroupDoesNotExist);
            
            policyGroup.Name = request.Name;
            policyGroup.Description = request.Description;
            policyGroup.TenantId= request.TenantId;
            policyGroup.ModifiedBy = auditLog.PerformerEmail;
            policyGroup.LastModifiedDate = DateTime.UtcNow;

            _auditLogCommandRepository.Add(auditLog);
            _policyGroupCommandRepository.Update(policyGroup);
            _policyGroupCommandRepository.Save();

            return BaseResponse.Success(ResponseMessages.OperationSuccessful,
                ResponseStatusCode.OperationSuccessful);
        }

        public async Task<BaseResponse> AddUserToPolicyGroup(AddUserToPolicyGroupDTO request, AuditLog auditLog)
        {
            PolicyGroup policyGroup = await _policyGroupQueryRepository.FindAsync(request.PolicyGroupId);
            if (policyGroup == null) return BaseResponse.Failure(ResponseMessages.PolicyGroupDoesNotExist, ResponseStatusCode.PolicyGroupDoesNotExist);

            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) return BaseResponse.Failure(ResponseMessages.UserDoesNotExist, ResponseStatusCode.UserDoesNotExist);

            var userPolicy = new ApplicationUserPolicyGroup();
            userPolicy.UserId = request.UserId;
            userPolicy.PolicyGroupId = request.PolicyGroupId;
            userPolicy.CreatedBy = auditLog.PerformerEmail;
            userPolicy.CreationDate = DateTime.UtcNow;

            _auditLogCommandRepository.Add(auditLog);
            _userPolicyCommandRepository.Add(userPolicy);
            await _userPolicyCommandRepository.SaveAsync();

            return BaseResponse.Success(ResponseMessages.OperationSuccessful, ResponseStatusCode.OperationSuccessful);
        }

        public async Task<BaseResponse> RemoveUserFromPolicyGroup(long userId, long policyGroupId, AuditLog auditLog)
        {
            var userPolicy = await _userPolicyQueryRepository.GetByDefaultAsync(u => u.UserId == userId && u.PolicyGroupId == policyGroupId);
            if(userPolicy == null) return BaseResponse.Failure(ResponseMessages.UserNotInPolicyGroup, ResponseStatusCode.UserNotInPolicyGroup);

            _userPolicyCommandRepository.Delete(userPolicy);
            _auditLogCommandRepository.Add(auditLog);
            _auditLogCommandRepository.Save();

            return BaseResponse.Success(ResponseMessages.OperationSuccessful, ResponseStatusCode.OperationSuccessful);
        }

        public async Task<BaseResponse> AddPolicyToPolicyGroup(AddPolicyToPolicyGroupDTO request, AuditLog auditLog)
        {
            PolicyGroup policyGroup = await _policyGroupQueryRepository.FindAsync(request.PolicyGroupId);
            if (policyGroup == null) return BaseResponse.Failure(ResponseMessages.PolicyGroupDoesNotExist, ResponseStatusCode.PolicyGroupDoesNotExist);

            var permission = await _permissionQueryRepository.FindAsync(request.PermissionId);
            if(permission == null) return BaseResponse.Failure(ResponseMessages.PermissionDoesNotExist, ResponseStatusCode.PermissionDoesNotExist);

            var moduleGroup = await _moduleGroupQueryRepository.FindAsync(request.ModuleGroupId);
            if (moduleGroup == null) return BaseResponse.Failure(ResponseMessages.ModuleGroupNotExist, ResponseStatusCode.ModuleGroupNotExist);

            var pmp = new PolicyModulePermission();
            pmp.PermissionId = request.PermissionId;
            pmp.PolicyGroupId = request.PolicyGroupId;
            pmp.ModuleGroupId = request.ModuleGroupId;
            pmp.CreatedBy = auditLog.PerformerEmail;
            pmp.CreationDate = DateTime.UtcNow;

            _auditLogCommandRepository.Add(auditLog);
            _pmpCommandRepository.Add(pmp);
            _pmpCommandRepository.Save();

            return BaseResponse.Success(ResponseMessages.OperationSuccessful, ResponseStatusCode.OperationSuccessful);
        }

        public async Task<BaseResponse> RemovePolicyFromPolicyGroup(long policyGroupId, long policy, AuditLog auditLog)
        {
            var pmp = await _pmpQueryRepository.GetByDefaultAsync(p => p.Id == policy && p.PolicyGroupId == policyGroupId);
            if (pmp == null) return BaseResponse.Failure(ResponseMessages.PolicyDoesNotExist, ResponseStatusCode.PolicyDoesNotExist);

            _auditLogCommandRepository.Add(auditLog);
            _pmpCommandRepository.Delete(pmp);
            _pmpCommandRepository.Save();

            return BaseResponse.Success(ResponseMessages.OperationSuccessful, ResponseStatusCode.OperationSuccessful);
        }

        public async Task<BaseResponse<List<GetPolicyGroupResponseDTO>>> GetPolicyGroups(GetPolicyGroupsRequestDTO request)
        {
            var policyGroups = _policyGroupQueryRepository.GetAllIncluding(p => p.ModulePermissions).Where(p => p.TenantId == request.TenantId);
            var response = _mapper.Map<List<GetPolicyGroupResponseDTO>>(policyGroups.ToList());
            return BaseResponse<List<GetPolicyGroupResponseDTO>>.Success(response, ResponseMessages.OperationSuccessful,
                ResponseStatusCode.OperationSuccessful);
        }

        public async Task<BaseResponse<GetPolicyGroupResponseDTO>> GetSinglePolicyGroup(long id)
        {
            PolicyGroup policyGroup = await _policyGroupQueryRepository.FindAsync(id);
            if (policyGroup == null) return BaseResponse<GetPolicyGroupResponseDTO>.Failure(null, ResponseMessages.PolicyGroupDoesNotExist, ResponseStatusCode.PolicyGroupDoesNotExist);
            return BaseResponse<GetPolicyGroupResponseDTO>.Success(_mapper.Map<GetPolicyGroupResponseDTO>(policyGroup), ResponseMessages.OperationSuccessful,
                ResponseStatusCode.OperationSuccessful);
        }
    }
}
