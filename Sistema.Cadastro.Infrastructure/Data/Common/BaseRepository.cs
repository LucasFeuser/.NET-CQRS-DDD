using Microsoft.EntityFrameworkCore;
using Sistema.Cadastro.CrossCutting.Common.Entities;
using Sistema.Cadastro.CrossCutting.Common.Abstractions; 

namespace Sistema.Cadastro.Infrastructure.Data.Common
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : AggregateRoot
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> DbSet;
        public IUnitOfWork UnitOfWork => _context; 

        protected BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }

        public async Task CreateAsync(T aggregate)
        {
            await DbSet.AddAsync(aggregate);
        }

        public void Remove(T aggregate)
        {
            DbSet.Remove(aggregate);
        }

        public void Update(T aggregate)
        {   }

        #region Dispose Pattern

        private bool Disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!Disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                Disposed = true;
            }
        }

        public void Dispose() { Dispose(true); }

        ~BaseRepository() { Dispose(false); }

        #endregion
    }
}
