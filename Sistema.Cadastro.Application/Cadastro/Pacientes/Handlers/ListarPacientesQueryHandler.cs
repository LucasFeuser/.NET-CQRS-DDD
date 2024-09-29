using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sistema.Cadastro.CrossCutting.Common.CQRS;
using Sistema.Cadastro.Application.Cadastro.Pacientes.Views;
using Sistema.Cadastro.Domain.Clientes.Paciente.Repositories;
using Sistema.Cadastro.Application.Cadastro.Pacientes.Queries;
using Sistema.Cadastro.Application.Cadastro.Pacientes.Commands;

namespace Sistema.Cadastro.Application.Cadastro.Pacientes.Handlers
{
    public class ListarPacientesQueryHandler : QueryHandler, IRequestHandler<ListarPacientesQuery, IActionResult>
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly ILogger<CadastroPacienteCommand> _logger;

        public ListarPacientesQueryHandler(IPacienteRepository pacienteRepository, ILogger<CadastroPacienteCommand> logger)
        {
            _pacienteRepository = pacienteRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Handle(ListarPacientesQuery command, CancellationToken cancellationToken)
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
