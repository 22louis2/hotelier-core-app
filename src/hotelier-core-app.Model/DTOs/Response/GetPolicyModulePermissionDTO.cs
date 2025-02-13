namespace hotelier_core_app.Model.DTOs.Response
{
    public class GetPolicyModulePermissionDTO
    {
        public long Id { get; set; }
        public long PolicyGroupId { get; set; }
        public long ModuleGroupId { get; set; }
        public long PermissionId { get; set; }
    }
}
