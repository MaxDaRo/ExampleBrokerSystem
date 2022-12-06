using AuthenticationService.Data.ViewModels.Auth;

namespace AuthenticationService.BLL.Contracts
{
    public interface IAuthLogic
    {
        Task<AuthenticationResponse> Authenticate(AuthenticationRequest model);
    }
}
