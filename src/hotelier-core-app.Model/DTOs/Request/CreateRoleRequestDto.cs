using System.ComponentModel.DataAnnotations;

namespace hotelier_core_app.Model.DTOs.Request;

public class CreateRoleRequestDto
{
    [Required, StringLength(256)]
    public string RoleName { get; set; }

    public long? TenantId { get; set; }
}