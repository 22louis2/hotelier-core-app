using hotelier_core_app.Model.Attributes;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotelier_core_app.Model.Entities
{
    [Table("UserLogin")]
    [TableName("UserLogin")]
    [Serializable]
    public class ApplicationUserLogin : IdentityUserLogin<long>
    {
        public long Id { get; set; }
    }
}
