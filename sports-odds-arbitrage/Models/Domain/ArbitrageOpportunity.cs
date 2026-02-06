public sealed record ArbitrageOpportunity
{
    public string EventId { get; init; } = string.Empty;
    public string SportKey { get; init; } = string.Empty;
    public string HomeTeam { get; init; } = string.Empty;
    public string AwayTeam { get; init; } = string.Empty;
    public DateTimeOffset CommenceTime { get; init; }
    public string MarketKey { get; init; } = string.Empty;
    public decimal TotalImpliedProbability { get; init; }
    public decimal ProfitMarginPercent { get; init; }
    public List<ArbitrageLeg> Legs { get; init; } = [];
    public DateTimeOffset DetectedAt { get; init; }
}