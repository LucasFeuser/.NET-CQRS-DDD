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
        private readonly IBackgroundJobClient _backgroundJobClient;

        public MediatorHandler(IMediator mediator, IBackgroundJobClient backgroundJobClient)
        {
            _mediator = mediator;
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task<IActionResult> EnviarComandoAsync<T>(T comando) where T : Command
            => await _mediator.Send(comando);

        public async Task<IActionResult> ExecutarQueryAsync<TInput>(TInput query) where TInput : Query
            => await _mediator.Send(query);

        /// <summary>
        /// Publica o evento na fila de processamento do hangfire
        /// </summary>
        /// <typeparam name="T">Evento</typeparam>
        /// <param name="evento"></param>
        /// <returns></returns>
        public async Task PublicarEventoAsync<T>(T evento) where T : Event
            => _backgroundJobClient.Enqueue(() => ExecutarEventoAsync(evento, null!));

        //Contexto do HANGFIRE
        [DisplayName("Processing {0}")]
        [AutomaticRetry(Attempts = 5, DelaysInSeconds = new[] { 15, 30, 60, 120, 240 })]
        public void ExecutarEventoAsync<T>(T evento, PerformContext context) where T : Event
        {
            try
            {
                evento.SetJobId(context!.BackgroundJob!.Id);

                var retryCount = JobStorage.Current.GetConnection().GetJobParameter(context!.BackgroundJob!.Id, "RetryCount");

                evento.SetRetryCount(!string.IsNullOrEmpty(retryCount) ? int.Parse(retryCount) : 0);

                _mediator.Publish(evento).Wait();
            }
            catch (Exception ex)
            {

                throw;
            }
     
        }
    }
}
