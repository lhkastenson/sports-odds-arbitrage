using System.Collections.Concurrent;
using sports_odds_arbitrage.Services.Interfaces;

namespace sports_odds_arbitrage.Services;

public sealed class OddsCacheService : IOddsCacheService, IDisposable
{
  private readonly ConcurrentDictionary<string, CacheEntry> _cache = new();
  private readonly ConcurrentDictionary<string, SemaphoreSlim> _locks = new();
  private const int EXPIRATION_MINUTES = 10;
  private sealed record CacheEntry(object Data, DateTimeOffset ExpiresAt)
  {
    public bool IsExpired => DateTimeOffset.UtcNow >= ExpiresAt;
  }

  public void Dispose()
  {
    foreach (var kvp in _locks)
      kvp.Value.Dispose();
  }

  public async Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> factory, TimeSpan? expiration = null)
  {
    if (!_cache.TryGetValue(cacheKey, out var cacheEntry) || cacheEntry.IsExpired)
    {
      SemaphoreSlim myLock = _locks.GetOrAdd(cacheKey, k => new SemaphoreSlim(1, 1));
      await myLock.WaitAsync();
      try
      {
        if (!_cache.TryGetValue(cacheKey, out cacheEntry) || cacheEntry.IsExpired)
        {
          T result = await factory();

          if (result == null)
            throw new InvalidOperationException($"Cache factory for {cacheKey} returned null");

          cacheEntry = new CacheEntry(result, DateTimeOffset.UtcNow.Add(expiration.GetValueOrDefault(TimeSpan.FromMinutes(EXPIRATION_MINUTES))));
          _cache.AddOrUpdate(cacheKey, cacheEntry, (key, old) => cacheEntry);
        }
      }
      finally
      {
        myLock.Release();
      }  
    }
    return (T)cacheEntry.Data;
    
  }
}