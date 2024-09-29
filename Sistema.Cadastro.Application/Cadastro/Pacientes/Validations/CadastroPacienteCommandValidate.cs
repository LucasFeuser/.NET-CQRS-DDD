using FluentValidation;
using Sistema.Cadastro.CrossCutting.Common.Extensions;
using Sistema.Cadastro.Domain.Clientes.Paciente.Enums;
using Sistema.Cadastro.Application.Cadastro.Pacientes.Commands;

namespace Sistema.Cadastro.Application.Cadastro.Pacientes.Validations
{
    public class CadastroPacienteCommandValidate : AbstractValidator<CadastroPacienteCommand>
    {
        public CadastroPacienteCommandValidate()
        {
            RuleFor(c => c.Cpf)
                .NotEmpty()
                .CpfValido()
                .WithName("Cpf")
                .WithMessage("Cpf informado não é valido.");

            RuleFor(c => c.NomeCompleto)
               .NotEmpty()
               .Length(3, 100)
               .WithMessage("Nome informado não é valido")
               .WithName("Nome Completo");

            RuleFor(c => c.DataNascimento)
               .NotNull()
               .DataNascimentoValida()
               .WithName("Data De Nascimento");

            RuleFor(c => c.Email)
               .NotEmpty()
               .EmailValido()
               .WithName("Email");

            RuleFor(c => c.Sexo)
               .NotNull()
               .When(c => c.Sexo != ESexo.NaoDefinido)
               .WithMessage("Sexo informado é inválido")
               .WithName("Sexo");

            RuleFor(c => c.Telefone)
               .NotEmpty()
               .When(c => !string.IsNullOrEmpty(c.Telefone))
               .NumeroTelefoneValido()
               .WithName("Numero de telefone");

            RuleFor(c => c.PlanoSaude)
               .NotEmpty()
               .WithMessage("Plano de saúde informado é inválido")
               .WithName("Plano de Saúde");

            RuleFor(c => c.NumeroCarterinha)
              .NotEmpty()
              .When(c => c.PlanoSaude != EPlanoSaude.SemPlano && string.IsNullOrEmpty(c.NumeroCarterinha))
              .WithMessage("É preciso informar o numero de carterinha enquanto tiver um plano")
              .WithName("Numero de Carterinha");
        }

    }
}
