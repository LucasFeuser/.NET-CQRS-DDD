using Sistema.Cadastro.CrossCutting.Common.Exceptions;
using Sistema.Cadastro.CrossCutting.Common.Validators;

namespace Sistema.Cadastro.CrossCutting.Common.Entities.ValueObjects.Common
{
    public class Cpf : ValueObject
    {
        protected Cpf() { }
        private string? Documento { get; }

        public Cpf(string documento)
        {
            Documento = documento;
            Validar();
        }

        public override void Validar()
        {
            if (!CPFValidator.Validar(Documento))
                throw new InvalidValueObjectException("Cpf informado é inválido.");
        }
    }
}
