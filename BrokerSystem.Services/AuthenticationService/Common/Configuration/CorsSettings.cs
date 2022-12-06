namespace AuthenticationService.Common.Configuration
{
    public class CorsSettings
    {
        public const string Key = "Cors";
        public string[]? Origins { get; set; }
    }
}
