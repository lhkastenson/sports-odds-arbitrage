public sealed record Market
{
  public string Key { get; init; } = string.Empty;
  public List<Outcome> Outcomes { get; init; } = [];
}