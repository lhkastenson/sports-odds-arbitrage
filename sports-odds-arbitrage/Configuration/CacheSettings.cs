namespace sports_odds_arbitrage.Configuration;

public sealed class CacheSettings
{
    public int OddsCacheDurationSeconds { get; init; } = 300;
    public int SportsCacheDurationSeconds { get; init; } = 3600;
    public int MaxConcurrentRefreshes { get; init; } = 3;
}
