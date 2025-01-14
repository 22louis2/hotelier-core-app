using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using hotelier_core_app.Model.Interfaces;
using hotelier_core_app.Model.Attributes;

namespace hotelier_core_app.Model.Entities
{
    [Table("Room")]
    [TableName("Room")]
    [Serializable]
    public class Room : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(255)]
        public string Number { get; set; }

        [StringLength(255)]
        public string Type { get; set; }

        [Range(1, int.MaxValue)]
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }

        [StringLength(200)]
        public string CreatedBy { get; set; }

        [StringLength(200)]
        public string? ModifiedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("Property")]
        public long PropertyId { get; set; }
        public Property Property { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
