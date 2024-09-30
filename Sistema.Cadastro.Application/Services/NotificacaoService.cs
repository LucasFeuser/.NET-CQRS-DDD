using Sistema.Cadastro.Domain.Clientes.Paciente.DTOs;
using Sistema.Cadastro.Application.Services.Interfaces;
using Sistema.Cadastro.Infrastructure.ExternalServices.Smtp.Interfaces;

namespace Sistema.Cadastro.Application.Services
{
    public class NotificacaoService : INotificacaoService
    {
        private readonly IEnvioEmailService _envioEmailService;

        public NotificacaoService(IEnvioEmailService envioEmailService)
        {
            _envioEmailService = envioEmailService;
        }

        public async void EnviarNotificacoesNovoCadastro(PacienteDto dto)
        {
            _envioEmailService.EnviarEmail(dto.Email, $"Sejá bem vindo @{dto.NomeCompleto}", "Agora você é um de nossos clientes!!");
            NotificarEquipeMedica();
            NotificarOutrosServicos();
        }

        private void NotificarEquipeMedica() { }
        private void NotificarOutrosServicos() { }
    }
}
