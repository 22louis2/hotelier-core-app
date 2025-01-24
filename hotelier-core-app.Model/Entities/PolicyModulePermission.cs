using hotelier_core_app.Model.Attributes;
using hotelier_core_app.Model.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotelier_core_app.Model.Entities
{
    [Table("PolicyModulePermission")]
    [TableName("PolicyModulePermission")]
    [Serializable]
    public class PolicyModulePermission : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(200)]
        public string CreatedBy { get; set; }

        [StringLength(200)]
        public string? ModifiedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("PolicyGroup")]
        public long PolicyGroupId { get; set; }
        public PolicyGroup PolicyGroup { get; set; }

        [ForeignKey("ModuleGroup")]
        public long ModuleGroupId { get; set; }
        public ModuleGroup ModuleGroup { get; set; }

        [ForeignKey("Permission")]
        public long PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
