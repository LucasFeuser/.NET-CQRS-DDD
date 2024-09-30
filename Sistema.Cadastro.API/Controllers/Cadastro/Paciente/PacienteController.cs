using Microsoft.AspNetCore.Mvc;
using Sistema.Cadastro.API.Controllers.Common;
using Sistema.Cadastro.Application.Cadastro.Pacientes.Queries;
using Sistema.Cadastro.CrossCutting.Common.Abstractions;
using System.Net;

namespace Sistema.Cadastro.API.Controllers.Cadastro.Paciente
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class PacienteController : BaseController
    {
        public PacienteController(IMediatorHandler mediatorHandler) : base(mediatorHandler) { }


        /// <summary>
        /// Get-Teste
        /// </summary>        
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<object>), (int)HttpStatusCode.OK)]
        [Produces("application/json")]
        public async Task<IActionResult> Get()
        {
            await _mediatorHandler.ExecutarQueryAsync(new ListarPacientesQuery());
            return Ok("Teste");
        }
    }
}
