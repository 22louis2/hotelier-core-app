using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using hotelier_core_app.Model.Attributes;

namespace hotelier_core_app.Model.Entities
{
    [Table("Payment")]
    [TableName("Payment")]
    [Serializable]
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [ForeignKey("Reservation")]
        public long ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        [StringLength(50)]
        public string PaymentMethod { get; set; }

        public decimal Amount { get; set; }
        public bool IsSuccessful { get; set; }

        [StringLength(255)]
        public string TransactionId { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
