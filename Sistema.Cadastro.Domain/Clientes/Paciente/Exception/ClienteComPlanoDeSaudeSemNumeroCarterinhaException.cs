using Sistema.Cadastro.CrossCutting.Common.Exceptions;

namespace Sistema.Cadastro.Domain.Clientes.Paciente.Exception
{
    public class ClienteComPlanoDeSaudeSemNumeroCarterinhaException : DomainException
    {
        public ClienteComPlanoDeSaudeSemNumeroCarterinhaException() 
            : base("O número da carteirinha deve ser informado quando o paciente possui um plano de saúde.") { }
    }
}
