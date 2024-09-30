using Sistema.Cadastro.Infrastructure.ExternalServices.Smtp.Interfaces;
using Sistema.Cadastro.Infrastructure.ExternalServices.Smtp.Options;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

namespace Sistema.Cadastro.Infrastructure.ExternalServices.Smtp
{
    public class SmtpService : IEnvioEmailService
    {
        private readonly SmtpOptions _options;
        private readonly string Section = "Smtp";

        public SmtpService(IConfiguration configuration)
        {
            _options = configuration.GetSection(Section).Get<SmtpOptions>() 
                ?? throw new ArgumentNullException(nameof(Section), "Erro ao resgatar configs SMTP.");
        }

        public async void EnviarEmail(string email, string subject, string body)
        {
            using var client = new SmtpClient(_options.Server, _options.Port)
            {
                Credentials = new NetworkCredential(_options.From, _options.Passwd),
                EnableSsl = true 
            };

            var mailMessage = new MailMessage(_options.From, email, subject, body);

            await client.SendMailAsync(mailMessage);
        }
    }
}
