using hotelier_core_app.Model.Interfaces;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using hotelier_core_app.Model.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotelier_core_app.Model.Entities
{
    [Table("User")]
    [TableName("User")]
    [Serializable]
    public class ApplicationUser : IdentityUser<long>, IBaseEntity
    {
        public bool IsActive { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [StringLength(200)]
        public string FullName { get; set; }

        [StringLength(200)]
        public string Status { get; set; }

        [StringLength(200)]
        public string CreatedBy { get; set; }

        [StringLength(200)]
        public string ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        [StringLength(200)]
        public string RefreshToken { get; set; }

        public ApplicationUserRole UserRole { get; set; }

        public ApplicationUser()
        {
            CreationDate = DateTime.UtcNow;
            IsDeleted = false;
            IsActive = true;
            Status = "Active";
        }
    }
}
