using Sistema.Cadastro.CrossCutting.Common.Exceptions;
using Sistema.Cadastro.CrossCutting.Common.Validators;

namespace Sistema.Cadastro.CrossCutting.Common.Entities.ValueObjects.Common
{
    public class DataNascimento : ValueObject
    {
        protected DataNascimento() { }
        private DateTime Data { get; }

        public DataNascimento(DateTime data)
        {
            Data = data;
            Validar();
        }

        public override void Validar()
        {
            if (!DataValidator.ValidarDataNascimento(Data))
                throw new InvalidValueObjectException("Cpf informado é inválido.");
        }
    }
}
