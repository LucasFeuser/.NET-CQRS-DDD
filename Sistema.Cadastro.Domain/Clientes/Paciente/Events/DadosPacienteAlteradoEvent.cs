using Sistema.Cadastro.CrossCutting.Common.Entities.ValueObjects.Common;
using Sistema.Cadastro.Domain.Clientes.Paciente.Enums;
using Sistema.Cadastro.Domain.Clientes.Paciente.DTOs;
using Sistema.Cadastro.CrossCutting.Common.CQRS;

namespace Sistema.Cadastro.Domain.Clientes.Paciente.Events
{
    public class DadosPacienteAlteradoEvent : Event
    {
        protected DadosPacienteAlteradoEvent()
        { }

        public Cpf Cpf { get; private set; }

        public NomeCompleto NomeCompleto { get; private set; }

        public DataNascimento DataNascimento { get; private set; }

        public ESexo Sexo { get; private set; } = ESexo.NaoDefinido;

        public Telefone Telefone { get; private set; }

        public Email Email { get; private set; }

        public EPlanoSaude PlanoSaude { get; private set; } = EPlanoSaude.SemPlano;

        public string NumeroCarterinha { get; private set; } = string.Empty;

        public bool ReceberNotificacoesWhats { get; private set; } = false;

        public DadosPacienteAlteradoEvent(Cpf cpf, NomeCompleto nomeCompleto, DataNascimento dataNascimento, AlteracaoDadosPacienteDto dto)
        {
            Cpf = cpf;
            NomeCompleto = nomeCompleto;
            DataNascimento = dataNascimento;
            Sexo = dto.Sexo;
            Email = new Email(dto.Email);
            PlanoSaude = dto.PlanoSaude;
            NumeroCarterinha = dto.NumeroCarterinha;
            ReceberNotificacoesWhats = dto.ReceberNotificacoesWhats;
        }
    }
}
