using AuthenticationService.Data;

namespace AuthenticationService.BLL.Contracts
{
    public interface IAccountsLogic
    {
        Task<Account?> GetAccountById(long id);
    }
}
