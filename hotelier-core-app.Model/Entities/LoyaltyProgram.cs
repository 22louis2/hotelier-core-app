using hotelier_core_app.Model.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using hotelier_core_app.Model.Attributes;

namespace hotelier_core_app.Model.Entities
{
    [Table("LoyaltyProgram")]
    [TableName("LoyaltyProgram")]
    [Serializable]
    public class LoyaltyProgram : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public int PointsEarned { get; set; }
        public int PointsRedeemed { get; set; }

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
    }
}
