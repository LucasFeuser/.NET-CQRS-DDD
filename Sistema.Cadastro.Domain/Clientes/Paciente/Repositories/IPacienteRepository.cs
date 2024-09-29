using Sistema.Cadastro.CrossCutting.Common.Abstractions;
using Sistema.Cadastro.CrossCutting.Common.Entities.ValueObjects.Common;
using Sistema.Cadastro.Domain.Clientes.Paciente.DTOs;

namespace Sistema.Cadastro.Domain.Clientes.Paciente.Repositories
{
    public interface IPacienteRepository : IBaseRepository<Paciente>
    {
        Task<bool> VerificarExistsPaciente(Cpf cpf);
        Task<PacienteDto> ObterPacientePorCpf(Cpf cpf);
        Task<List<PacienteDto>> ObterPacientes();
    }
}
