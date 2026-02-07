namespace sports_odds_arbitrage.Services.Interfaces;

public interface IOddsCacheService
{
  Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> factory, TimeSpan? expiration = null);
}