using AutoMapper;
using hotelier_core_app.Core.Constants;
using hotelier_core_app.Domain.Commands.Interface;
using hotelier_core_app.Domain.Queries.Interface;
using hotelier_core_app.Model.DTOs.Request;
using hotelier_core_app.Model.DTOs.Response;
using hotelier_core_app.Model.Entities;
using hotelier_core_app.Service.Interface;

namespace hotelier_core_app.Service.Implementation;

public class RoleService : IRoleService
{
    private readonly IDBCommandRepository<ApplicationRole> _roleCommandRepository;
    private readonly IDBQueryRepository<ApplicationRole> _roleQueryRepository;
    private readonly IDBCommandRepository<AuditLog> _auditLogCommandRepository;
    private readonly IMapper _mapper;
    
    public RoleService(
        IDBCommandRepository<ApplicationRole> roleCommandRepository, 
        IDBQueryRepository<ApplicationRole> roleQueryRepository, 
        IDBCommandRepository<AuditLog> auditLogCommandRepository, 
        IMapper mapper)
    {
        this._roleCommandRepository = roleCommandRepository;
        this._roleQueryRepository = roleQueryRepository;
        this._auditLogCommandRepository = auditLogCommandRepository;
        this._mapper = mapper;
    }
    public async Task<BaseResponse> CreateRoleAsync(CreateRoleRequestDTO request, AuditLog auditLog)
    {
        var existingRole = await _roleQueryRepository.GetByDefaultAsync(r => r.Name == request.RoleName && r.IsDeleted == false);
        if (existingRole != null) return BaseResponse.Failure(ResponseMessages.RoleExist);
        
        var role = _mapper.Map<ApplicationRole>(request);
        role.Name = request.RoleName;
        role.CreationDate = DateTime.UtcNow;
        role.CreatedBy = auditLog.PerformedBy;
        await _roleCommandRepository.AddAsync(role);
        await _roleCommandRepository.SaveAsync();
        await _auditLogCommandRepository.AddAsync(auditLog);
        await _auditLogCommandRepository.SaveAsync();

        return BaseResponse.Success("Role created successfully.");
    }

    public async Task<BaseResponse> UpdateRoleAsync(UpdateRoleRequestDTO request, AuditLog auditLog)
    {
        var role = await _roleQueryRepository.FindAsync(request.Id);
        if (role == null) return BaseResponse.Failure(ResponseMessages.RoleNotExist);

        role.Name = request.RoleName;
        role.LastModifiedDate = DateTime.UtcNow;
        role.ModifiedBy = auditLog.PerformedBy;
        await _roleCommandRepository.UpdateAsync(role);
        await _auditLogCommandRepository.AddAsync(auditLog);
        await _auditLogCommandRepository.SaveAsync();

        return BaseResponse.Success("Role updated successfully.");
    }
    
    public async Task<BaseResponse<RoleResponseDTO>> GetRoleByIdAsync(long id)
    {
        var role = await _roleQueryRepository.FindAsync(id);
        if (role == null) return BaseResponse<RoleResponseDTO>.Failure(new RoleResponseDTO(), ResponseMessages.RoleNotExist);

        var response = _mapper.Map<RoleResponseDTO>(role);
        return BaseResponse<RoleResponseDTO>.Success(response);
    }
    
    public async Task<BaseResponse<List<RoleResponseDTO>>> GetAllRolesAsync()
    {
        var roles = await _roleQueryRepository.GetAllAsync();
        var response = _mapper.Map<List<RoleResponseDTO>>(roles);
        return BaseResponse<List<RoleResponseDTO>>.Success(response);
    }
    
    public async Task<BaseResponse> DeleteRoleAsync(long id, AuditLog auditLog)
    {
        var role = await _roleQueryRepository.FindAsync(id);
        if (role == null) return BaseResponse.Failure(ResponseMessages.RoleNotExist);

        role.IsDeleted = true;
        role.LastModifiedDate = DateTime.UtcNow;
        await _roleCommandRepository.UpdateAsync(role);
        await _auditLogCommandRepository.AddAsync(auditLog);
        await _auditLogCommandRepository.SaveAsync();

        return BaseResponse.Success("Role removed successfully.");
    }
}
