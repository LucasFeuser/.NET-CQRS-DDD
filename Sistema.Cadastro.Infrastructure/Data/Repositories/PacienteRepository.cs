using Sistema.Cadastro.Domain.Clientes.Paciente.Repositories;
using Sistema.Cadastro.Infrastructure.Data.Common;
using Sistema.Cadastro.Domain.Clientes.Paciente;

namespace Sistema.Cadastro.Infrastructure.Data.Repositories
{
    public class PacienteRepository : BaseRepository<Paciente>, IPacienteRepository
    {
        public PacienteRepository(ApplicationDbContext context) : base(context)
        {   }
    }
}
