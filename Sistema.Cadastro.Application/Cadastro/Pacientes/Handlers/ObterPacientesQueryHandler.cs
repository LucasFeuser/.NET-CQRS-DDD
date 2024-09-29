using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sistema.Cadastro.CrossCutting.Common.CQRS;
using Sistema.Cadastro.Application.Cadastro.Pacientes.Views;
using Sistema.Cadastro.Domain.Clientes.Paciente.Repositories;
using Sistema.Cadastro.Application.Cadastro.Pacientes.Queries;
using Sistema.Cadastro.Application.Cadastro.Pacientes.Commands;
using Sistema.Cadastro.CrossCutting.Common.Entities.ValueObjects.Common;

namespace Sistema.Cadastro.Application.Cadastro.Pacientes.Handlers
{
    public class ObterPacientesQueryHandler : QueryHandler, IRequestHandler<ObterPacienteQuery, IActionResult>
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly ILogger<ObterPacientesQueryHandler> _logger;

        public ObterPacientesQueryHandler(IPacienteRepository pacienteRepository, ILogger<ObterPacientesQueryHandler> logger)
        {
            _pacienteRepository = pacienteRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Handle(ObterPacienteQuery command, CancellationToken cancellationToken)
        {
            try
            {
                var paciente = await _pacienteRepository.ObterPacientePorCpf(new Cpf(command.Cpf));
                if (paciente is null)
                {
                    AdicionarErro("Paciente não encontrado.");
                    return ReturnNotFound<PacienteView>("Não foram encontrados pacientes!");
                }

                return RetornaOk(new PacienteView(paciente));
            }
            catch (Exception ex)
            {
                _logger.LogError("[QUERY-HANDLER][EXCEPTION] - Erro inesperado ao resgatar paciente. Erro: {exception}", ex);
                throw;
            }
        }
    }
}
