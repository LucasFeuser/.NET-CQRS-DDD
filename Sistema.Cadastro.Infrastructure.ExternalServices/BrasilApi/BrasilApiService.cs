using Sistema.Cadastro.Infrastructure.ExternalServices.BrasilApi.Interfaces;
using Sistema.Cadastro.Infrastructure.ExternalServices.BrasilApi.DTOs;
using Sistema.Cadastro.CrossCutting.Common.Abstractions;
using System.Text.Json;
using System.Net;

namespace Sistema.Cadastro.Infrastructure.ExternalServices.BrasilApi
{
    public class BrasilApiService : IBrasilApiService
    {
        private readonly IBaseHttpClient _baseHttpClient;

        public BrasilApiService(IBaseHttpClient baseHttpClient)
        {
            _baseHttpClient = baseHttpClient;
        }

        /// <summary>
        /// Consulta endereco BrasilAPI
        /// </summary>
        /// <typeparam name="T">Evento</typeparam>
        /// <param name="evento"></param>
        /// <returns>
        /// {
        ///  "cep": "string",
        ///  "state": "string",
        ///  "city": "string",
        ///  "neighborhood": "string",
        ///  "street": "string",
        ///  "service": "viacep"
        /// }
        /// </returns>
        public async Task<BuscaCepResponse> ObterDadosEnderecoPorCep(string cep)
        {
            BuscaCepResponse response = null!;

            var message = await _baseHttpClient.GetAsync($"https://brasilapi.com.br/api/cep/v1/{int.Parse(cep)}");
            if (message.IsSuccessStatusCode)
            {
                var json = await message.Content.ReadAsStringAsync();
                response = JsonSerializer.Deserialize<BuscaCepResponse>(json);

                return response;
            }            

            return response;
        }
    }
}
