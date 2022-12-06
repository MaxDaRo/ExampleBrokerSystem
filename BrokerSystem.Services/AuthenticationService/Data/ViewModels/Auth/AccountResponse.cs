using AuthenticationService.Data.Enums;

namespace AuthenticationService.Data.ViewModels.Auth
{
    public class AccountResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}
