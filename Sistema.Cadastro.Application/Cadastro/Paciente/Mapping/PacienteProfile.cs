using Sistema.Cadastro.Domain.Clientes.Paciente.DTOs;
using AutoMapper;

namespace Sistema.Cadastro.Application.Cadastro.Pacientes.Mapping
{
    public class PacienteProfile : Profile
    {
        public PacienteProfile()
        {
            CreateMap<Domain.Clientes.Paciente.Pacientes, PacienteDto>();
            CreateMap<PacienteDto, Domain.Clientes.Paciente.Pacientes>();
        }
    }
}
