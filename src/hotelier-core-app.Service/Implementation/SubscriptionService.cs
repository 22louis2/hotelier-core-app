using AutoMapper;
using hotelier_core_app.Core.Constants;
using hotelier_core_app.Core.Enums;
using hotelier_core_app.Domain.Commands.Interface;
using hotelier_core_app.Domain.Queries.Interface;
using hotelier_core_app.Model.DTOs.Request;
using hotelier_core_app.Model.DTOs.Response;
using hotelier_core_app.Model.Entities;
using hotelier_core_app.Service.Interface;

namespace hotelier_core_app.Service.Implementation;

public class SubscriptionService(
    IDBCommandRepository<SubscriptionPlan> planCommandRepository,
    IDBQueryRepository<SubscriptionPlan> planQueryRepository,
    IDBQueryRepository<Tenant> tenantQueryRepository,
    IDBCommandRepository<Tenant> tenantCommandRepository,
    IMapper mapper)
    : ISubscriptionService
{
    public async Task<BaseResponse> CreateSubscriptionPlanAsync(CreateSubscriptionPlanDto request)
    {
        var existingPlan = await planQueryRepository.GetByDefaultAsync(p => p.Name == request.Name);
        if (existingPlan != null)
            return BaseResponse.Failure($"{ResponseMessages.SubscriptionExist}': ' {request.Name}");

        var plan = mapper.Map<SubscriptionPlan>(request);
        plan.CreationDate = DateTime.UtcNow;
        await planCommandRepository.AddAsync(plan);

        return BaseResponse.Success(ResponseMessages.SubscriptionCreated);
    }
    
    public async Task<BaseResponse<SubscriptionPlanResponseDto>> GetSubscriptionPlanByIdAsync(long id)
    {
        var plan = await planQueryRepository.FindAsync(id);
        if (plan == null)
            return BaseResponse<SubscriptionPlanResponseDto>.Failure(null, ResponseMessages.SubscriptionNotExist);

        var response = mapper.Map<SubscriptionPlanResponseDto>(plan);
        return BaseResponse<SubscriptionPlanResponseDto>.Success(response);
    }
    
    public async Task<List<SubscriptionPlanResponseDto>> GetAllSubscriptionPlansAsync()
    {
        var plans = await planQueryRepository.GetAllAsync();
        return mapper.Map<List<SubscriptionPlanResponseDto>>(plans);
    }

    public async Task<BaseResponse> DeleteSubscriptionPlanAsync(long id)
    {
        var plan = await planQueryRepository.FindAsync(id);
        if (plan == null)
            return BaseResponse.Failure("Subscription plan not found.");

        // Soft delete
        plan.IsDeleted = true;
        plan.LastModifiedDate = DateTime.UtcNow;
        await planCommandRepository.UpdateAsync(plan);

        return BaseResponse.Success("Subscription plan deleted successfully.");
    }

    
    public async Task<BaseResponse> AssignSubscriptionPlanToTenantAsync(AssignSubscriptionPlanDto request)
    {
        var tenant = await tenantQueryRepository.FindAsync(request.TenantId);
        if (tenant == null)
            return BaseResponse.Failure($"Tenant with ID {request.TenantId} not found.");

        // Check if the subscription plan exists in the database
        var plan = await planQueryRepository.GetByDefaultAsync(p => p.Name == request.SubscriptionPlan.ToString());
        if (plan == null)
            return BaseResponse.Failure($"Subscription plan '{request.SubscriptionPlan}' not found in the database.");

        // Assign the subscription plan to the tenant
        tenant.SubscriptionPlanId = plan.Id;
        tenant.SubscriptionStartDate = DateTime.UtcNow;

        // Example logic for subscription duration
        tenant.SubscriptionEndDate = request.SubscriptionPlan switch
        {
            Subscription.Free => DateTime.UtcNow.AddMonths(1), // 1-month trial
            Subscription.Basic => DateTime.UtcNow.AddMonths(6), // 3 months
            Subscription.Standard => DateTime.UtcNow.AddYears(1), // 6 months
            Subscription.Premium => DateTime.UtcNow.AddYears(1), // 1 year
            // _ => throw new ArgumentOutOfRangeException(nameof(request.SubscriptionPlan), "Invalid subscription plan.")
        };

        await tenantCommandRepository.UpdateAsync(tenant);

        return BaseResponse.Success($"Subscription plan '{request.SubscriptionPlan}' assigned to tenant successfully.");
    }

}