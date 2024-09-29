using Sistema.Cadastro.CrossCutting.Common.Exceptions;
using Sistema.Cadastro.CrossCutting.Common.Validators;

namespace Sistema.Cadastro.CrossCutting.Common.Entities.ValueObjects.Common
{
    public class Telefone : ValueObject
    {
        protected Telefone() { }
        private string? NumTelefone { get; } = string.Empty;

        public Telefone(string telefone)
        {
            NumTelefone = telefone;
            Validar();
        }

        public override void Validar()
        {
            if (string.IsNullOrEmpty(NumTelefone)) return;

            if (!TelefoneValidator.ValidarNumeroTelefone(NumTelefone))
                throw new InvalidValueObjectException("Telefone informado é inválido.");
        }
    }
}
