using Sistema.Cadastro.CrossCutting.Common.Abstractions;
using Sistema.Cadastro.CrossCutting.Common.Options;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace Sistema.Cadastro.Infrastructure.Cache
{
    public class RedisCache : IRedisCache
    {
        private readonly IDistributedCache _distributedCache;

        private readonly ILogger<RedisCache> _logger;

        private DistributedCacheEntryOptions _options;
        public RedisCache(IDistributedCache distributedCache, ILogger<RedisCache> logger, IOptions<CacheOptions> options)
        {
            _options = new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(options.Value.DefaultExpirationTime)
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
                _logger.LogError($"[EXCEPTION][REDIS][GET] => Erro ao definir cache para chave {key}. Erro: {ex}", key);
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
                _logger.LogError($"[EXCEPTION][REDIS][GET] => Erro ao definir cache com TTL para chave {key}. Erro: {ex}", key);
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
                _logger.LogError($"[EXCEPTION][REDIS][GET] => Erro ao resgatar cache para chave {key}. Erro: {ex}", key);
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
                _logger.LogError($"[EXCEPTION][REDIS][REMOVE] => Exception {ex}");
            }
        }
    }
}
