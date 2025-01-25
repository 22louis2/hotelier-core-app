using hotelier_core_app.Model.DTOs.Request;
using hotelier_core_app.Model.DTOs.Response;
using hotelier_core_app.Model.Entities;

namespace hotelier_core_app.Service.Interface
{
    public interface IRoleService: IAutoDependencyService
    {
        Task<BaseResponse> CreateRoleAsync(CreateRoleRequestDto request, AuditLog auditLog);
        Task<BaseResponse<RoleResponseDto>> GetRoleByIdAsync(long roleId);
        Task<PageBaseResponse<List<RoleResponseDto>>> GetAllRolesAsync(GetRolesInputDTO input);
        Task<BaseResponse> UpdateRoleAsync(UpdateRoleRequestDto request, AuditLog auditLog);
        Task<BaseResponse> DeleteRoleAsync(long roleId, AuditLog auditLog);
    }
}