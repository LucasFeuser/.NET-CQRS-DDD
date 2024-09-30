namespace Sistema.Cadastro.CrossCutting.Common.Abstractions
{
    public interface IBaseHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string requestUri);
        Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content);
    }
}
