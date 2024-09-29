using Sistema.Cadastro.CrossCutting.Common.Entities;
using Sistema.Cadastro.Domain.Clientes.Paciente.DTOs;
using Sistema.Cadastro.Domain.Clientes.Paciente.Enums;
using Sistema.Cadastro.Domain.Clientes.Paciente.Events;
using Sistema.Cadastro.Domain.Clientes.Paciente.Exception;
using Sistema.Cadastro.CrossCutting.Common.Entities.ValueObjects.Common;

namespace Sistema.Cadastro.Domain.Clientes.Paciente
{
    public class Paciente : AggregateRoot
    {
        protected Paciente()
        { }

        /// <summary>
        /// Documento pessoa fisica paciente
        /// </summary>
        public Cpf Cpf { get; private set; }

        /// <summary>
        /// Nome completo do paciente
        /// </summary>
        public NomeCompleto NomeCompleto { get; private set; }

        /// <summary>
        /// Data de nascimento do paciente
        /// </summary>
        public DataNascimento DataNascimento { get; private set; }

        /// <summary>
        /// Genero do paciente
        /// </summary>
        public ESexo Sexo { get; private set; } = ESexo.NaoDefinido;

        /// <summary>
        /// Telefone do paciente
        /// </summary>
        public Telefone Telefone { get; private set; }

        /// <summary>
        /// Email do paciente
        /// </summary>
        public Email Email { get; private set; }

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

        public Paciente(Cpf cpf, NomeCompleto nomeCompleto, DataNascimento dataNascimento, PacienteDto dto)
        {
            Cpf = cpf;
            NomeCompleto = nomeCompleto;
            DataNascimento = dataNascimento;
            Sexo = dto.Sexo;
            Telefone = new Telefone(dto.Telefone);
            Email = new Email(dto.Email);
            PlanoSaude = dto.PlanoSaude;
            NumeroCarterinha = dto.NumeroCarterinha;
            ReceberNotificacoesWhats = dto.ReceberNotificacoesWhats;

            ValidarPlanoSaude();
            AdicionarEvento(new PacienteCadastradoEvent(Cpf, NomeCompleto, DataNascimento, dto));
        }

        public Paciente AlterarDadosCliente(Cpf cpf, NomeCompleto nomeCompleto, DataNascimento dataNascimento, AlteracaoDadosPacienteDto dto)
        {
            NomeCompleto = nomeCompleto;
            DataNascimento = dataNascimento;
            Telefone = new Telefone(dto.Telefone);
            Email = new Email(dto.Email);
            PlanoSaude = dto.PlanoSaude;
            NumeroCarterinha = dto.NumeroCarterinha;
            ReceberNotificacoesWhats = dto.ReceberNotificacoesWhats;

            ValidarPlanoSaude();
            AdicionarEvento(new DadosPacienteAlteradoEvent(Cpf, NomeCompleto, DataNascimento, dto));

            return this;
        }

        private void ValidarPlanoSaude()
        {
            if (!PlanoSaude.Equals(EPlanoSaude.SemPlano) && string.IsNullOrEmpty(NumeroCarterinha))
                throw new ClienteComPlanoDeSaudeSemNumeroCarterinhaException();
        }
    }
}
