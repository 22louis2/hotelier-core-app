namespace hotelier_core_app.Model.DTOs.Request
{
    public class AddPolicyToPolicyGroupDTO
    {
        public long PolicyGroupId { get; set; }
        public long PermissionId { get; set; }
        public long ModuleGroupId { get; set; }
    }
}
