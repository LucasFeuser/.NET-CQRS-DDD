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

        public Paciente(Cpf cpf, NomeCompleto nomeCompleto, DataNascimento dataNascimento, PacienteDto dto)
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

            ValidarPlanoSaude();
            AdicionarEvento(new PacienteCadastradoEvent(cpf, nomeCompleto, dataNascimento, dto));
        }

        public Paciente AlterarDadosCliente(Cpf cpf, NomeCompleto nomeCompleto, DataNascimento dataNascimento, AlteracaoDadosPacienteDto dto)
        {
            NomeCompleto = nomeCompleto.Value;
            DataNascimento = dataNascimento.Value;
            Telefone = new Telefone(dto.Telefone).Value;
            Email = new Email(dto.Email).Value;
            PlanoSaude = dto.PlanoSaude;
            NumeroCarterinha = dto.NumeroCarterinha;
            ReceberNotificacoesWhats = dto.ReceberNotificacoesWhats;

            ValidarPlanoSaude();
            AdicionarEvento(new DadosPacienteAlteradoEvent(cpf, nomeCompleto, dataNascimento, dto));

            return this;
        }

        private void ValidarPlanoSaude()
        {
            if (!PlanoSaude.Equals(EPlanoSaude.SemPlano) && string.IsNullOrEmpty(NumeroCarterinha))
                throw new ClienteComPlanoDeSaudeSemNumeroCarterinhaException();
        }
    }
}
