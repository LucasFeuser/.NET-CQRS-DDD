using Sistema.Cadastro.Domain.Clientes.Paciente.DTOs;
using Sistema.Cadastro.Domain.Clientes.Paciente;
using AutoMapper;

namespace Sistema.Cadastro.Application.Cadastro.Pacientes.Mapping
{
    public class PacienteProfile : Profile
    {
        public PacienteProfile()
        {
            CreateMap<Paciente, PacienteDto>();
        }
    }
}
