namespace Infrastructure.Identity.Models.SecuritySettings
{
    public class JwtAppSettings
    {
        public string Secret { get; set; } = null!;
        public int ExpirationHours { get; set; }
        public string Issuer { get; set; } = null!;
        public string ValidIn { get; set; } = null!;
    }
}
