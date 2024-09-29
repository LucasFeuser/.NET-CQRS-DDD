using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sistema.Cadastro.Domain.Clientes.Paciente.Events;
using Sistema.Cadastro.CrossCutting.Common.Abstractions;

namespace Sistema.Cadastro.Application.Cadastro.Pacientes.Handlers.Event
{
    public class PacienteCadastradoEventHandler : INotificationHandler<PacienteCadastradoEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public PacienteCadastradoEventHandler(IServiceProvider serviceProvider)
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
