public sealed record SportEvent
{
  public string Id { get; init; } = string.Empty;
  public string SportKey { get; init; } = string.Empty;
  public string SportTitle { get; init; } = string.Empty;
  public DateTimeOffset CommenceTime { get; init; }
  public string HomeTeam { get; init; } = string.Empty;
  public string AwayTeam { get; init; } = string.Empty;
  public List<Bookmaker> Bookmakers { get; init; } = [];
}