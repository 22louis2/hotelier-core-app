using System.ComponentModel.DataAnnotations;

namespace hotelier_core_app.Model.DTOs.Request;

public class CreateSubscriptionPlanDto
{
    [Required, StringLength(50)]
    public string Name { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    [Required]
    public decimal Price { get; set; }

    public long? DiscountId { get; set; }
}