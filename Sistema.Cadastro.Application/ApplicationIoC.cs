using Sistema.Cadastro.Application.Cadastro.Pacientes.Mapping;
using Microsoft.Extensions.DependencyInjection;
using Sistema.Cadastro.Application.Common;
using Sistema.Cadastro.Infrastructure;
using System.Reflection;
using FluentValidation;
using MediatR;

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

            services.AddAutoMapper(typeof(PacienteProfile));

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(ApplicationIoC).Assembly);
                config.RegisterServicesFromAssembly(typeof(InfraestructureIoC).Assembly);
            });

        }
    }
}
