using hotelier_core_app.Core.Enums;
using hotelier_core_app.Model.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotelier_core_app.Model.Entities
{
    public class ServiceRequest : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public ServiceRequestType Type { get; set; }

        public ServiceRequestStatus Status { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public long ReservationId { get; set; }

        [StringLength(200)]
        public string CreatedBy { get; set; }

        [StringLength(200)]
        public string? ModifiedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public bool IsDeleted { get; set; }


        public Reservation Reservation { get; set; }
    }
}
