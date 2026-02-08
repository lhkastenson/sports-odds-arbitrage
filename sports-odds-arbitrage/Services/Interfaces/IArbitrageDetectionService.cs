namespace sports_odds_arbitrage.Services.Interfaces;

public interface IArbitrageDetectionService
{
  IReadOnlyCollection<ArbitrageOpportunity> DetectArbitrage(IReadOnlyList<SportEvent> events);
}