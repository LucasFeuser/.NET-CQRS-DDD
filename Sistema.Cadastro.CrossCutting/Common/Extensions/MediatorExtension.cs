using Microsoft.EntityFrameworkCore;
using Sistema.Cadastro.CrossCutting.Common.Entities;
using Sistema.Cadastro.CrossCutting.Common.Abstractions;

namespace Sistema.Cadastro.CrossCutting.Common.Extensions
{
    public static class MediatorExtension
    {
        public static async Task PublicarEventosAsync<T>(this IMediatorHandler mediator, T @context) where T : DbContext
        {
            var domainEntities = @context
                .ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Eventos != null && x.Entity.Eventos.Any());

            var domainEvents = domainEntities.SelectMany(x => x.Entity.Eventos).ToList();

            domainEntities.ToList().ForEach(entity => entity.Entity.LimparEventos());

            var tasks = domainEvents.Select(async (@event) =>
            {
                await mediator.PublicarEventoAsync(@event);
            });

            await Task.WhenAll(tasks);
        }
    }
}
