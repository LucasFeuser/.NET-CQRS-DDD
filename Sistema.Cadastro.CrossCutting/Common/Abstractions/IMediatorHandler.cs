using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sistema.Cadastro.CrossCutting.Common.CQRS;

namespace Sistema.Cadastro.CrossCutting.Common.Abstractions
{
    public interface IMediatorHandler
    {
        Task PublicarEventoAsync<T>(T evento) where T : Event;
        Task<IActionResult> EnviarComandoAsync<T>(T comando) where T : Command;
        Task<IActionResult> ExecutarQueryAsync<TInput>(TInput query) where TInput : Query;
    }
}
