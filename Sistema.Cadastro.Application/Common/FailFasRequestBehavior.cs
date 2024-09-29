using MediatR;
using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Sistema.Cadastro.CrossCutting.Common.Enums;
using Sistema.Cadastro.CrossCutting.Common.CQRS.Views;

namespace Sistema.Cadastro.Application.Common
{
    public class FailFastRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
      where TRequest : IRequest<TResponse> where TResponse : IActionResult
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public FailFastRequestBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
            {
                var listaErro = failures.Select(c => c.ErrorMessage).ToList();
                var view = new BaseResponse<View>(HttpStatusCode.BadRequest,
                    failures.Select(
                        c => new ResponseErroView(
                            EErroGenericoCodigo.CAMPO_INVALIDO.ToString(),
                            EErroGrupo.REQUISICAO_INVALIDA.ToString(),
                            c.ErrorMessage
                        )
                    ).ToList()
                 );

                return Task.FromResult((TResponse)(IActionResult)new BadRequestObjectResult(view));

            }

            return next();
        }
    }
}
