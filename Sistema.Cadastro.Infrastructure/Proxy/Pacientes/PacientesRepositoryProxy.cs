using Sistema.Cadastro.Domain.Clientes.Paciente.DTOs;
using Sistema.Cadastro.CrossCutting.Common.Abstractions;
using Sistema.Cadastro.Domain.Clientes.Paciente.Repositories;

namespace Sistema.Cadastro.Infrastructure.Proxy.Pacientes
{
    public class PacientesRepositoryProxy : IPacienteRepository
    {
        private readonly IPacienteRepository _repository;
        private readonly IRedisCache _cache;

        public PacientesRepositoryProxy(IPacienteRepository repository, IRedisCache cache)
        {
            _repository = repository;
            _cache = cache;
        }

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public async Task CreateAsync(Domain.Clientes.Paciente.Pacientes agregado)
        {
            await _repository.CreateAsync(agregado);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public async Task<PacienteDto> ObterPacientePorCpf(string cpf)
        {
            string redisKey = cpf.ToString()!;
            PacienteDto paciente = null!;

            paciente = await _cache.GetAsync<PacienteDto>(redisKey);
            if (paciente is not null){
                return paciente;
            }

            paciente = await _repository.ObterPacientePorCpf(cpf);
            await _cache.SetAsync(redisKey, paciente);

            return paciente;
        }

        public async Task<List<PacienteDto>> ObterPacientes()
        {
            string redisKey = "pacientes_all";
            List<PacienteDto> pacientes = new(0)!;

            pacientes = await _cache.GetAsync<List<PacienteDto>>(redisKey);
            if (pacientes is not null)
            {
                return pacientes;
            }

            pacientes = await _repository.ObterPacientes();
            await _cache.SetAsync(redisKey, pacientes);

            return pacientes;
        }

        public void Remove(Domain.Clientes.Paciente.Pacientes agregado)
        {
            _repository.Remove(agregado);
        }

        public void Update(Domain.Clientes.Paciente.Pacientes agregado)
        {
            _repository.Update(agregado);
        }

        public async Task<bool> VerificarExistsPaciente(string cpf) =>
            await _repository.VerificarExistsPaciente(cpf);
    }
}
