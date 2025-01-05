using hotelier_core_app.Model.Attributes;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotelier_core_app.Model.Entities
{
    [Table("UserToken")]
    [TableName("UserToken")]
    [Serializable]
    public class ApplicationUserToken : IdentityUserToken<long>
    {
        public long Id { get; set; }
    }
}
