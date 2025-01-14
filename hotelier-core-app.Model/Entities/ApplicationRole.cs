using hotelier_core_app.Model.Attributes;
using hotelier_core_app.Model.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotelier_core_app.Model.Entities
{
    [Table("Role")]
    [TableName("Role")]
    [Serializable]
    public class ApplicationRole : IdentityRole<long>, IBaseEntity
    {
        [StringLength(200)]
        public string CreatedBy { get; set; }

        [StringLength(200)]
        public string? ModifiedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("Tenant")]
        public long? TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public List<RolePermission>? RolePermissions { get; set; }
        public ApplicationRole() { }
    }
}
