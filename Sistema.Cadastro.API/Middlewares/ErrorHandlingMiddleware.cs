using ValidationResult = FluentValidation.Results.ValidationResult;
using Sistema.Cadastro.CrossCutting.Common.CQRS.Views;
using Sistema.Cadastro.CrossCutting.Common.Exceptions;
using Sistema.Cadastro.CrossCutting.Common.Enums;
using FluentValidation.Results;
using System.Text.Json;
using System.Net;

namespace Sistema.Cadastro.API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (DomainException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var validations = new ValidationResult();

                ObterTodasInnerExceptions(ex, validations.Errors);

                await context.Response.WriteAsync(JsonSerializer.Serialize(new BaseResponse<View>(HttpStatusCode.BadRequest, validations.Errors.Select(c => c.ErrorMessage).ToList())));
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(JsonSerializer.Serialize(
                value: new BaseResponse<View>(
                    code: HttpStatusCode.InternalServerError,
                    errors: new List<ResponseErroView>() { new("ERRO_INTERNO".ToString(), EErroGrupo.ERRO_SISTEMA.ToString(), "Ocorreu um erro interno!") }),
                options: new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        }

        private static int ObterTodasInnerExceptions(Exception ex, List<ValidationFailure> validations)
        {
            validations.Add(new ValidationFailure(propertyName: "Server", errorMessage: ex.Message));

            return ex.InnerException is not null ? ObterTodasInnerExceptions(ex.InnerException, validations) : -1;
        }
    }
}
