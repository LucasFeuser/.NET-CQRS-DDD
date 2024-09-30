using Sistema.Cadastro.CrossCutting.Common.Abstractions;

namespace Sistema.Cadastro.Infrastructure.ExternalServices.Common.HttpClients
{
    public class BaseHttpClient : IBaseHttpClient
    {
        private readonly HttpClient _httpClient;

        public BaseHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetAsync(string uri)
        {
            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> PostAsync(string uri, HttpContent content)
        {
            var response = await _httpClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
