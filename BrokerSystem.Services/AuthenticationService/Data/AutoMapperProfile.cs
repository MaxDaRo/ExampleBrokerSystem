using AuthenticationService.Data.ViewModels.Auth;
using AutoMapper;

namespace AuthenticationService.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateAuthMap();
            CreateAccountsMap();
        }

        private void CreateAccountsMap()
        {
            CreateMap<Account, AccountResponse>();
        }

        private void CreateAuthMap()
        {
            CreateMap<Account, AuthenticationResponse>()
                .ForMember(
                    dest => dest.Account,
                    opt => opt.MapFrom(x => x));
        }
    }
}
