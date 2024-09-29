using Microsoft.Extensions.DependencyInjection;
using Sistema.Cadastro.Application.Cadastro.Pacientes.Mapping;
using Sistema.Cadastro.Application.Common;
using FluentValidation;
using System.Reflection;
using MediatR;

namespace Sistema.Cadastro.Application
{
    public static class ApplicationIoC
    {
        public static void ResolveApplication(this IServiceCollection services)
        {
            AssemblyScanner
            .FindValidatorsInAssembly(Assembly.GetAssembly(typeof(ApplicationIoC)))
            .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FailFastRequestBehavior<,>));

            services.AddAutoMapper(typeof(PacienteProfile));
        }
    }
}
