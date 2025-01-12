using hotelier_core_app.Core.Enums;

namespace hotelier_core_app.Model.DTOs.Request
{
    public class ActivateUserRequestDTO
    {
        public string Email {  get; set; }
        public UserRole Role { get; set; }
    }
}
