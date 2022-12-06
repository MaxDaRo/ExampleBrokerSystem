using AuthenticationService.DAO.Contracts;
using AuthenticationService.Data;
using AuthenticationService.Data.Enums;

namespace AuthenticationService.DAO.Postgre
{
    public class AccountsInMemoryRepository : IAccountsRepository
    {
        private List<Account> _accounts;

        public AccountsInMemoryRepository()
        {
            _accounts = new List<Account>();
            SeedAccountsCollection();
        }

        public async Task<Account?> GetAccountByEmailAsync(string email)
        {
            return await Task.FromResult(_accounts.FirstOrDefault(a => a.Email.Equals(email, StringComparison.InvariantCulture)));
        }

        public async Task<Account?> GetAccountById(long id)
        {
            return await Task.FromResult(_accounts.FirstOrDefault(a => a.Id == id));
        }

        private void SeedAccountsCollection()
        {
            _accounts.Add(new Account { Id = 1, Name = "Admin", Email = "broker@inbox.ru", Role = Role.Admin, Password = "12345678" });
        }
    }
}
