using hotelier_core_app.Model.Attributes;
using hotelier_core_app.Model.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotelier_core_app.Model.Entities
{
    [Table("Module")]
    [TableName("Module")]
    [Serializable]
    public class Module : IBaseEntity
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


        [Range(1, Int64.MaxValue)]
        public long ModuleGroupId { get; set; }
        [ForeignKey("ModuleGroupId")]
        public required ModuleGroup ModuleGroup { get; set; }

        public Module() { }
    }
}
