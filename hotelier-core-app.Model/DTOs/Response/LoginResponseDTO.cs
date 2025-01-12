namespace hotelier_core_app.Model.DTOs.Response
{
    public class LoginResponseDTO
    {
        public string Email { get; set; }

        public string FullName { get; set; }

        public string Picture { get; set; }

        public List<string> Roles { get; set; }
    }
}
