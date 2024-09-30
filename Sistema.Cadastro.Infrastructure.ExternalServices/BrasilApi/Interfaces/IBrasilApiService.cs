using Sistema.Cadastro.Infrastructure.ExternalServices.BrasilApi.DTOs;

namespace Sistema.Cadastro.Infrastructure.ExternalServices.BrasilApi.Interfaces
{
    public interface IBrasilApiService
    {
        Task<BuscaCepResponse> ObterDadosEnderecoPorCep(string cep);
    }
}
