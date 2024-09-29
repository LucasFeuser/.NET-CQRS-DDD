using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sistema.Cadastro.CrossCutting.Common.CQRS;
using Sistema.Cadastro.CrossCutting.Common.Enums;
using Sistema.Cadastro.Domain.Clientes.Paciente;
using Sistema.Cadastro.Domain.Clientes.Paciente.DTOs;
using Sistema.Cadastro.Application.Cadastro.Pacientes.Views;
using Sistema.Cadastro.Domain.Clientes.Paciente.Repositories;
using Sistema.Cadastro.Application.Cadastro.Pacientes.Commands;
using Sistema.Cadastro.CrossCutting.Common.Entities.ValueObjects.Common;

namespace Sistema.Cadastro.Application.Cadastro.Pacientes.Handlers
{
    public class CadastroPacienteCommandHandler : CommandHandler, IRequestHandler<CadastroPacienteCommand, IActionResult>
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly ILogger<CadastroPacienteCommand> _logger;

        public CadastroPacienteCommandHandler(IPacienteRepository pacienteRepository, ILogger<CadastroPacienteCommand> logger)
        {
            _pacienteRepository = pacienteRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Handle(CadastroPacienteCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var cpfPaciente = new Cpf(command.Cpf);
                bool existsPaciente = await _pacienteRepository.VerificarExistsPaciente(cpfPaciente);

                if (existsPaciente) {
                    AdicionarErro("Cpf já cadastrado!");
                    return ReturnBadRequestComErros<ClienteCadastradoView>
                        (EErroGenericoCodigo.CAMPO_INVALIDO.ToString(), EErroGrupo.REQUISICAO_INVALIDA.ToString());
                }

                var pacienteDto = PacienteDtoBuilder(command);

                await _pacienteRepository.CreateAsync(
                    new Paciente(
                        cpfPaciente, 
                        new NomeCompleto(command.NomeCompleto), 
                        new DataNascimento(command.DataNascimento),
                        pacienteDto
                    ));

                await _pacienteRepository.UnitOfWork.Commit();


                return RetornaOk(new ClienteCadastradoView(pacienteDto));
            }
            catch (Exception ex)
            {
                _logger.LogError("[HANDLER][EXCEPTION] - Erro inesperado ao cadastrar um paciente. Erro: {exception}", ex);
                throw;
            }
        }

        private static PacienteDto PacienteDtoBuilder(CadastroPacienteCommand command)
        {
            return new PacienteDto
            {
                Sexo = command.Sexo,
                Telefone = command.Telefone,
                Email = command.Email,
                PlanoSaude = command.PlanoSaude,
                NumeroCarterinha = command.NumeroCarterinha,
                ReceberNotificacoesWhats = command.ReceberNotificacoesWhats
            };
        }
    }
}
