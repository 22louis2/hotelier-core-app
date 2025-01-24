using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using hotelier_core_app.Model.Interfaces;
using hotelier_core_app.Model.Attributes;

namespace hotelier_core_app.Model.Entities
{
    [Table("Reservation")]
    [TableName("Reservation")]
    [Serializable]
    public class Reservation : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public decimal TotalPrice { get; set; }

        [StringLength(255)]
        public string Status { get; set; }

        [StringLength(200)]
        public string CreatedBy { get; set; }

        [StringLength(200)]
        public string? ModifiedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("User")]
        public long GuestId { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey("Room")]
        public long RoomId { get; set; }
        public Room Room { get; set; }

        [ForeignKey("Discount")]
        public long? DiscountId { get; set; }
        public Discount Discount { get; set; }
    }
}
