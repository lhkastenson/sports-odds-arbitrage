public sealed record Outcome
{
  public string Name { get; init; } = string.Empty;
  public int Price { get; init; }
  public decimal? Point { get; init; }
}