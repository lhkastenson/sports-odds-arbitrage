namespace sports_odds_arbitrage.Data.Entities;

public sealed class OddsSnapshotEntity
{
  public int Id { get; set; }
  public int SportEventId { get; set;} //FK
  public string BookmakerKey { get; set; } = string.Empty;
  public string BookmakerTitle { get; set; } = string.Empty;
  public string MarketKey { get; set; } = string.Empty;
  public string OutcomeName { get; set; } = string.Empty;
  public int Price { get; set; }
  public decimal? Point { get; set; }
  public DateTimeOffset CapturedAt { get; set; }

  // Navigation
  public SportEventEntity SportEvent { get; set; } = null!;
}