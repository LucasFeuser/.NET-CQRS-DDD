using Sistema.Cadastro.CrossCutting.Common.Abstractions;
using Sistema.Cadastro.CrossCutting.Common.Options;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Sistema.Cadastro.Infrastructure.Cache
{
    public class RedisCache : IRedisCache
    {
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<RedisCache> _logger;
        private DistributedCacheEntryOptions _options;
        private readonly string Section = "Redis";


        public RedisCache(IDistributedCache distributedCache, ILogger<RedisCache> logger, IConfiguration configuration)
        {
            _options = new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(configuration.GetSection(Section).Get<CacheOptions>().DefaultExpirationTime)
            };

            _distributedCache = distributedCache;
            _logger = logger;
        }

        public async Task SetAsync<TInput>(string key, TInput input) where TInput : class
        {
            try
            {
                await _distributedCache.SetAsync(key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(input)), _options);
            }
            catch (Exception ex)
            {
                _logger.LogError("[EXCEPTION][REDIS][SET] => Erro ao definir cache para chave {key}. Erro: {exception}", key, ex);
            }
        }


        public async Task SetWithTTLAsync<TInput>(string key, TInput input, TimeSpan cacheExpiration) where TInput : class
        {
            try
            {
                var options = new DistributedCacheEntryOptions
                {
                    SlidingExpiration = cacheExpiration
                };

                await _distributedCache.SetAsync(key, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(input)), options);
            }
            catch (Exception ex)
            {
                _logger.LogError("[EXCEPTION][REDIS][SET] => Erro ao definir cache com TTL para chave {key}. Erro: {exception}", key, ex);
            }
        }

        public async Task<TOutput?> GetAsync<TOutput>(string key) where TOutput : class
        {
            try
            {
                var bytes = await _distributedCache.GetAsync(key);

                return bytes != null ? JsonConvert.DeserializeObject<TOutput>(Encoding.UTF8.GetString(bytes)) : null;
            }
            catch (Exception ex)
            {
                _logger.LogError("[EXCEPTION][REDIS][GET] => Erro ao resgatar cache para chave {key}. Erro: {exception}", key, ex);
                return null;
            }
        }

        public async Task RemoveAsync(string key)
        {
            try
            {
                await _distributedCache.RemoveAsync(key);
            }
            catch (Exception ex)
            {
                _logger.LogError("[EXCEPTION][REDIS][REMOVE] => Erro ao deletar registro para chave {key}. Erro: {exception}", key, ex);
            }
        }
    }
}
