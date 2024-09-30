using Sistema.Cadastro.CrossCutting.Common.Exceptions;
using Sistema.Cadastro.CrossCutting.Common.Validators;

namespace Sistema.Cadastro.CrossCutting.Common.Entities.ValueObjects.Common
{
    public class Telefone : ValueObject
    {
        protected Telefone() { }
        public string? Value { get; } = string.Empty;

        public Telefone(string telefone)
        {
            Value = telefone;
            Validar();
        }

        public override void Validar()
        {
            if (string.IsNullOrEmpty(Value)) return;

            if (!TelefoneValidator.ValidarNumeroTelefone(Value))
                throw new InvalidValueObjectException("Telefone informado é inválido.");
        }
    }
}
