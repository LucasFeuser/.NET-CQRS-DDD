using Sistema.Cadastro.CrossCutting.Common.Constants;
using Sistema.Cadastro.CrossCutting.Common.Exceptions;

namespace Sistema.Cadastro.CrossCutting.Common.Entities.ValueObjects.Common
{
    public class Email : ValueObject
    {
        protected Email() { }
        private string? Value { get; }

        public Email(string email)
        {
            Value = email;
            Validar();
        }

        public override void Validar()
        {
            if (!RegexPatterns.EmailRegexPattern.Match(Value).Success)
                throw new InvalidValueObjectException("Email informado é inválido.");
        }
    }
}
