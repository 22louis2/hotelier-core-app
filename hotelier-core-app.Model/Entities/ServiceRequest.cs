using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using hotelier_core_app.Model.Interfaces;
using hotelier_core_app.Model.Attributes;

namespace hotelier_core_app.Model.Entities
{
    [Table("ServiceRequest")]
    [TableName("ServiceRequest")]
    [Serializable]
    public class ServiceRequest : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(150)]
        public string ServiceType { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(200)]
        public string CreatedBy { get; set; }

        [StringLength(200)]
        public string? ModifiedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("Reservation")]
        public long ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
