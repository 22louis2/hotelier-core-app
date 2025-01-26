using hotelier_core_app.Model.DTOs.Request;
using hotelier_core_app.Model.DTOs.Response;
using hotelier_core_app.Model.Entities;

namespace hotelier_core_app.Service.Interface;

public interface ISubscriptionService : IAutoDependencyService
{
    Task<BaseResponse> CreateSubscriptionPlanAsync(CreateSubscriptionPlanDto request, AuditLog auditLog);
    Task<BaseResponse<SubscriptionPlanResponseDto>> GetSubscriptionPlanByIdAsync(long id);
    Task<List<SubscriptionPlanResponseDto>> GetAllSubscriptionPlansAsync();
    Task<BaseResponse> AssignSubscriptionPlanToTenantAsync(AssignSubscriptionPlanDto request, AuditLog auditLog);
    Task<BaseResponse> DeleteSubscriptionPlanAsync(long id, AuditLog auditLog);
}