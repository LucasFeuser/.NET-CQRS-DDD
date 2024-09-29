using Sistema.Cadastro.CrossCutting.Common.CQRS.Views;
using Sistema.Cadastro.Domain.Clientes.Paciente.DTOs;

namespace Sistema.Cadastro.Application.Cadastro.Pacientes.Views
{
    public class ClienteCadastradoView : View
    {
        public PacienteDto Paciente { get;  }

        public ClienteCadastradoView(PacienteDto dto)
        {
            Paciente = dto;
        }
    }
}
