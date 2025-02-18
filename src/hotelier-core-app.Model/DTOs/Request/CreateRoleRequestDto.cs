using System.ComponentModel.DataAnnotations;

namespace hotelier_core_app.Model.DTOs.Request;

public class CreateRoleRequestDTO
{
    [Required, StringLength(255)]
    public string RoleName { get; set; }
    public long? TenantId { get; set; }
}