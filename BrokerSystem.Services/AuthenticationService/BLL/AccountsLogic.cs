using AuthenticationService.BLL.Contracts;
using AuthenticationService.DAO.Contracts;
using AuthenticationService.Data;

namespace AuthenticationService.BLL
{
    public class AccountsLogic : IAccountsLogic
    {
        private readonly IAccountsRepository _accountsRepository;

        public AccountsLogic(IAccountsRepository accountsRepository)
        {
            _accountsRepository = accountsRepository;
        }

        public async Task<Account?> GetAccountById(long id)
        {
            return await _accountsRepository.GetAccountById(id);
        }
    }
}
