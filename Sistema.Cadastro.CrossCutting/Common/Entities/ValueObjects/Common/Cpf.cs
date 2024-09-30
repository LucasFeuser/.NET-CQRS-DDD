using Sistema.Cadastro.CrossCutting.Common.Exceptions;
using Sistema.Cadastro.CrossCutting.Common.Validators;

namespace Sistema.Cadastro.CrossCutting.Common.Entities.ValueObjects.Common
{
    public class Cpf : ValueObject
    {
        protected Cpf() { }
        public string? Value { get; }

        public Cpf(string documento)
        {
            Value = documento;
            Validar();
        }

        public override void Validar()
        {
            if (!CPFValidator.Validar(Value))
                throw new InvalidValueObjectException("Cpf informado é inválido.");
        }
    }
}
