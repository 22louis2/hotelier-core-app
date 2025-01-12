using System.ComponentModel.DataAnnotations;

namespace hotelier_core_app.Model.DTOs.Request
{
    public class EditUserDetailRequestDTO
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public IEnumerable<string> Roles { get; set; }
    }
}
