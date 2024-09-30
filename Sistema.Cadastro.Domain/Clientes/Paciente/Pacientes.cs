using Sistema.Cadastro.Domain.Clientes.Endereco;
using Sistema.Cadastro.CrossCutting.Common.Entities;
using Sistema.Cadastro.Domain.Clientes.Paciente.DTOs;
using Sistema.Cadastro.Domain.Clientes.Paciente.Enums;
using Sistema.Cadastro.Domain.Clientes.Paciente.Events;
using Sistema.Cadastro.Domain.Clientes.Paciente.Exception;
using Sistema.Cadastro.CrossCutting.Common.Entities.ValueObjects.Common;

namespace Sistema.Cadastro.Domain.Clientes.Paciente
{
    public class Pacientes : AggregateRoot
    {
        protected Pacientes()
        { }

        /// <summary>
        /// Documento pessoa fisica paciente
        /// </summary>
        public string Cpf { get; private set; }

        /// <summary>
        /// Nome completo do paciente
        /// </summary>
        public string NomeCompleto { get; private set; }

        /// <summary>
        /// Data de nascimento do paciente
        /// </summary>
        public DateTime DataNascimento { get; private set; }

        /// <summary>
        /// Genero do paciente
        /// </summary>
        public ESexo Sexo { get; private set; } = ESexo.NaoDefinido;

        /// <summary>
        /// Telefone do paciente
        /// </summary>
        public string Telefone { get; private set; }

        /// <summary>
        /// Email do paciente
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Tipo plano de saúde do paciente
        /// </summary>
        public EPlanoSaude PlanoSaude { get; private set; } = EPlanoSaude.SemPlano;

        /// <summary>
        /// Numero da cartinha do paciente
        /// </summary>
        public string NumeroCarterinha { get; private set; } = string.Empty;

        /// <summary>
        /// Flag para envio de mensagens ao Whatsapp do paciente
        /// </summary>
        public bool ReceberNotificacoesWhats { get; private set; } = false;

        /// <summary>
        /// Endereco do paciente
        /// </summary>
        public virtual Enderecos Endereco { get; set; }
        public virtual long EnderecoId { get; set; }

        public Pacientes(Cpf cpf, NomeCompleto nomeCompleto, DataNascimento dataNascimento, PacienteDto dto, Enderecos endereco)
        {
            Cpf = cpf.Value;
            NomeCompleto = nomeCompleto.Value;
            DataNascimento = dataNascimento.Value;
            Sexo = dto.Sexo;
            Telefone = new Telefone(dto.Telefone).Value;
            Email = new Email(dto.Email).Value;
            PlanoSaude = dto.PlanoSaude;
            NumeroCarterinha = dto.NumeroCarterinha;
            ReceberNotificacoesWhats = dto.ReceberNotificacoesWhats;
            Endereco = endereco;

            ValidarPlanoSaude();
            AdicionarEvento(new PacienteCadastradoEvent(Cpf, Email, NomeCompleto));
        }

        public Pacientes AlterarDadosCliente(Cpf cpf, NomeCompleto nomeCompleto, DataNascimento dataNascimento, AlteracaoDadosPacienteDto dto)
        {
            NomeCompleto = nomeCompleto.Value;
            DataNascimento = dataNascimento.Value;
            Telefone = new Telefone(dto.Telefone).Value;
            Email = new Email(dto.Email).Value;
            PlanoSaude = dto.PlanoSaude;
            NumeroCarterinha = dto.NumeroCarterinha;
            ReceberNotificacoesWhats = dto.ReceberNotificacoesWhats;

            ValidarPlanoSaude();

            return this;
        }

        private void ValidarPlanoSaude()
        {
            if (!PlanoSaude.Equals(EPlanoSaude.SemPlano) && string.IsNullOrEmpty(NumeroCarterinha))
                throw new ClienteComPlanoDeSaudeSemNumeroCarterinhaException();
        }
    }
}
