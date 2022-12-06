using AuthenticationService.Data.Enums;

namespace AuthenticationService.Data
{
    public class Account
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public Role Role { get; set; }
    }
}
