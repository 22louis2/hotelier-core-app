using hotelier_core_app.Model.DTOs.Request;
using hotelier_core_app.Model.DTOs.Response;
using hotelier_core_app.Model.Entities;

namespace hotelier_core_app.Service.Interface
{
    public interface IPolicyGroupService : IAutoDependencyService
    {
        Task<BaseResponse> AddPolicyGroup(AddPolicyGroupDTO request, AuditLog auditLog);
        Task<BaseResponse> UpdatePolicyGroup(UpdatePolicyGroupDTO request, AuditLog auditLog);
        // delete policy group (soft or not?)
        Task<BaseResponse> AddUserToPolicyGroup(long userId, long policyGroupId, AuditLog auditLog);
        Task<BaseResponse> RemoveUserFromPolicyGroup(long userId, long policyGroupId, AuditLog auditLog);
        Task<BaseResponse> AddPermissionToPolicyGroup(long policyGroupId, long moduleGroupId, long permissionId, AuditLog auditLog);
        Task<BaseResponse> RemovePermissionFromPolicyGroup(long policyGroupId, long moduleGroupId, long permissionId, AuditLog auditLog);
        Task<BaseResponse<List<GetPolicyGroupsResponseDTO>>> GetPolicyGroups(GetPolicyGroupsRequestDTO request);
        Task<BaseResponse<GetPolicyGroupsResponseDTO>> GetSinglePolicyGroup(long id);
    }
}
