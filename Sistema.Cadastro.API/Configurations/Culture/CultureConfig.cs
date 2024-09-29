using Microsoft.AspNetCore.Localization;

namespace Sistema.Cadastro.API.Configurations.Culture
{
    public static class CultureConfig
    {
        public static IServiceCollection SetDefaultCultureToBrazilian(this IServiceCollection services)
            => services.Configure<RequestLocalizationOptions>(o => o.DefaultRequestCulture = new RequestCulture("pt-BR"));

    }
}
