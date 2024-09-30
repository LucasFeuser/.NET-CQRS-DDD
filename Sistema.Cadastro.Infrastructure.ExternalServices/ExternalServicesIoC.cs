using Microsoft.Extensions.DependencyInjection;
using Sistema.Cadastro.CrossCutting.Common.Abstractions;
using Sistema.Cadastro.Infrastructure.ExternalServices.Smtp;
using Sistema.Cadastro.Infrastructure.ExternalServices.BrasilApi;
using Sistema.Cadastro.Infrastructure.ExternalServices.Smtp.Interfaces;
using Sistema.Cadastro.Infrastructure.ExternalServices.Common.HttpClients;
using Sistema.Cadastro.Infrastructure.ExternalServices.BrasilApi.Interfaces;

namespace Sistema.Cadastro.Infrastructure.ExternalServices
{
    public static class ExternalServicesIoC
    {
        public static void ResolveExternalServices(this IServiceCollection services)
        {
            services.AddHttpClient<IBaseHttpClient, BaseHttpClient>();
            services.AddScoped<IBrasilApiService, BrasilApiService>();
            services.AddSingleton<IEnvioEmailService, SmtpService>();
        }
    }
}
