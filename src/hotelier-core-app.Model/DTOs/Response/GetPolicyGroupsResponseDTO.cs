using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace hotelier_core_app.Model.DTOs.Response
{
    public class GetPolicyGroupsResponseDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public long TenantId { get; set; }
    }
}
