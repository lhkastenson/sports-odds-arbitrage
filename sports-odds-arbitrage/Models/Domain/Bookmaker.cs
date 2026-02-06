public sealed record Bookmaker
{
  public string Key { get; init; } = string.Empty;
  public string Title { get; init; } = string.Empty;
  public DateTimeOffset LastUpdate { get; init; }
  public List<Market> Markets { get; init; } = [];
}