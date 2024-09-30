using Sistema.Cadastro.Domain.Clientes.Paciente.DTOs;

namespace Sistema.Cadastro.Application.Services.Interfaces
{
    public interface INotificacaoService
    {
        void EnviarNotificacoesNovoCadastro(PacienteDto dto);
    }
}
