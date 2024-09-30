using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sistema.Cadastro.Domain.Clientes.Endereco;
using Sistema.Cadastro.CrossCutting.Common.CQRS;
using Sistema.Cadastro.CrossCutting.Common.Enums;
using Sistema.Cadastro.Domain.Clientes.Paciente.DTOs;
using Sistema.Cadastro.Domain.Clientes.Endereco.DTOs;
using Sistema.Cadastro.Application.Cadastro.Paciente.Views;
using Sistema.Cadastro.Domain.Clientes.Endereco.ValueObjects;
using Sistema.Cadastro.Domain.Clientes.Paciente.Repositories;
using Sistema.Cadastro.Application.Cadastro.Paciente.Commands;
using Sistema.Cadastro.CrossCutting.Common.Entities.ValueObjects.Common;
using Sistema.Cadastro.Infrastructure.ExternalServices.BrasilApi.Interfaces;

namespace Sistema.Cadastro.Application.Cadastro.Paciente.Handlers
{
    public class CadastroPacienteCommandHandler : CommandHandler, IRequestHandler<CadastroPacienteCommand, IActionResult>
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IBrasilApiService _brasilApiService;

        private readonly ILogger<CadastroPacienteCommand> _logger;

        public CadastroPacienteCommandHandler(
            IPacienteRepository pacienteRepository,
            IBrasilApiService brasilApiService,
            ILogger<CadastroPacienteCommand> logger)
        {
            _pacienteRepository = pacienteRepository;
            _brasilApiService = brasilApiService;
            _logger = logger;
        }

        public async Task<IActionResult> Handle(CadastroPacienteCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var dados = await ValidacoesAntesDeCriar(command);

                if (!dados.valido) {
                    return ReturnBadRequestComErros<PacienteCadastradoView>
                        (EErroGenericoCodigo.CAMPO_INVALIDO.ToString(), EErroGrupo.REQUISICAO_INVALIDA.ToString());
                }

                var pacienteDto = PacienteDtoBuilder(command);

                Enderecos endereco = new(
                    new Cep(command.Endereco.Cep),
                    new Numero(command.Endereco.Numero),
                    command.Endereco.Complemento,
                    dados.enderecoDto
                );

                Domain.Clientes.Paciente.Pacientes paciente = new(
                    new Cpf(command.Cpf),
                    new NomeCompleto(command.NomeCompleto),
                    new DataNascimento(command.DataNascimento),
                    pacienteDto,
                    endereco
                );

                await _pacienteRepository.CreateAsync(paciente);               
                await _pacienteRepository.UnitOfWork.Commit();

                return RetornaOk(new PacienteCadastradoView(pacienteDto));
            }
            catch (Exception ex)
            {
                _logger.LogError("[HANDLER][EXCEPTION] - Erro inesperado ao cadastrar um paciente. Erro: {exception}", ex);
                throw;
            }
        }

        private async Task<(EnderecoDto enderecoDto, bool valido)> ValidacoesAntesDeCriar(CadastroPacienteCommand command)
        {
            bool existsPaciente = await _pacienteRepository.VerificarExistsPaciente(command.Cpf);

            if (existsPaciente)
            {
                AdicionarErro("Cpf já cadastrado!");
                return (null!, false);
            }

            var dadosEndereco = await _brasilApiService.ObterDadosEnderecoPorCep(command.Endereco.Cep);

            if(dadosEndereco is null)
            {
                AdicionarErro("Dados de endereço inválido!");
                return (null!, false);
            }

            return (new EnderecoDto
            {
                Cep = dadosEndereco.cep,
                Endereco = dadosEndereco.street,
                Bairro = dadosEndereco.neighborhood,
                Cidade = dadosEndereco.city,
                UF = dadosEndereco.state
            }, true);
        }

        private static PacienteDto PacienteDtoBuilder(CadastroPacienteCommand command)
        {
            return new PacienteDto
            {
                Cpf = command.Cpf,
                NomeCompleto = command.NomeCompleto,
                DataNascimento = command.DataNascimento,
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
