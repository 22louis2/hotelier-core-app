namespace hotelier_core_app.Model.DTOs.Request
{
    public class UserLoginRequestDTO
    {
        public string Email {  get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
