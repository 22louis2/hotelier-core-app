using hotelier_core_app.Model.Attributes;
using hotelier_core_app.Model.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotelier_core_app.Model.Entities
{
    [Table("RolePermission")]
    [TableName("RolePermission")]
    [Serializable]
    public class RolePermission : IBaseEntity
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

        [ForeignKey("Role")]
        public long RoleId { get; set; }
        public ApplicationRole Role { get; set; }

        [ForeignKey("Permission")]
        public long PermissionId { get; set; }
        public Permission Permission { get; set; }

        [ForeignKey("ModuleGroup")]
        public long ModuleGroupId { get; set; }
        public ModuleGroup ModuleGroup { get; set; }
    }
}
