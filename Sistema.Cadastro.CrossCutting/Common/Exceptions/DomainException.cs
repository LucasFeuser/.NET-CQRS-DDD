﻿namespace Sistema.Cadastro.CrossCutting.Common.Exceptions
{
    public abstract class DomainException : Exception
    {
        protected DomainException() {    }

        public DomainException(string message) : base(message)  {   }

        public DomainException(string message, Exception innerException) : base(message, innerException)
        {   }
    }
}
