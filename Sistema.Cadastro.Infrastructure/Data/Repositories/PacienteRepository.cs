using Sistema.Cadastro.CrossCutting.Common.Entities.ValueObjects.Common;
using Sistema.Cadastro.Domain.Clientes.Paciente.Repositories;
using Sistema.Cadastro.Domain.Clientes.Paciente.DTOs;
using Sistema.Cadastro.Infrastructure.Data.Common;
using Sistema.Cadastro.Domain.Clientes.Paciente;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Sistema.Cadastro.Infrastructure.Data.Repositories
{
    public class PacienteRepository : BaseRepository<Paciente>, IPacienteRepository
    {
        private readonly IMapper _map;

        public PacienteRepository(ApplicationDbContext context, IMapper map) : base(context)
        {
            _map = map;
        }

        public async Task<PacienteDto> ObterPacientePorCpf(Cpf cpf)
        {
            var paciente = await _context.Pacientes
                .Where(p => p.Cpf == cpf.Value)
                .SingleOrDefaultAsync();

            return _map.Map<PacienteDto>(paciente);
        }

        public async Task<List<PacienteDto>> ObterPacientes()
        {
            var pacientes = await _context.Pacientes.ToListAsync();

            return _map.Map<List<PacienteDto>>(pacientes);
        }

        public async Task<bool> VerificarExistsPaciente(Cpf cpf)
        {
            var paciente = await _context.Pacientes
                .Where(p => p.Cpf == cpf.Value)
                .SingleOrDefaultAsync();

            return paciente is not null;
        }
    }
}
