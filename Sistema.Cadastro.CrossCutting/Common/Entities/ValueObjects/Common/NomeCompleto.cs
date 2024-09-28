using Sistema.Cadastro.CrossCutting.Common.Exceptions;
using System.Drawing;

namespace Sistema.Cadastro.CrossCutting.Common.Entities.ValueObjects.Common
{
    public class NomeCompleto : ValueObject
    {
        protected NomeCompleto() { }
        private string? Nome { get; }

        public NomeCompleto(string nome)
        {
            Nome = nome;
            Validar();
        }

        public override void Validar()
        {
            if (string.IsNullOrWhiteSpace(Nome))
                throw new InvalidValueObjectException("O nome informado é inválido.");

            if (Nome.Split(" ").Length < 2)
                throw new InvalidValueObjectException("O nome informado é inválido.");

            if (Nome.PossuiNumero())
                throw new InvalidValueObjectException("O nome informado é inválido.");
        }

    }
}
