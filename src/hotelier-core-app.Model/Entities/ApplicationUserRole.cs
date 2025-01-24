using hotelier_core_app.Model.Attributes;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotelier_core_app.Model.Entities
{
    [Table("UserRole")]
    [TableName("UserRole")]
    [Serializable]
    public class ApplicationUserRole : IdentityUserRole<long>
    {
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [ForeignKey("RoleId")]
        public ApplicationRole Role { get; set; }
        public ApplicationUserRole() { }
    }
}
