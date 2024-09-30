using Sistema.Cadastro.Domain.Clientes.Paciente.Repositories;
using Sistema.Cadastro.Domain.Clientes.Paciente.DTOs;
using Sistema.Cadastro.Infrastructure.Data.Common;
using Sistema.Cadastro.Domain.Clientes.Paciente;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Sistema.Cadastro.Infrastructure.Data.Repositories
{
    public class PacienteRepository : BaseRepository<Pacientes>, IPacienteRepository
    {
        private readonly IMapper _map;

        public PacienteRepository(ApplicationDbContext context, IMapper map) : base(context)
        {
            _map = map;
        }

        public async Task<PacienteDto> ObterPacientePorCpf(string cpf)
        {
            var paciente = await _context.Pacientes
                .Where(p => p.Cpf == cpf)
                .FirstOrDefaultAsync();

            return _map.Map<PacienteDto>(paciente);
        }

        public async Task<List<PacienteDto>> ObterPacientes()
        {
            var pacientes = await _context.Pacientes.ToListAsync();

            return _map.Map<List<PacienteDto>>(pacientes);
        }

        public async Task<bool> VerificarExistsPaciente(string cpf)
        {
            var paciente = await _context.Pacientes
                            .Where(p => p.Cpf == cpf)
                            .FirstOrDefaultAsync();

            return paciente is not null;
        }
    }
}
