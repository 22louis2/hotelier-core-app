using hotelier_core_app.Model.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using hotelier_core_app.Model.Attributes;

namespace hotelier_core_app.Model.Entities
{
    [Table("Tenant")]
    [TableName("Tenant")]
    [Serializable]
    public class Tenant : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Logo { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        public DateTime? TrialStartDate { get; set; }

        [StringLength(200)]
        public string CreatedBy { get; set; }

        [StringLength(200)]
        public string? ModifiedBy { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("SubscriptionPlan")]
        public long? SubscriptionPlanId { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; }
        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
        public ICollection<ApplicationRole> Roles { get; set; } = new List<ApplicationRole>();
        public ICollection<Property> Properties { get; set; } = new List<Property>();
        public ICollection<PolicyGroup> PolicyGroups { get; set; } = new List<PolicyGroup>();
    }
}
