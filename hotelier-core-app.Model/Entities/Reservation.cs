using hotelier_core_app.Core.Enums;
using hotelier_core_app.Model.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotelier_core_app.Model.Entities
{
    public class Reservation : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public decimal TotalPrice { get; set; }

        public ReservationStatus Status { get; set; }

        public long GuestId { get; set; }

        public long RoomId { get; set; }

        public long? DiscountId { get; set; }

        [StringLength(200)]
        public string CreatedBy { get; set; }

        [StringLength(200)]
        public string? ModifiedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public bool IsDeleted { get; set; }

        
        public ApplicationUser Guest { get; set; }

        public Room Room { get; set; }

        public ICollection<Payment> Payments { get; set; }

        public ICollection<ServiceRequest> ServiceRequests { get; set; }
    }
}
