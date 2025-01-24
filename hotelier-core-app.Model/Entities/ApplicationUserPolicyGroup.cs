using hotelier_core_app.Model.Attributes;
using hotelier_core_app.Model.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotelier_core_app.Model.Entities
{
    [Table("UserPolicyGroup")]
    [TableName("UserPolicyGroup")]
    [Serializable]
    public class ApplicationUserPolicyGroup : IBaseEntity
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

        [ForeignKey("User")]
        public long UserId { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey("PolicyGroup")]
        public long PolicyGroupId { get; set; }
        public PolicyGroup PolicyGroup { get; set; }
    }
}
