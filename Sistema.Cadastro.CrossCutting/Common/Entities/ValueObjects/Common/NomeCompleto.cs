using Sistema.Cadastro.CrossCutting.Common.Exceptions;
using System.Drawing;

namespace Sistema.Cadastro.CrossCutting.Common.Entities.ValueObjects.Common
{
    public class NomeCompleto : ValueObject
    {
        protected NomeCompleto() { }
        public string? Value { get; }

        public NomeCompleto(string nome)
        {
            Value = nome;
            Validar();
        }

        public override void Validar()
        {
            if (string.IsNullOrWhiteSpace(Value))
                throw new InvalidValueObjectException("O nome informado é inválido.");

            if (Value.Split(" ").Length < 2)
                throw new InvalidValueObjectException("O nome informado é inválido.");

            if (Value.PossuiNumero())
                throw new InvalidValueObjectException("O nome informado é inválido.");
        }

    }
}
