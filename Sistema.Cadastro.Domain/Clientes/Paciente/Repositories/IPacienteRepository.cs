using Sistema.Cadastro.CrossCutting.Common.Abstractions;
using Sistema.Cadastro.CrossCutting.Common.Entities.ValueObjects.Common;
using Sistema.Cadastro.Domain.Clientes.Paciente.DTOs;

namespace Sistema.Cadastro.Domain.Clientes.Paciente.Repositories
{
    public interface IPacienteRepository : IBaseRepository<Pacientes>
    {
        Task<bool> VerificarExistsPaciente(string cpf);
        Task<PacienteDto> ObterPacientePorCpf(string cpf);
        Task<List<PacienteDto>> ObterPacientes();
    }
}
