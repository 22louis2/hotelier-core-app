namespace hotelier_core_app.Model.DTOs.Request
{
    public class UpdatePolicyGroupDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long TenantId { get; set; }
    }
}
