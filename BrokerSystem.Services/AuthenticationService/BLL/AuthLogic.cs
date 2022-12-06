using AuthenticationService.BLL.Contracts;
using AuthenticationService.Common.Configuration;
using AuthenticationService.Common.Constants;
using AuthenticationService.Common.Generators;
using AuthenticationService.DAO.Contracts;
using AuthenticationService.Data.ViewModels.Auth;
using AutoMapper;
using Microsoft.Extensions.Options;

namespace AuthenticationService.BLL
{
    public class AuthLogic : IAuthLogic
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly SecuritySettings _securitySettings;
        private readonly IMapper _mapper;

        public AuthLogic(IAccountsRepository accountsRepository,
            IOptions<SecuritySettings> securitySettings,
            IMapper mapper)
        {
            _accountsRepository = accountsRepository;
            _securitySettings = securitySettings.Value;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest authenticationRequest)
        {
            var account = await _accountsRepository.GetAccountByEmailAsync(authenticationRequest.Email);

            //TODO: should be replaced with hashed password comparing
            if (account == null || account.Password != authenticationRequest.Password)
            {
                throw new BadHttpRequestException("Email or password is invalid");
            }

            var jwtToken = JwtTokenGenerator.GenerateJwtToken(
                new Dictionary<string, string>
                {
                    { AuthConsants.IdClaimIdentifier, account.Id.ToString() },
                    { AuthConsants.NameClaimIdentifier, account.Name.ToString() },
                    { AuthConsants.EmailClaimIdentifier, account.Email.ToString() },
                    { AuthConsants.RoleClaimIdentifier, account.Role.ToString() },
                },
                _securitySettings.Secret.ToCharArray(),
                _securitySettings.JwtTokenTTLInMinutes
            );

            var response = _mapper.Map<AuthenticationResponse>(account);
            response.AccessToken = jwtToken;

            return response;
        }
    }
}
