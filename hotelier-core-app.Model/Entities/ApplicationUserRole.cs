using hotelier_core_app.Model.Attributes;
using hotelier_core_app.Model.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotelier_core_app.Model.Entities
{
    [Table("UserRole")]
    [TableName("UserRole")]
    [Serializable]
    public class ApplicationUserRole : IdentityUserRole<long>, IBaseEntity
    {
        public long Id { get; set; }

        [StringLength(200)]
        public string CreatedBy { get; set; }

        [StringLength(200)]
        public string ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [ForeignKey("RoleId")]
        public ApplicationRole Role { get; set; }
        public ApplicationUserRole() { }
    }
}
