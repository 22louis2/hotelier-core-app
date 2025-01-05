using hotelier_core_app.Model.Attributes;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace hotelier_core_app.Model.Entities
{
    [Table("UserClaim")]
    [TableName("UserClaim")]
    [Serializable]
    public class ApplicationUserClaim : IdentityUserClaim<long>
    {
    }
}
