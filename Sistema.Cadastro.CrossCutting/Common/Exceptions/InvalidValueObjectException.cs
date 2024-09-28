namespace Sistema.Cadastro.CrossCutting.Common.Exceptions
{
    public class InvalidValueObjectException : DomainException
    {

        protected InvalidValueObjectException()
        {   }

        public InvalidValueObjectException(string message) : base(message)
        {   }
    }
}
