using hotelier_core_app.Model.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using hotelier_core_app.Model.Attributes;

namespace hotelier_core_app.Model.Entities
{
    [Table("Discount")]
    [TableName("Discount")]
    [Serializable]
    public class Discount : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; } // Discount name for identification

        [StringLength(500)]
        public string Description { get; set; } // Explanation of the discount

        [Range(0, 100)]
        public decimal Percentage { get; set; } // Discount percentage

        public decimal? FixedAmount { get; set; } // Optional fixed amount discount

        public DateTime? StartDate { get; set; } // When the discount starts
        public DateTime? EndDate { get; set; } // When the discount ends

        public int? MinimumStayDays { get; set; } // Minimum days for eligibility
        public int? MaximumStayDays { get; set; } // Maximum days for eligibility

        public bool IsActive { get; set; } // Status of the discount

        [StringLength(200)]
        public string CreatedBy { get; set; }

        [StringLength(200)]
        public string? ModifiedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }


        [ForeignKey("Tenant")]
        public long? TenantId { get; set; } // Discount applicable to a specific hotel
        public Tenant Tenant { get; set; }

        [ForeignKey("Property")]
        public long? PropertyId { get; set; } // Discount applicable to a specific property
        public Property Property { get; set; }

        [ForeignKey("Room")]
        public long? RoomId { get; set; } // Discount applicable to a specific room
        public Room Room { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>(); // Discounts linked to reservations
    }
}
