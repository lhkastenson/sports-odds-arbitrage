using sports_odds_arbitrage.Services.Interfaces;

namespace sports_odds_arbitrage.Services.Providers;

public class MockOddsProvider : IOddsProvider
{
  public string ProviderName { get; } = "MockOddsProvider";

  private Outcome testOutcomeDKMilwaukeeAdmirals = new Outcome { Name = "Milwaukee Admirals", Price = -175 };

  private Outcome testOutcomeDKRockfordIceHogs = new Outcome { Name = "Rockford IceHogs", Price = 135 };

  private Outcome testOutcomeFDMilwaukeeAdmirals = new Outcome { Name = "Milwaukee Admirals", Price = -195 };

  private Outcome testOutcomeFDRockfordIceHogs = new Outcome { Name = "Rockford IceHogs", Price = 130 };

  private Market testMarketDKAHL()
  {
    List<Outcome> outcomes = new List<Outcome>() { testOutcomeDKMilwaukeeAdmirals, testOutcomeDKRockfordIceHogs };
    return new Market
    {
      Key = "h2h",
      Outcomes = outcomes
    };
  }

  private Market testMarketFDAHL()
  {
    List<Outcome> outcomes = new List<Outcome>() { testOutcomeFDMilwaukeeAdmirals, testOutcomeFDRockfordIceHogs };
    return new Market
    {
      Key = "h2h",
      Outcomes = outcomes
    };
  }

  private Bookmaker testBookmakerDKAHL()
  {
    List<Market> markets = new List<Market>() { testMarketDKAHL() };
    return new Bookmaker
    {
      Key = "draftkings",
      Title = "DraftKings",
      LastUpdate = DateTimeOffset.Now,
      Markets = markets
    };
  }

  private Bookmaker testBookmakerFDAHL()
  {
    List<Market> markets = new List<Market>() { testMarketFDAHL() };
    return new Bookmaker
    {
      Key = "fanduel",
      Title = "Fan Duel",
      LastUpdate = DateTimeOffset.Now,
      Markets = markets
    };
  }

  private SportEvent testSportEventAHL()
  {
    List<Bookmaker> bookmakers = new List<Bookmaker>() { testBookmakerDKAHL(), testBookmakerFDAHL() };
    return new SportEvent
    {
      Id = "mock-ahl-001",
      SportKey = "icehockey_ahl",
      SportTitle = "AHL",
      HomeTeam = "Milwaukee Admirals",
      AwayTeam = "Rockford IceHogs",
      CommenceTime = DateTimeOffset.Now.AddHours(3),
      Bookmakers = bookmakers
    };
  }

  private Outcome testOutcomeDKAuburnTigers = new Outcome { Name = "Auburn Tigers", Price = -155 };

  private Outcome testOutcomeDKAlabama = new Outcome { Name = "Alabama Crimson Tide", Price = 130 };

  private Outcome testOutcomeFDAuburnTigers = new Outcome { Name = "Auburn Tigers", Price = -164 };

  private Outcome testOutcomeFDAlabama = new Outcome { Name = "Alabama Crimson Tide", Price = 136 };

  private Market testMarketNCAABDK()
  {
    List<Outcome> outcomes = new List<Outcome>() { testOutcomeDKAuburnTigers, testOutcomeDKAlabama };
    return new Market
    {
    Key = "h2h",
    Outcomes = outcomes
    };  
  }
  
  private Market testMarketNCAABFD()
  {
    List<Outcome> outcomes = new List<Outcome>() { testOutcomeFDAuburnTigers, testOutcomeFDAlabama};
    return new Market
    {
      Key = "h2h",
      Outcomes = outcomes
    };
  }

  private Bookmaker testBookmakerNCAABDK()
  {
    List<Market> markets = new List<Market>() { testMarketNCAABDK() };
    return new Bookmaker
    {
      Key = "draftkings",
      Title = "DraftKings",
      LastUpdate = DateTimeOffset.Now,
      Markets = markets
    };
  }

  private Bookmaker testBookmakerNCAABFD()
  {
    List<Market> markets = new List<Market>() { testMarketNCAABFD() };
    return new Bookmaker
    {
      Key = "fanduel",
      Title = "FanDuel",
      LastUpdate = DateTimeOffset.Now,
      Markets = markets
    };
  }

  private SportEvent testSportEventNCAAB()
  {
    List<Bookmaker> bookmakers = new List<Bookmaker>() { testBookmakerNCAABDK(), testBookmakerNCAABFD() };
    return new SportEvent
    {
      Id = "mock-ncaab-001",
      SportKey = "basketball_ncaab",
      SportTitle = "NCAAB",
      HomeTeam = "Auburn Tigers",
      AwayTeam = "Alabama Crimson Tide",
      Bookmakers = bookmakers
    };
  }

  private Outcome testOutcomeDKNewEngland = new Outcome { Name = "New England Patriots", Price = 190 };
  private Outcome testOutcomeDKSeattle = new Outcome { Name = "Seattle Seahawks", Price = -230 };

    private Outcome testOutcomeFDNewEngland = new Outcome { Name = "New England Patriots", Price = 188 };
  private Outcome testOutcomeFDSeattle = new Outcome { Name = "Seattle Seahawks", Price = -255 };

