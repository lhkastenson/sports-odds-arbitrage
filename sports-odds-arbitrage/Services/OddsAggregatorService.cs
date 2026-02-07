using sports_odds_arbitrage.Services.Interfaces;

namespace sports_odds_arbitrage.Services;

public sealed class OddsAggregatorService(IOddsProvider oddsProvider, IOddsCacheService oddsCacheService) : IOddsAggregatorService
{
  public async Task<IReadOnlyList<SportEvent>> GetAggregatedOddsAsync(string sportKey, CancellationToken ct = default)
  {
    string cachedSportKey = $"odds:{sportKey}";
    return await oddsCacheService.GetOrCreateAsync(cachedSportKey, () => oddsProvider.GetOddsAsync(sportKey, ct));
  }

  public async Task<IReadOnlyList<SportEvent>> GetAggregatedOddsForMultipleSportsAsync(IEnumerable<string> sportKeys, CancellationToken ct)
  {
    var tasks = sportKeys.Select(sk => GetAggregatedOddsAsync(sk, ct));
    var results = await Task.WhenAll(tasks);
    return results.SelectMany(r => r).ToList();
  }
}