namespace Sistema.Cadastro.CrossCutting.Common.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}
