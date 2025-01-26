using System.ComponentModel.DataAnnotations;
using hotelier_core_app.Core.Enums;

namespace hotelier_core_app.Model.DTOs.Request;

public class AssignSubscriptionPlanDto
{
    [Required]
    public long TenantId { get; set; }

    [Required]
    [EnumDataType(typeof(Subscription), ErrorMessage = "Invalid subscription plan.")]
    public Subscription SubscriptionPlan { get; set; }
}