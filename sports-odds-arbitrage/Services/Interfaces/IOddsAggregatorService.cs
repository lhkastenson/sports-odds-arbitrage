namespace sports_odds_arbitrage.Services.Interfaces;

public interface IOddsAggregatorService
{
  Task<IReadOnlyList<SportEvent>> GetAggregatedOddsAsync(string sportKey, CancellationToken ct = default);
  Task<IReadOnlyList<SportEvent>> GetAggregatedOddsForMultipleSportsAsync(IEnumerable<string> sportKeys, CancellationToken ct);
}