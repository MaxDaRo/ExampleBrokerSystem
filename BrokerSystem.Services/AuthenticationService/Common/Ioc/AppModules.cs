using AuthenticationService.BLL;
using AuthenticationService.BLL.Contracts;
using AuthenticationService.DAO.Contracts;
using AuthenticationService.DAO.Postgre;

namespace AuthenticationService.Common.Ioc
{
    public static class AppModules
    {
        public static IServiceCollection RegisterAppModules(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            RegisterDAO(serviceCollection);
            RegisterBLL(serviceCollection);

            return serviceCollection;
        }

        private static void RegisterBLL(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAuthLogic, AuthLogic>();
            serviceCollection.AddScoped<IAccountsLogic, AccountsLogic>();
        }

        private static void RegisterDAO(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAccountsRepository, AccountsInMemoryRepository>();
        }
    }
}
