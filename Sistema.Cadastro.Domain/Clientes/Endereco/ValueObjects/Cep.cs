using Sistema.Cadastro.CrossCutting.Common.Entities.ValueObjects;
using Sistema.Cadastro.CrossCutting.Common.Exceptions;

namespace Sistema.Cadastro.Domain.Clientes.Endereco.ValueObjects
{
    public class Cep : ValueObject
    {
        protected Cep() { }
        private string? NumeroCep { get; } = string.Empty;

        public Cep(string cep)
        {
            NumeroCep = cep;
            Validar();
        }

        public override void Validar()
        {
            if (string.IsNullOrEmpty(NumeroCep))
                throw new InvalidValueObjectException("Cep não pode ser vazio");

            if (NumeroCep.All(char.IsDigit))
                throw new InvalidValueObjectException("Cep não pode conter outros valores além de numeros");
        }
    }
}
