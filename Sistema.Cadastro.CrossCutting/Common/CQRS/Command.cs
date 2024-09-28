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
        [DataMember]
        protected DateTime Timestamp { get; private set; } = DateTime.Now;

        [DataMember]
        protected ValidationResult ValidationResult { get; private set; } = new ValidationResult();

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
