namespace hotelier_core_app.Model.DTOs.Response;

public class SubscriptionPlanResponseDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public long? DiscountId { get; set; }
    
    public DateTime? CreationDate { get; set; }

    public DateTime? LastModifiedDate { get; set; }

    public bool IsDeleted { get; set; }
}