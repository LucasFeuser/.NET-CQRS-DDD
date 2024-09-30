using Sistema.Cadastro.CrossCutting.Common.CQRS.Views;
using Sistema.Cadastro.Domain.Clientes.Paciente.DTOs;

namespace Sistema.Cadastro.Application.Cadastro.Paciente.Views
{
    public class PacienteCadastradoView : View
    {
        public PacienteDto Paciente { get;  }

        public PacienteCadastradoView(PacienteDto dto)
        {
            Paciente = dto;
        }
    }
}
