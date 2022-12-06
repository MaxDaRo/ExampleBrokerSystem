namespace AuthenticationService.Common.Configuration
{
    public class SecuritySettings
    {
        public const string Key = "Security";
        public string Secret { get; set; }
        public int RefreshTokenTTL { get; set; }
        public int JwtTokenTTLInMinutes { get; set; }
    }
}
