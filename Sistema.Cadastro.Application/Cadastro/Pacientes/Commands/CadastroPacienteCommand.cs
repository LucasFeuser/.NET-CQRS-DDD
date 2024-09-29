using Sistema.Cadastro.Application.Cadastro.Pacientes.Validations;
using Sistema.Cadastro.Domain.Clientes.Paciente.Enums;
using Sistema.Cadastro.CrossCutting.Common.CQRS;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Sistema.Cadastro.Application.Cadastro.Pacientes.Commands
{
    [DataContract]
    public class CadastroPacienteCommand : Command
    {
        protected CadastroPacienteCommand()
        {   }

        public CadastroPacienteCommand(
            string cpf, 
            string nomeCompleto, 
            DateTime dtaNascimento, 
            ESexo sexo, 
            string telefone, 
            string email, 
            EPlanoSaude planoSaude,
            string numeroCarteirinha,
            bool receberNotificacoesWpp)
        {
            Cpf = cpf;
            NomeCompleto = nomeCompleto;
            DataNascimento = dtaNascimento;
            Sexo = sexo;
            Telefone = telefone;
            Email = email;
            PlanoSaude = planoSaude;
            NumeroCarterinha = numeroCarteirinha;
            ReceberNotificacoesWhats = receberNotificacoesWpp;
        }

        [DataMember]
        [Required]
        public string Cpf { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public string NomeCompleto { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public DateTime DataNascimento { get; private set; } = new();

        [DataMember]
        public ESexo Sexo { get; private set; } = ESexo.NaoDefinido;

        [DataMember]
        public string Telefone { get; private set; } = string.Empty;

        [DataMember]
        [Required]
        public string Email { get; private set; } = string.Empty;

        [DataMember]
        public EPlanoSaude PlanoSaude { get; private set; } = EPlanoSaude.SemPlano;

        [DataMember]
        public string NumeroCarterinha { get; private set; } = string.Empty;

        [DataMember]
        public bool ReceberNotificacoesWhats { get; private set; } = false;

        public override bool IsValid()
        {
            AdicionarErro(new CadastroPacienteCommandValidate().Validate(this));

            return ValidationResult.IsValid;
        }
    }
}
