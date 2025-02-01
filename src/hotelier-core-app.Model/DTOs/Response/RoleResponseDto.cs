namespace hotelier_core_app.Model.DTOs.Response;

public class RoleResponseDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public bool IsDeleted { get; set; }
    public long? TenantId { get; set; }
}