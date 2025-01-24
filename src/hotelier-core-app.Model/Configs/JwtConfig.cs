namespace hotelier_core_app.Model.Configs
{
    public class JwtConfig
    {
        public required string TokenKey { get; set; }
        public required string TokenIssuer { get; set; }
        public required string TokenExpiryPeriod { get; set; }
    }
}
