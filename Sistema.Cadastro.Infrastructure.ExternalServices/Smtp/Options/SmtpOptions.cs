namespace Sistema.Cadastro.Infrastructure.ExternalServices.Smtp.Options
{
    public class SmtpOptions
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string From { get; set; }
        public string Passwd { get; set; }
    }
}
