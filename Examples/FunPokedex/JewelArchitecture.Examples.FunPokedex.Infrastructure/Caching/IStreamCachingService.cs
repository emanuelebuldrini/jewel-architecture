namespace JewelArchitecture.Examples.FunPokedex.Infrastructure.Caching;

public interface IStreamCachingService
{
    bool TryGetFromCache(string cacheTopic, string cacheKey, TimeSpan cacheDuration, out Stream? cachedResponse);
    Task CacheResponseAsync(string cacheTopic, string cacheKey, MemoryStream response);
}
