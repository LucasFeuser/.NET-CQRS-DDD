using MediatR;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Sistema.Cadastro.Domain.Clientes.Paciente.DTOs;
using Sistema.Cadastro.Domain.Clientes.Paciente.Events;
using Sistema.Cadastro.Application.Services.Interfaces;
using Sistema.Cadastro.Domain.Clientes.Paciente.Repositories;

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
            var repo = scope.ServiceProvider.GetRequiredService<IPacienteRepository>();

            var paciente = await repo.ObterPacientePorCpf(notification.Cpf);
            var map = scope.ServiceProvider.GetRequiredService<IMapper>();
            var dto = map.Map<PacienteDto>(paciente);

            var notificacao = scope.ServiceProvider.GetRequiredService<INotificacaoService>();
            notificacao.EnviarNotificacoesNovoCadastro(dto);
        }
    }
}
