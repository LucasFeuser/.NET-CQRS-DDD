using Sistema.Cadastro.CrossCutting.Common.Entities.ValueObjects.Common;
using Sistema.Cadastro.Domain.Clientes.Paciente.Enums;

namespace Sistema.Cadastro.Domain.Clientes.Paciente.DTOs
{
    public class PacienteDto
    {
        public ESexo Sexo { get; set; }
        public Telefone Telefone { get; set; }
        public Email Email { get; set; }
        public EPlanoSaude PlanoSaude { get; set; }
        public string NumeroCarterinha { get; set; }
        public bool ReceberNotificacoesWhats { get; set; }
    }
}
