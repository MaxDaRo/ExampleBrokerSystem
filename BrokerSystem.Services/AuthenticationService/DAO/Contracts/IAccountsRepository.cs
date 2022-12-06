using AuthenticationService.Data;

namespace AuthenticationService.DAO.Contracts
{
    public interface IAccountsRepository
    {
        Task<Account?> GetAccountById(long id);
        Task<Account?> GetAccountByEmailAsync(string email);
    }
}
