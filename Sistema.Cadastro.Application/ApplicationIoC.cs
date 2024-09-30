using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sistema.Cadastro.Application.Cadastro.Pacientes.Mapping;
using Sistema.Cadastro.Application.Common;
using Sistema.Cadastro.Application.Services;
using Sistema.Cadastro.Application.Services.Interfaces;
using Sistema.Cadastro.Infrastructure;
using System.Reflection;

namespace Sistema.Cadastro.Application
{
    public static class ApplicationIoC
    {
        public static void ResolveApplication(this IServiceCollection services, Type startup)
        {
            AssemblyScanner
            .FindValidatorsInAssembly(Assembly.GetAssembly(typeof(ApplicationIoC)))
            .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FailFastRequestBehavior<,>));
            services.AddTransient<INotificacaoService, NotificacaoService>();

            services.AddAutoMapper(typeof(PacienteProfile));

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(ApplicationIoC).Assembly);
                config.RegisterServicesFromAssembly(typeof(InfraestructureIoC).Assembly);
            });

        }
    }
}
