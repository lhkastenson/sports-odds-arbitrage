using sports_odds_arbitrage.Services.Interfaces;

namespace sports_odds_arbitrage.Services;

public sealed class ArbitrageDetectionService : IArbitrageDetectionService
{
  public IReadOnlyCollection<ArbitrageOpportunity> DetectArbitrage(IReadOnlyList<SportEvent> events)
  {
    var opportunities = new List<ArbitrageOpportunity>();

    foreach (var sportEvent in events)
    {
      // Step 1: Flatten all bookmakers' markets into a single stream of tuples.
      var allOutcomes = sportEvent.Bookmakers
        .SelectMany(bookmaker => bookmaker.Markets
          .SelectMany(market => market.Outcomes
            .Select(outcome => new
            {
              Bookmaker = bookmaker,
              MarketKey = market.Key,
              Outcome = outcome
            })));

      // Step 2: Group by market key ex "h2h", "spread"
      var marketGroups = allOutcomes.GroupBy(x => x.MarketKey);

      foreach (var marketGroup in marketGroups)
      {
        // Step 3: Within this market, group by outcome name (e.g., "Milwaukee Admirals").
        // Each group will contain the odds every bookmaker is offering for that same outcome.
        var outcomeGroups = marketGroup.GroupBy(x => x.Outcome.Name);

        // Step 4: For each outcome, find the BEST price across all bookmakers.
        // "Best" means highest numerical value for American odds:
        //   +160 is better than +150 (bigger payout for underdogs)
        //   -170 is better than -195 (less risk for favorites)
        var legs = outcomeGroups.Select(outcomeGroup =>
        {
          var bestEntry = outcomeGroup
            .OrderByDescending(x => x.Outcome.Price)
            .First();

          decimal impliedProb = CalculateImpliedProbability(bestEntry.Outcome.Price);

          return new ArbitrageLeg
          {
            OutcomeName = bestEntry.Outcome.Name,
            BookmakerKey = bestEntry.Bookmaker.Key,
            BookmakerTitle = bestEntry.Bookmaker.Title,
            BestPrice = bestEntry.Outcome.Price,
            ImpliedProbability = impliedProb,
            Point = bestEntry.Outcome.Point
            // StakePercent gets calculated below once we know the total
          };
        }).ToList();

        // Step 5: Sum up implied probabilities across all outcomes.
        // If this total is < 1.0, the bookmakers' odds are inconsistent
        // in our favor — that's the arbitrage opportunity.
        decimal totalImpliedProbability = legs.Sum(l => l.ImpliedProbability);

        if (totalImpliedProbability < 1.0m)
        {
          // Step 6: Calculate how to split your bankroll across each leg.
          // Each leg's stake is proportional to its share of the total probability.
          // This ensures equal profit regardless of which outcome wins.
          var legsWithStakes = legs.Select(leg => leg with
          {
            StakePercent = leg.ImpliedProbability / totalImpliedProbability * 100
          }).ToList();

          decimal profitMargin = (1.0m - totalImpliedProbability) * 100;

          opportunities.Add(new ArbitrageOpportunity
          {
            EventId = sportEvent.Id,
            SportKey = sportEvent.SportKey,
            HomeTeam = sportEvent.HomeTeam,
            AwayTeam = sportEvent.AwayTeam,
            CommenceTime = sportEvent.CommenceTime,
            MarketKey = marketGroup.Key,
            TotalImpliedProbability = totalImpliedProbability,
            ProfitMarginPercent = profitMargin,
            Legs = legsWithStakes,
            DetectedAt = DateTimeOffset.UtcNow
          });
        }
      }
    }

    return opportunities;
  }

  /// Converts American odds to implied probability (0.0 to 1.0).
  /// Negative odds (favorites): |odds| / (|odds| + 100)  →  -175 = 175/275 = 0.6364
  /// Positive odds (underdogs): 100 / (odds + 100)        →  +135 = 100/235 = 0.4255
  private static decimal CalculateImpliedProbability(int americanOdds)
  {
    if (americanOdds > 0)
    {
      return 100m / (americanOdds + 100m);
    }

    decimal absOdds = Math.Abs(americanOdds);
    return absOdds / (absOdds + 100m);
  }
}
