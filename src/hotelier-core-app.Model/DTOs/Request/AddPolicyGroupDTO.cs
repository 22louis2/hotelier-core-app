using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace hotelier_core_app.Model.DTOs.Request
{
    public class AddPolicyGroupDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long TenantId { get; set; }
    }
}
