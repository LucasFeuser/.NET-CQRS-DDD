using Sistema.Cadastro.Domain.Clientes.Endereco;
using Sistema.Cadastro.Infrastructure.Data.Common;
using Sistema.Cadastro.Domain.Clientes.Endereco.Interfaces;

namespace Sistema.Cadastro.Infrastructure.Data.Repositories
{
    public class EnderecoRepository : BaseRepository<Enderecos>, IEnderecoRepository
    {
        public EnderecoRepository(ApplicationDbContext context) : base(context)
        {   }
    }
}
