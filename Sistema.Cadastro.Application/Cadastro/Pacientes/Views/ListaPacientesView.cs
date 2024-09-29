using Sistema.Cadastro.CrossCutting.Common.CQRS.Views;
using Sistema.Cadastro.Domain.Clientes.Paciente.DTOs;

namespace Sistema.Cadastro.Application.Cadastro.Pacientes.Views
{
    public class ListaPacientesView : View
    {
        public List<PacienteDto> Pacientes { get; }

        public ListaPacientesView(List<PacienteDto> lista)
        {
            Pacientes = lista;
        }
    }
}
