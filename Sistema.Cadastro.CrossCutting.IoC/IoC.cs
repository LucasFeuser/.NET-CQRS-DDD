using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Sistema.Cadastro.Infrastructure;
using Sistema.Cadastro.Application;

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

            services.ResolveInfraestructure(configuration);

            services.ResolveApplication(startup);
        }

    }
}
