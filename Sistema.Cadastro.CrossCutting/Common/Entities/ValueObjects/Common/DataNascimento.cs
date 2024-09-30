using Sistema.Cadastro.CrossCutting.Common.Exceptions;
using Sistema.Cadastro.CrossCutting.Common.Validators;

namespace Sistema.Cadastro.CrossCutting.Common.Entities.ValueObjects.Common
{
    public class DataNascimento : ValueObject
    {
        protected DataNascimento() { }
        public DateTime Value { get; }

        public DataNascimento(DateTime data)
        {
            Value = data;
            Validar();
        }

        public override void Validar()
        {
            if (!DataValidator.ValidarDataNascimento(Value))
                throw new InvalidValueObjectException("Cpf informado é inválido.");
        }
    }
}
