using hotelier_core_app.Model.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotelier_core_app.Model.Entities
{
    public class Discount : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public double Percentage { get; set; }

        public decimal FixedAmount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int MinimumStayDays { get; set; }

        public int MaximumStayDays { get; set; }

        public bool IsActive { get; set; }

        public long TenantId { get; set; }

        public long PropertyId { get; set; }

        public long RoomId { get; set; }

        [StringLength(200)]
        public string CreatedBy { get; set; }

        [StringLength(200)]
        public string? ModifiedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public bool IsDeleted { get; set; }


        public Tenant Tenant { get; set; }

        public Property Property { get; set; }

        public Room Room { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
