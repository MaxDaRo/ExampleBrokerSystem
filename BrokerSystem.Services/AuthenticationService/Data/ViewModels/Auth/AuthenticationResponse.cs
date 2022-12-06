namespace AuthenticationService.Data.ViewModels.Auth
{
    public class AuthenticationResponse
    {
        public AccountResponse Account { get; set; }
        public string AccessToken { get; set; }
    }
}
