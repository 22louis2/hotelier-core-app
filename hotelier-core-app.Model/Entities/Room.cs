using hotelier_core_app.Core.Enums;
using hotelier_core_app.Model.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotelier_core_app.Model.Entities
{
    public class Room : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(255)]
        public string Number { get; set; }

        public RoomType Type { get; set; }

        public int Capacity { get; set; }

        public decimal PricePerNight { get; set; }

        public bool IsAvailable { get; set; }

        public long PropertyId { get; set; }

        [StringLength(200)]
        public string CreatedBy { get; set; }

        [StringLength(200)]
        public string? ModifiedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public bool IsDeleted { get; set; }


        public Property Property { get; set; }

        public ICollection<Discount> Discounts { get; set; }
    }
}
