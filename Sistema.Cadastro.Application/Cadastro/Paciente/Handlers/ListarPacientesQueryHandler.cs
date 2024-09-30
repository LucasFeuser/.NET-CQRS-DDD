using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sistema.Cadastro.CrossCutting.Common.CQRS;
using Sistema.Cadastro.Infrastructure.Proxy.Pacientes;
using Sistema.Cadastro.CrossCutting.Common.Abstractions;
using Sistema.Cadastro.Application.Cadastro.Paciente.Views;
using Sistema.Cadastro.Application.Cadastro.Paciente.Queries;
using Sistema.Cadastro.Domain.Clientes.Paciente.Repositories;
using Sistema.Cadastro.Application.Cadastro.Paciente.Commands;

namespace Sistema.Cadastro.Application.Cadastro.Pacientes.Handlers
{
    public class ListarPacientesQueryHandler : QueryHandler, IRequestHandler<ListarPacientesQuery, IActionResult>
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly ILogger<CadastroPacienteCommand> _logger;

        public ListarPacientesQueryHandler(IPacienteRepository pacienteRepository, ILogger<CadastroPacienteCommand> logger, IRedisCache cache)
        {
            _pacienteRepository = new PacientesRepositoryProxy(pacienteRepository, cache);
            _logger = logger;
        }

        public async Task<IActionResult> Handle(ListarPacientesQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var pacientes = await _pacienteRepository.ObterPacientes();
                if (!pacientes.Any())
                {
                    return ReturnNotFound<ListaPacientesView>("Não foram encontrados pacientes!");
                }

                return RetornaOk(new ListaPacientesView(pacientes));
            }
            catch (Exception ex)
            {
                _logger.LogError("[QUERY-HANDLER][EXCEPTION] - Erro inesperado ao resgatar lista de pacientes. Erro: {exception}", ex);
                throw;
            }
        }

    }
}
