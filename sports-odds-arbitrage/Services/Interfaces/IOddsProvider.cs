namespace sports_odds_arbitrage.Services.Interfaces;

public interface IOddsProvider
{
  string ProviderName { get; }
  Task<IReadOnlyList<SportEvent>> GetOddsAsync(string sportKey, CancellationToken ct = default);
}