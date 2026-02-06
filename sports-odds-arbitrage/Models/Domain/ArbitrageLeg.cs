public sealed record ArbitrageLeg
{
    public string OutcomeName { get; init; } = string.Empty;
    public string BookmakerKey { get; init; } = string.Empty;
    public string BookmakerTitle { get; init; } = string.Empty;
    public int BestPrice { get; init; }
    public decimal ImpliedProbability { get; init; }
    public decimal? Point { get; init; }
    public decimal StakePercent { get; init; }
}