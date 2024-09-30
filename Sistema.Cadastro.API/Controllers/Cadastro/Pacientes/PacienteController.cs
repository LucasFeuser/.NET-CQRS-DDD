using Sistema.Cadastro.Application.Cadastro.Paciente.Commands;
using Sistema.Cadastro.Application.Cadastro.Paciente.Views;
using Sistema.Cadastro.CrossCutting.Common.Abstractions;
using Sistema.Cadastro.API.Controllers.Common;
using Sistema.Cadastro.API.DTOs.Request;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Sistema.Cadastro.Application.Cadastro.Paciente.Queries;

namespace Sistema.Cadastro.API.Controllers.Cadastro.Pacientes
{
    [Route("api/v{version:apiVersion}/pacientes")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class PacienteController : BaseController
    {
        public PacienteController(IMediatorHandler mediatorHandler) : base(mediatorHandler) { }


        /// <summary>
        /// Obter paciente por CPF
        /// </summary>        
        /// <returns></returns>
        [HttpGet("ObterPorCpf/{cpf}")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [Produces("application/json")]
        public async Task<IActionResult> ObterPorCpf(string cpf)
            => await _mediatorHandler.ExecutarQueryAsync(new ObterPacienteQuery(cpf));

        /// <summary>
        /// Obter todos os pacientes
        /// </summary>        
        /// <returns></returns>
        [HttpGet("ObterTodos")]
        [ProducesResponseType(typeof(List<object>), (int)HttpStatusCode.OK)]
        [Produces("application/json")]
        public async Task<IActionResult> ObterTodosPacientes()
            => await _mediatorHandler.ExecutarQueryAsync(new ListarPacientesQuery());

        /// <summary>
        /// Cadastrar novos pacientes
        /// </summary>        
        /// <returns></returns>
        [HttpPost("cadastrar")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [Produces("application/json")]
        public async Task<IActionResult> Cadastrar([FromBody] CadastroPacienteRequest request)
        {
            CadastroPacienteCommand command = new(
               request.Cpf,
               request.NomeCompleto, 
               request.DataNascimento,
               char.Parse(request.Sexo), 
               request.Telefone, 
               request.Email, 
               request.PlanoSaude, 
               request.NumeroCarterinha, 
               request.ReceberNotificacoesWhats,
               new EnderecoPacienteDto(request.Endereco.Cep, request.Endereco.Numero, request.Endereco.Complemento));

           return await _mediatorHandler.EnviarComandoAsync(command);
        }
        
    }
}
