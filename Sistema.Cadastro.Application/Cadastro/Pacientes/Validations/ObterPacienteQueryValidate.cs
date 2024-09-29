using FluentValidation;
using Sistema.Cadastro.Application.Cadastro.Pacientes.Queries;
using Sistema.Cadastro.CrossCutting.Common.Extensions;

namespace Sistema.Cadastro.Application.Cadastro.Pacientes.Validations
{
    public class ObterPacienteQueryValidate : AbstractValidator<ObterPacienteQuery>
    {
        public ObterPacienteQueryValidate()
        {
            RuleFor(r => r.Cpf)
                .NotEmpty()
                .CpfValido()
                .WithMessage("Cpf informado não é valido.");
        }
    }
}
