using MediatR;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;
using Sistema.Cadastro.CrossCutting.Messages;

namespace Sistema.Cadastro.CrossCutting.Common.CQRS
{
    [DataContract]
    public abstract class Command : Message, IRequest<IActionResult>
    {
        public Command()
        {
            Timestamp = DateTime.Now;
            ValidationResult = new();
            AggregateId = new();
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }

        [DataMember]
        protected DateTime Timestamp { get; } = new();

        [DataMember]
        protected ValidationResult ValidationResult { get; private set; } = new();

        public ValidationResult ObterErros()
          => ValidationResult;

        public virtual void AdicionarErro(ValidationResult validationResult)
            => ValidationResult = validationResult;

        public virtual void AdicionarErros(List<ValidationFailure> erros)
            => ValidationResult.Errors.AddRange(erros);
    }
}
