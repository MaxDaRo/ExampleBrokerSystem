namespace AuthenticationService.Common.Ioc
{
    public static class AppModulesBuilderExtension
    {
        public static WebApplicationBuilder AddAppModules(this WebApplicationBuilder builder)
        {
            AppModules.RegisterAppModules(builder.Services, builder.Configuration);

            return builder;
        }
    }
}
