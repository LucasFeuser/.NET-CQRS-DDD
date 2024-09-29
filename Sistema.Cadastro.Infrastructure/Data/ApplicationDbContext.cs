using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Sistema.Cadastro.Domain.Clientes.Paciente;
using Sistema.Cadastro.Domain.Clientes.Endereco;
using Sistema.Cadastro.CrossCutting.Common.CQRS;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sistema.Cadastro.CrossCutting.Common.Entities;
using Sistema.Cadastro.CrossCutting.Common.Extensions;
using Sistema.Cadastro.CrossCutting.Common.Abstractions;

namespace Sistema.Cadastro.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediator;
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Enderecos> Endrecos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediatorHandler mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Entity>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }

        public async Task<bool> Commit()
        {
            try
            {
                var entidadesAlteradas = ChangeTracker.Entries()
                    .Where(e => e.State is not EntityState.Unchanged).ToList();

                if (entidadesAlteradas.Any())
                {
                    AtualizarDataUltimaAtualizacao(entidadesAlteradas);

                    if (await SaveChangesAsync() > 0)
                    {
                        await _mediator.PublicarEventosAsync(this);
                        return true;
                    }

                    return false;
                }

                await _mediator.PublicarEventosAsync(this);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void AtualizarDataUltimaAtualizacao(IEnumerable<EntityEntry> entries)
        {
            foreach (var entry in entries
                         .Where(e => e.Entity is Entity && e.State is EntityState.Modified))
            {
                var propertyInfo = entry.Entity.GetType().GetProperty("DataUltimaAtualizacao");
                if (propertyInfo is not null)
                {
                    propertyInfo.SetValue(entry.Entity, DateTime.UtcNow);
                }
            }
        }
    }
}
