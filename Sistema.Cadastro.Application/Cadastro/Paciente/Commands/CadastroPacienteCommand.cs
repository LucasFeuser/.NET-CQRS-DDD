using Sistema.Cadastro.Application.Cadastro.Paciente.Validations;
using Sistema.Cadastro.Domain.Clientes.Paciente.Enums;
using Sistema.Cadastro.CrossCutting.Common.CQRS;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Sistema.Cadastro.Application.Cadastro.Paciente.Commands
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
            char sexo, 
            string telefone, 
            string email, 
            int planoSaude,
            string numeroCarteirinha,
            bool receberNotificacoesWpp,
            EnderecoPacienteDto endereco)
        {
            Cpf = cpf;
            NomeCompleto = nomeCompleto;
            DataNascimento = dtaNascimento;
            Sexo = (ESexo)sexo;
            Telefone = telefone;
            Email = email;
            PlanoSaude = (EPlanoSaude)planoSaude;
            NumeroCarterinha = numeroCarteirinha;
            ReceberNotificacoesWhats = receberNotificacoesWpp;
            Endereco = endereco;
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

        [DataMember]
        public EnderecoPacienteDto Endereco { get; private set; }

        public override bool IsValid()
        {
            AdicionarErro(new CadastroPacienteCommandValidate().Validate(this));

            return ValidationResult.IsValid;
        }
    }

    [DataContract]
    public class EnderecoPacienteDto
    {
        public EnderecoPacienteDto(string cep, int numeroResidencia, string complemento)
        {
            Cep = cep;
            Numero = numeroResidencia;
            Complemento = complemento;
        }

        [DataMember]
        public string Cep { get; private set; }
        [DataMember]
        public int Numero { get; private set; }
        [DataMember]
        public string Complemento { get; private set; } = string.Empty;
    }
}
