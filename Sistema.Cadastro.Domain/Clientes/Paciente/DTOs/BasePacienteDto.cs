using Sistema.Cadastro.CrossCutting.Common.Entities.ValueObjects.Common;
using Sistema.Cadastro.Domain.Clientes.Paciente.Enums;

namespace Sistema.Cadastro.Domain.Clientes.Paciente.DTOs
{
    public class BasePacienteDto
    {
        public ESexo Sexo { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public EPlanoSaude PlanoSaude { get; set; }
        public string NumeroCarterinha { get; set; }
        public bool ReceberNotificacoesWhats { get; set; }
    }
}
