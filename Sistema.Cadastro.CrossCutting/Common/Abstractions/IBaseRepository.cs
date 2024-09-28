using Sistema.Cadastro.CrossCutting.Common.Entities;

namespace Sistema.Cadastro.CrossCutting.Common.Abstractions
{
    public interface IBaseRepository<T> : IDisposable where T : AggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }

        Task CreateAsync(T aggregate);
        void Update(T aggregate);
        void Remove(T aggregate);
    }
}
