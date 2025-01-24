namespace hotelier_core_app.Model.DTOs.Response
{
    public class ApplicationUserDTO
    {
        public string FullName { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastActiveDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public List<RoleDTO> UserRoles { get; set; }
    }
}
