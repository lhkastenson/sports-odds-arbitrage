namespace sports_odds_arbitrage.Data.Entities;

public sealed class ArbitrageOpportunityEntity
{
  public int Id { get; set; }
  public int SportEventId { get; set; } // FK
  public string MarketKey { get; set; } = string.Empty;
  public decimal TotalImpliedProbability { get; set; }
  public decimal ProfitMarginPercent { get; set; }
  public string LegsJson { get; set; } = string.Empty; //serialized legs
  public DateTimeOffset DetectedAt { get; set; }

  // Navigation
  public SportEventEntity SportEvent { get; set; } = null!;
}