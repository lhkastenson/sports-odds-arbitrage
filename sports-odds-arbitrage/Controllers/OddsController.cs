using Microsoft.AspNetCore.Mvc;
using sports_odds_arbitrage.Services.Interfaces;

namespace sports_odds_arbitrage.Controllers;

[ApiController]
public class OddsController(IOddsAggregatorService oddsAggregatorService) : ControllerBase
{
  [Route("api/odds/{sportKey}")]
  [HttpGet]
  public async Task<ActionResult<IReadOnlyCollection<SportEvent>>> GetAggregatedOddsForSports(string sportKey, CancellationToken ct)
  {
    var sportEvents = await oddsAggregatorService.GetAggregatedOddsAsync(sportKey, ct);
    return Ok(sportEvents);
  }
}