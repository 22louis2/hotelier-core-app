using hotelier_core_app.Model.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotelier_core_app.Model.Entities
{
    [Table("AuditLog")]
    [TableName("AuditLog")]
    [Serializable]
    public class AuditLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [StringLength(500)]
        public string Devicetype { get; set; }
        [StringLength(500)]
        public string IpAddress { get; set; }
        [StringLength(500)]
        public string MacAddress { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        [StringLength(500)]
        public string Location { get; set; }
        [StringLength(500)]
        public string PerformedBy { get; set; }
        [StringLength(500)]
        public string PerformerEmail { get; set; }
        [StringLength(500)]
        public string PerformedAgainst { get; set; }
        [StringLength(500)]
        public string Action { get; set; }
        public DateTime DatePerformed { get; set; }

        public AuditLog() { }
    }
}
