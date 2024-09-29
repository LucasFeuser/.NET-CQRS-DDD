using Microsoft.AspNetCore.Mvc;
using Sistema.Cadastro.CrossCutting.Common.Abstractions;

namespace Sistema.Cadastro.API.Controllers.Common
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMediatorHandler _mediatorHandler;

        protected BaseController()
        {

        }

        protected BaseController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

    }
}
