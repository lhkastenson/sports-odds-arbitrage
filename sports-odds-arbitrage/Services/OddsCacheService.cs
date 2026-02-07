using System.Collections.Concurrent;
using sports_odds_arbitrage.Services.Interfaces;

namespace sports_odds_arbitrage.Services;

public sealed class OddsCacheService : IOddsCacheService, IDisposable
{
  private ConcurrentDictionary<string, CacheEntry> _cache = new ConcurrentDictionary<string, CacheEntry>();
  private readonly ConcurrentDictionary<string, SemaphoreSlim> _locks = new ConcurrentDictionary<string, SemaphoreSlim>();

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

          cacheEntry = new CacheEntry(result, DateTimeOffset.UtcNow.Add(expiration.GetValueOrDefault(new TimeSpan(0, 10, 0))));
          _cache.TryAdd(cacheKey, cacheEntry);
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