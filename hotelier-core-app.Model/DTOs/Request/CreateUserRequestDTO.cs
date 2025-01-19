using hotelier_core_app.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace hotelier_core_app.Model.DTOs.Request
{
    public class CreateUserRequestDTO : UserRequestDTO
    {
        public string HotelName { get; set; }
        public int SubscriptionPlanId { get; set; }
    }

    public class UserRequestDTO
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }
}
