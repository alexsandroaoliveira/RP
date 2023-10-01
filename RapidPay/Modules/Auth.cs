namespace RapidPay.Modules
{
    public class TokenConfigurations
    {
        public string? Audience { get; set; }
        public string? Issuer { get; set; }
        public int Seconds { get; set; }
        public string? SecretJwtKey { get; set; }
    }

    public class TokenRequest
    {
        public string Email { get; set; }
        public string UserId { get; set; }
    }
}
