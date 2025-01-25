using AutoMapper;
using hotelier_core_app.Core.Constants;
using hotelier_core_app.Domain.Commands.Interface;
using hotelier_core_app.Domain.Queries.Interface;
using hotelier_core_app.Model.DTOs.Request;
using hotelier_core_app.Model.DTOs.Response;
using hotelier_core_app.Model.Entities;
using hotelier_core_app.Service.Interface;

namespace hotelier_core_app.Service.Implementation;

public class RoleService(
    IDBCommandRepository<ApplicationRole> roleCommandRepository,
    IDBQueryRepository<ApplicationRole> roleQueryRepository,
    IDBCommandRepository<AuditLog> auditLogCommandRepository,
    IMapper mapper)
    : IRoleService
{
    public async Task<BaseResponse> CreateRoleAsync(CreateRoleRequestDto request, AuditLog auditLog)
    {
        // Check if the role already exists
        var existingRole = await roleQueryRepository.GetByDefaultAsync(r => r.Name == request.RoleName);
        if (existingRole != null) return BaseResponse.Failure(ResponseMessages.RoleExist);

        // Create the role
        var role = mapper.Map<ApplicationRole>(request);
        role.CreationDate = DateTime.UtcNow;
        await roleCommandRepository.AddAsync(role);
        await auditLogCommandRepository.AddAsync(auditLog);

        return BaseResponse.Success("Role created successfully.");
    }

    public async Task<BaseResponse> UpdateRoleAsync(UpdateRoleRequestDto request, AuditLog auditLog)
    {
        // Find the role by ID
        var role = await roleQueryRepository.FindAsync(request.Id);
        if (role == null) return BaseResponse.Failure(ResponseMessages.RoleNotExist);

        // Update the role's name
        role.Name = request.RoleName;
        role.LastModifiedDate = DateTime.UtcNow;
        role.ModifiedBy = request.ModifiedBy;
        await roleCommandRepository.UpdateAsync(role);
        await auditLogCommandRepository.AddAsync(auditLog);

        return BaseResponse.Success("Role updated successfully.");
    }
    
    public async Task<BaseResponse<RoleResponseDto>> GetRoleByIdAsync(long id)
    {
        var role = await roleQueryRepository.FindAsync(id);
        if (role == null) return BaseResponse<RoleResponseDto>.Failure(new RoleResponseDto(), ResponseMessages.RoleNotExist);

        var response = mapper.Map<RoleResponseDto>(role);
        return BaseResponse<RoleResponseDto>.Success(response);
    }
    
    public async Task<PageBaseResponse<List<RoleResponseDto>>> GetAllRolesAsync(GetRolesInputDTO input)
    {
        var roles = await roleQueryRepository.GetAllAsync();
        var applicationRoles = roles.ToList();
        var paginated = applicationRoles.Skip((input.PageNumber - 1) * input.PageSize).Take(input.PageSize).ToList();

        var response = mapper.Map<List<RoleResponseDto>>(paginated);
        return PageBaseResponse<List<RoleResponseDto>>.Success(
            response,
            count: applicationRoles.Count,
            pageSize: input.PageSize,
            pageNumber: input.PageNumber,
            totalPageCount: (int)Math.Ceiling(applicationRoles.Count / (double)input.PageSize)
        );
    }
    
    public async Task<BaseResponse> DeleteRoleAsync(long id, AuditLog auditLog)
    {
        var role = await roleQueryRepository.FindAsync(id);
        if (role == null) return BaseResponse.Failure(ResponseMessages.RoleNotExist);

        role.IsDeleted = true;
        role.LastModifiedDate = DateTime.UtcNow;
        await roleCommandRepository.UpdateAsync(role);
        await auditLogCommandRepository.AddAsync(auditLog);

        return BaseResponse.Success("Role removed successfully.");
    }
}
