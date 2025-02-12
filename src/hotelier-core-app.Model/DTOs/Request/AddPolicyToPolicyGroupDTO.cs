namespace hotelier_core_app.Model.DTOs.Request
{
    public class AddPolicyToPolicyGroupDTO
    {
        public long PolicyGroupId { get; set; }
        public long PolicyId { get; set; }
        public long ModuleGroupId { get; set; }
    }
}
