namespace sports_odds_arbitrage.Configuration;

public sealed class OddsApiSettings
{
    public string ApiKey { get; init; } = string.Empty;
    public string BaseUrl { get; init; } = "https://api.the-odds-api.com";
    public string DefaultRegions { get; init; } = "us";
    public string DefaultMarkets { get; init; } = "h2h,spreads,totals";
    public string OddsFormat { get; init; } = "american";
    public List<string> DefaultSports { get; init; } = [];
}
