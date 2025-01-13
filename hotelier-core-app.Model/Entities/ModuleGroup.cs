using hotelier_core_app.Model.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using hotelier_core_app.Model.Attributes;

namespace hotelier_core_app.Model.Entities
{
    [Table("ModuleGroup")]
    [TableName("ModuleGroup")]
    [Serializable]
    public class ModuleGroup : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(500)]
        public string? Url { get; set; }

        [StringLength(200)]
        public string CreatedBy { get; set; }

        [StringLength(200)]
        public string ModifiedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Module> Modules { get; set; } = new List<Module>();

        public ModuleGroup() { }
    }
}
