using Microsoft.Extensions.Caching.Memory;
using TerevintoSoftware.AspNetCore.Authentication.ApiKeys.Abstractions;

namespace Assessment.DataAccess.ApiKey
{
    public class CacheService : IApiKeysCacheService
    {
        private static readonly TimeSpan _cacheKeysTimeToLive = new(1, 0, 0);
        private readonly IMemoryCache _memoryCache;
        private readonly IApiKey.IApiKey.IClientsService _clientsService;

        public CacheService(IMemoryCache memoryCache, IApiKey.IApiKey.IClientsService clientsService)
        {
            _memoryCache = memoryCache;
            _clientsService = clientsService;
        }
        public async ValueTask<string?> GetOwnerIdFromApiKey(string apiKey)
        {
            if (!_memoryCache.TryGetValue<Dictionary<string, Guid>>("Authentication_ApiKeys", out Dictionary<string, Guid>? internalKeys))
            {
                internalKeys = await _clientsService.GetActiveClients();

                _ = _memoryCache.Set("Authentication_ApiKeys", internalKeys, _cacheKeysTimeToLive);
            }

            return !internalKeys.TryGetValue(apiKey, out Guid clientId) ? null : clientId.ToString();
        }
        public async Task InvalidateApiKey(string apiKey)
        {
            if (_memoryCache.TryGetValue<Dictionary<string, Guid>>("Authentication_ApiKeys", out Dictionary<string, Guid>? internalKeys))
            {
                if (internalKeys.ContainsKey(apiKey))
                {
                    _ = internalKeys.Remove(apiKey);
                    _ = _memoryCache.Set("Authentication_ApiKeys", internalKeys);
                }
            }

            await _clientsService.InvalidateApiKey(apiKey);
        }
    }
}
