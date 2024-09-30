namespace Sistema.Cadastro.Infrastructure.ExternalServices.Smtp.Interfaces
{
    public interface IEnvioEmailService
    {
        void EnviarEmail(string email, string subject, string body);
    }
}
