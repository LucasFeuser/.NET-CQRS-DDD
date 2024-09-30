using Sistema.Cadastro.CrossCutting.Common.CQRS;

namespace Sistema.Cadastro.Domain.Clientes.Paciente.Events
{
    public class PacienteCadastradoEvent : Event
    {
        protected PacienteCadastradoEvent()
        { }

        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public string NomeCompleto { get; private set; }

        public PacienteCadastradoEvent(string cpf, string email, string nomeCompleto)
        {
            Cpf = cpf;
            Email = email;
            NomeCompleto = nomeCompleto;
        }
    }
}
