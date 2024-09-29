using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sistema.Cadastro.Infrastructure.Data;
using Sistema.Cadastro.Infrastructure.Cache;
using Microsoft.Extensions.DependencyInjection;
using Sistema.Cadastro.Infrastructure.Mediator;
using Sistema.Cadastro.CrossCutting.Common.Abstractions;
using Sistema.Cadastro.Infrastructure.Data.Repositories;
using Sistema.Cadastro.Domain.Clientes.Paciente.Repositories;

namespace Sistema.Cadastro.Infrastructure
{
    public static class InfraestructureIoC
    {
        public static void ResolveInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IUnitOfWork, ApplicationDbContext>();

            services.AddSingleton<IRedisCache, RedisCache>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["Redis:Connection"];
            });

            services.AddScoped<IPacienteRepository, PacienteRepository>();

        }
    }
}
