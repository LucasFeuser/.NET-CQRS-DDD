using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sistema.Cadastro.CrossCutting.Common.Abstractions;
using Sistema.Cadastro.Domain.Clientes.Paciente.Events;

namespace Sistema.Cadastro.Application.Cadastro.Pacientes.Handlers.Event
{
    public class DadosAlteradosPacienteEventHandler : INotificationHandler<PacienteCadastradoEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public DadosAlteradosPacienteEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Handle(PacienteCadastradoEvent notification, CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
        }
    }
}
