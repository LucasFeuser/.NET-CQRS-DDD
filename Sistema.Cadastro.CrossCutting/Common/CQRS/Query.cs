using MediatR;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;
using System.Runtime.Serialization;
using Sistema.Cadastro.CrossCutting.Messages;

namespace Sistema.Cadastro.CrossCutting.Common.CQRS
{
    [DataContract]
    public abstract class Query : Message, IRequest<IActionResult>
    {
        [DataMember]
        protected ValidationResult ValidationResult { get; private set; } = new ValidationResult();

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
