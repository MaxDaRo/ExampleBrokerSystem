using AuthenticationService.DAO.Contracts;
using AuthenticationService.Data;

namespace AuthenticationService.DAO.Postgre
{
    public class AccountsRepository : IAccountsRepository
    {
        public Task<Account?> GetAccountByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Account?> GetAccountById(long id)
        {
            throw new NotImplementedException();
        }
    }
}
