using hotelier_core_app.Model.Attributes;
using hotelier_core_app.Model.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotelier_core_app.Model.Entities
{
    [Table("PolicyGroup")]
    [TableName("PolicyGroup")]
    [Serializable]
    public class PolicyGroup : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(200)]
        public string CreatedBy { get; set; }

        [StringLength(200)]
        public string? ModifiedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("Tenant")]
        public long TenantId { get; set; }
        public Tenant Tenant { get; set; }

        public ICollection<PolicyModulePermission> ModulePermissions { get; set; } = new List<PolicyModulePermission>();
        public ICollection<ApplicationUserPolicyGroup> UserPolicyGroups { get; set; } = new List<ApplicationUserPolicyGroup>();

    }
}
