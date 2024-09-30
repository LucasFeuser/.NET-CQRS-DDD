using Sistema.Cadastro.CrossCutting.Common.Entities.ValueObjects;
using Sistema.Cadastro.CrossCutting.Common.Exceptions;

namespace Sistema.Cadastro.Domain.Clientes.Endereco.ValueObjects
{
    public class Cep : ValueObject
    {
        protected Cep() { }
        public string? Value { get; } = string.Empty;

        public Cep(string cep)
        {
            Value = cep;
            Validar();
        }

        public override void Validar()
        {
            if (string.IsNullOrEmpty(Value))
                throw new InvalidValueObjectException("Cep não pode ser vazio");

            if (!Value.All(char.IsDigit))
                throw new InvalidValueObjectException("Cep não pode conter outros valores além de numeros");
        }
    }
}
