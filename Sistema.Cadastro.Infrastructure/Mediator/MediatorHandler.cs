using Hangfire;
using MediatR;
using Hangfire.Server;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Sistema.Cadastro.CrossCutting.Common.CQRS;
using Sistema.Cadastro.CrossCutting.Common.Abstractions;

namespace Sistema.Cadastro.Infrastructure.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> EnviarComandoAsync<T>(T comando) where T : Command
            => await _mediator.Send(comando);

        public async Task<IActionResult> ExecutarQueryAsync<TInput>(TInput query) where TInput : Query
            => await _mediator.Send(query);

        public async Task PublicarEventoAsync<T>(T evento) where T : Event
           => await Task.FromResult(BackgroundJob.Enqueue(() => ExecutarEventoAsync(evento, null!)));

        //Contexto do HANGFIRE
        [DisplayName("Processing {0}")]
        [AutomaticRetry(Attempts = 5, DelaysInSeconds = new[] { 15, 30, 60, 120, 240 })]
        private void ExecutarEventoAsync<T>(T evento, PerformContext context) where T : Event
        {
            evento.SetJobId(context!.BackgroundJob!.Id);

            var retryCount = JobStorage.Current.GetConnection().GetJobParameter(context!.BackgroundJob!.Id, "RetryCount");

            evento.SetRetryCount(!string.IsNullOrEmpty(retryCount) ? int.Parse(retryCount) : 0);


            _mediator.Publish(evento).Wait();
        }
    }
}
