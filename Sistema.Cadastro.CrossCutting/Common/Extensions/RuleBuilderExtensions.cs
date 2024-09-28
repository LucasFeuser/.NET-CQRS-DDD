using FluentValidation;
using Sistema.Cadastro.CrossCutting.Common.Constants;
using Sistema.Cadastro.CrossCutting.Common.Validators;

namespace Sistema.Cadastro.CrossCutting.Common.Extensions
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, string> EmailValido<T>(this IRuleBuilder<T, string> ruleBuilder) =>
            ruleBuilder
           .Matches(RegexPatterns.EmailRegexPattern)
           .WithMessage("'{PropertyName}' é um endereço de e-mail inválido.");

        public static IRuleBuilderOptions<T, string> CpfValido<T>(this IRuleBuilder<T, string> ruleBuilder) =>
           ruleBuilder
          .Must(CPFValidator.Validar)
          .WithMessage("'{PropertyName}' o cpf informado não é valido.");

        public static IRuleBuilderOptions<T, DateTime> DataNascimentoValida<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
        {
            return ruleBuilder
                .Must(DataValidator.ValidarDataNascimento)
                .WithMessage("A data de nascimento deve ser válida e não pode estar no futuro ou além de 120 anos.");
        }
    }
}
