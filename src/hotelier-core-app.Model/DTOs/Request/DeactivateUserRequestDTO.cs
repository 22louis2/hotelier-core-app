using hotelier_core_app.Core.Enums;

namespace hotelier_core_app.Model.DTOs.Request
{
    public class DeactivateUserRequestDTO : ActivateUserRequestDTO
    {
        public UserStatus Status { get; set; }
    }
}