  private Market testMarketFootballDK()
  {
    List<Outcome> outcomes = new List<Outcome>() {testOutcomeDKNewEngland, testOutcomeDKSeattle};
    return new Market
    {
      Key = "h2h",
      Outcomes = outcomes
    };
  }

  private Market testMarketFootballFD()
  {
    List<Outcome> outcomes = new List<Outcome>() {testOutcomeFDNewEngland, testOutcomeFDSeattle};
    return new Market
    {
      Key = "h2h",
      Outcomes = outcomes
    };
  }

  private Bookmaker testBookmakerFootballDraftKings()
  {
    List<Market> markets = new List<Market>() {testMarketFootballDK()};
    return new Bookmaker
    {
      Key = "draftkings",
      Title = "DraftKings",
      LastUpdate = DateTimeOffset.Now,
      Markets = markets
    };
  }

  private Bookmaker testBookmakerFootballFanDuel()
  {
    List<Market> markets = new List<Market>() {testMarketFootballFD()};
    return new Bookmaker
    {
      Key = "fanduel",
      Title = "FanDuel",
      LastUpdate = DateTimeOffset.Now,
      Markets = markets
    };
  }

  private SportEvent testSportEventNFL()
  {
    Bookmaker draftKings = testBookmakerFootballDraftKings();
    Bookmaker fanduel = testBookmakerFootballFanDuel();
    List<Bookmaker> bookmakers = new List<Bookmaker> {draftKings, fanduel};
    return new SportEvent
    {
        Id = "mock-nfl-001",
        SportKey = "americanfootball_nfl",
        SportTitle = "NFL",
        CommenceTime = DateTimeOffset.Now.AddHours(3),
        HomeTeam = "New England Patriots",
        AwayTeam = "Seattle Seahawks",
        Bookmakers = bookmakers
    };
  }

  // NHL mock data — odds are intentionally set to produce an arbitrage opportunity.
  // DraftKings thinks Minnesota is the underdog (+115), FanDuel thinks Colorado is even (+100).
  // Best prices: Colorado +100 (FD), Minnesota +115 (DK)
  // Implied probabilities: 100/200 + 100/215 = 0.5000 + 0.4651 = 0.9651 → ~3.5% arb
  private Outcome testOutcomeDKColorado = new Outcome { Name = "Colorado Avalanche", Price = -120 };
  private Outcome testOutcomeDKMinnesota = new Outcome { Name = "Minnesota Wild", Price = 115 };
  private Outcome testOutcomeFDColorado = new Outcome { Name = "Colorado Avalanche", Price = 100 };
  private Outcome testOutcomeFDMinnesota = new Outcome { Name = "Minnesota Wild", Price = -110 };

  private Market testMarketNHLDK()
  {
    return new Market
    {
      Key = "h2h",
      Outcomes = new List<Outcome>() { testOutcomeDKColorado, testOutcomeDKMinnesota }
    };
  }

  private Market testMarketNHLFD()
  {
    return new Market
    {
      Key = "h2h",
      Outcomes = new List<Outcome>() { testOutcomeFDColorado, testOutcomeFDMinnesota }
    };
  }

  private Bookmaker testBookmakerNHLDK()
  {
    return new Bookmaker
    {
      Key = "draftkings",
      Title = "DraftKings",
      LastUpdate = DateTimeOffset.Now,
      Markets = new List<Market>() { testMarketNHLDK() }
    };
  }

  private Bookmaker testBookmakerNHLFD()
  {
    return new Bookmaker
    {
      Key = "fanduel",
      Title = "FanDuel",
      LastUpdate = DateTimeOffset.Now,
      Markets = new List<Market>() { testMarketNHLFD() }
    };
  }

  private SportEvent testSportEventNHL()
  {
    return new SportEvent
    {
      Id = "mock-nhl-001",
      SportKey = "icehockey_nhl",
      SportTitle = "NHL",
      HomeTeam = "Colorado Avalanche",
      AwayTeam = "Minnesota Wild",
      CommenceTime = DateTimeOffset.Now.AddHours(2),
      Bookmakers = new List<Bookmaker>() { testBookmakerNHLDK(), testBookmakerNHLFD() }
    };
  }

  public async Task<IReadOnlyList<SportEvent>> GetOddsAsync(string sportKey, CancellationToken ct = default)
  {
    Console.WriteLine($"Fetching results from mock service for key: {sportKey}...");
    List<SportEvent> mockData = new List<SportEvent>();
    await Task.Delay(200, ct);
    switch(sportKey)
    {
      case "americanfootball_nfl":
        mockData = new List<SportEvent>() { testSportEventNFL() };
        break;
      case "basketball_ncaab":
        mockData = new List<SportEvent>() { testSportEventNCAAB() };
        break;
      case "icehockey_ahl":
        mockData = new List<SportEvent>() { testSportEventAHL() };
        break;
      case "icehockey_nhl":
        mockData = new List<SportEvent>() { testSportEventNHL() };
        break;
      default:
        Console.WriteLine($"No results for {sportKey} from mock service");
        throw new ArgumentException($"The sportkey {sportKey} is not a valid argument for the mock service.");
    }
    return mockData;
  }
}