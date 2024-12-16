using Microsoft.Extensions.Logging;
namespace JewelArchitecture.Examples.FunPokedex.Infrastructure.Caching;

public class FileStreamCachingService(ILogger<FileStreamCachingService> logger) : IStreamCachingService
{
    private const string CacheEntryPointFolder = ".Cache";

    public bool TryGetFromCache(string cacheTopic, string cacheKey, TimeSpan cacheDuration,
        out Stream? cachedResponse)
    {
        cachedResponse = null;

        var cachedFilePath = Path.Combine(CacheEntryPointFolder, cacheTopic, cacheKey);
        if (!File.Exists(cachedFilePath) || IsCacheExpired(cachedFilePath, cacheDuration) == true)
        {
            return false;
        }

        cachedResponse = new FileStream(cachedFilePath, FileMode.Open, FileAccess.Read);
        return true;
    }

    public async Task CacheResponseAsync(string cacheTopic, string cacheKey, MemoryStream response)
    {
        if (!Directory.Exists(CacheEntryPointFolder))
        {
            Directory.CreateDirectory(CacheEntryPointFolder);
        }
        var cachePath = Path.Combine(CacheEntryPointFolder, cacheTopic);
        if (!Directory.Exists(cachePath))
        {
            Directory.CreateDirectory(cachePath);
        }

        string filePath = Path.Combine(cachePath, cacheKey);
        try
        {
            // Reset the memory stream position to the beginning
            response.Position = 0;
            using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);

            await response.CopyToAsync(fileStream);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, exception.Message);
        }
    }

    private bool IsCacheExpired(string cachedFilePath, TimeSpan cacheDuration)
    {
        return DateTime.UtcNow - File.GetCreationTimeUtc(cachedFilePath)
            > cacheDuration;
    }
}
