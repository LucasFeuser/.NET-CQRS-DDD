using Sistema.Cadastro.CrossCutting.Common.Entities.ValueObjects;
using Sistema.Cadastro.CrossCutting.Common.Exceptions;

namespace Sistema.Cadastro.Domain.Clientes.Endereco.ValueObjects
{
    public class Numero : ValueObject
    {
        protected Numero() { }
        public int Value { get; } = 0;

        public Numero(int numero)
        {
            Value = numero;
            Validar();
        }

        public override void Validar()
        {
            if (Value <= 0)
                throw new InvalidValueObjectException("Numero de endereço não pode ser 0, caso nao houver numero, especifique no complemento.");
        }
    }
}
