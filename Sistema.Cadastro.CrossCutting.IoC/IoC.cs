using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sistema.Cadastro.Application;
using Sistema.Cadastro.Infrastructure;

namespace Sistema.Cadastro.CrossCutting.IoC
{
    public static class IoC
    {
        public static void DependencyInjection(this IServiceCollection services, IConfiguration configuration, Type startup)
        {
            services.AddAutoMapper(
               typeof(Application.ApplicationIoC),
               typeof(Infrastructure.InfraestructureIoC),
                startup
           );

            services.ResolveApplication();

            services.ResolveInfraestructure(configuration);
        }

    }
}
