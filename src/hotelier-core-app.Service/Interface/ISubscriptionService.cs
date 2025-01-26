using hotelier_core_app.Model.DTOs.Request;
using hotelier_core_app.Model.DTOs.Response;

namespace hotelier_core_app.Service.Interface;

public interface ISubscriptionService : IAutoDependencyService
{
    Task<BaseResponse> CreateSubscriptionPlanAsync(CreateSubscriptionPlanDto request);
    Task<BaseResponse<SubscriptionPlanResponseDto>> GetSubscriptionPlanByIdAsync(long id);
    Task<List<SubscriptionPlanResponseDto>> GetAllSubscriptionPlansAsync();
    Task<BaseResponse> AssignSubscriptionPlanToTenantAsync(AssignSubscriptionPlanDto request);
    Task<BaseResponse> DeleteSubscriptionPlanAsync(long id);
}