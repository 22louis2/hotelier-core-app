namespace hotelier_core_app.Model.DTOs.Response
{
    public class RefreshTokenResponseDTO
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public List<string>? Roles { get; set; }
    }
}
