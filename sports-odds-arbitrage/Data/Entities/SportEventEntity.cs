namespace sports_odds_arbitrage.Data.Entities;

public sealed class SportEventEntity
{
  public int Id { get; set; }
  public string ExternalId { get; set; } = string.Empty;
  public string SportKey { get; set; } = string.Empty;
  public string SportTitle { get; set; } = string.Empty;
  public DateTimeOffset CommenceTime { get; set; }
  public string HomeTeam { get; set; } = string.Empty;
  public string AwayTeam { get; set; } = string.Empty;
  public DateTimeOffset CreatedAt { get; set; }
  public DateTimeOffset UpdatedAt { get; set; }

  // Navigation properties
  public ICollection<OddsSnapshotEntity> Snapshots { get; set; } = [];
  public ICollection<ArbitrageOpportunityEntity> ArbitrageOpportunities { get; set; } = [];
}