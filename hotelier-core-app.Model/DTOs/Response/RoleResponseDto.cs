namespace hotelier_core_app.Model.DTOs.Response;

public class RoleResponseDto
{
    public long Id { get; set; }
    public string RoleName { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreationDate { get; set; }
}