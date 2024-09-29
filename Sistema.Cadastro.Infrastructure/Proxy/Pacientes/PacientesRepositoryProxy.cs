using Microsoft.Extensions.Caching.Distributed;
using Sistema.Cadastro.Domain.Clientes.Paciente;
using Sistema.Cadastro.CrossCutting.Common.Abstractions;
using Sistema.Cadastro.Domain.Clientes.Paciente.Repositories;
using Sistema.Cadastro.CrossCutting.Common.Entities.ValueObjects.Common;
using Sistema.Cadastro.Domain.Clientes.Paciente.DTOs;

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

        public async Task CreateAsync(Paciente agregado)
        {
            await _repository.CreateAsync(agregado);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

        public async Task<PacienteDto> ObterPacientePorCpf(Cpf cpf)
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
            if (pacientes.Any())
            {
                return pacientes;
            }

            pacientes = await _repository.ObterPacientes();
            await _cache.SetAsync(redisKey, pacientes);

            return pacientes;
        }

        public void Remove(Paciente agregado)
        {
            _repository.Remove(agregado);
        }

        public void Update(Paciente agregado)
        {
            _repository.Update(agregado);
        }

        public async Task<bool> VerificarExistsPaciente(Cpf cpf) =>
            await _repository.VerificarExistsPaciente(cpf);
    }
}
