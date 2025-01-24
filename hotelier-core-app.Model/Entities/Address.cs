using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using hotelier_core_app.Model.Attributes;

namespace hotelier_core_app.Model.Entities
{
    [Table("Address")]
    [TableName("Address")]
    [Serializable]
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(100)]
        public string Street { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [StringLength(100)]
        public string State { get; set; }

        [StringLength(50)]
        public string ZipCode { get; set; }

        [StringLength(150)]
        public string Country { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
