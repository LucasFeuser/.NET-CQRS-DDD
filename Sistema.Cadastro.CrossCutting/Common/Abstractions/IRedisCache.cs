namespace Sistema.Cadastro.CrossCutting.Common.Abstractions
{
    public interface IRedisCache
    {
        Task SetAsync<TInput>(string key, TInput input) where TInput : class;
        Task SetWithTTLAsync<TInput>(string key, TInput input, TimeSpan cacheExpiration) where TInput : class;
        Task<TOutput?> GetAsync<TOutput>(string key) where TOutput : class;
        Task RemoveAsync(string key);
    }
}
