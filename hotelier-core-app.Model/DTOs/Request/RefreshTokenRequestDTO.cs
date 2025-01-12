using System.ComponentModel.DataAnnotations;

namespace hotelier_core_app.Model.DTOs.Request
{
    public class RefreshTokenRequestDTO
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
