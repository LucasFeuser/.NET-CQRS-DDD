using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Sistema.Cadastro.CrossCutting.Common.CQRS.Views;

namespace Sistema.Cadastro.CrossCutting.Common.CQRS
{
    public abstract class CommonHandler
    {
        protected ValidationResult ValidationResult = new ValidationResult();

        protected virtual bool ErroNoProcessamento => ValidationResult.Errors.Any();

        protected virtual void AdicionarErro(string mensagem)
            => ValidationResult.Errors.Add(new ValidationFailure(propertyName: string.Empty, errorMessage: mensagem));

        #region 2xxx

        public IActionResult RetornaOk<T>() where T : View
        => new OkObjectResult(new BaseResponse<T>(System.Net.HttpStatusCode.OK, $"Operação realizada com sucesso."));

        public IActionResult RetornaOk<T>(T view) where T : View
         => new OkObjectResult(new BaseResponse<T>(System.Net.HttpStatusCode.OK, $"Operação realizada com sucesso.", view));

        public IActionResult RetornaOkComLista<T>(List<T> view) where T : View
            => new OkObjectResult(new BaseListResponse<T>(System.Net.HttpStatusCode.OK, "Operação realizada com sucesso.", view));

        public IActionResult RetornaOkComListaVazia<T>() where T : View
           => new OkObjectResult(new BaseListResponse<T>(System.Net.HttpStatusCode.OK, "Operação realizada com sucesso."));
        #endregion

        #region 4xx
        public IActionResult ReturnBadRequestComErros<T>(string codigo, string grupo) where T : View
            => new BadRequestObjectResult(new BaseResponse<T>(System.Net.HttpStatusCode.BadRequest, ValidationResult.Errors.Select(c => new ResponseErroView(codigo, grupo, c.ErrorMessage)).ToList()));

        public IActionResult ReturnBadRequest(string message)
            => new BadRequestObjectResult(new BaseResponse<View>(System.Net.HttpStatusCode.BadRequest, message));

        public static IActionResult ReturnNotFound<T>(string message) where T : View
       => new NotFoundObjectResult(new BaseResponse<T>(System.Net.HttpStatusCode.NotFound, message));

        #endregion

    }
}
