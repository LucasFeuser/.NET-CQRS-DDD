using Sistema.Cadastro.CrossCutting.Common.CQRS.Views;
using Sistema.Cadastro.Domain.Clientes.Paciente.DTOs;

namespace Sistema.Cadastro.Application.Cadastro.Paciente.Views
{
    public class PacienteView : View
    {
        public PacienteDto Paciente { get; set; }

        public PacienteView(PacienteDto dto)
        {
            Paciente = dto;
        }
    }
}
