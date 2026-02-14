using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using sports_odds_arbitrage.Services.Interfaces;

namespace sports_odds_arbitrage.Controllers;

[ApiController]
public class OddsController(IOddsAggregatorService oddsAggregatorService) : ControllerBase
{
  [HttpGet("api/odds/{sportKey}")]
  public async Task<ActionResult<IReadOnlyCollection<SportEvent>>> GetAggregatedOddsForSports([FromRoute] [Required] string sportKey, CancellationToken ct)
  {
    if (!SportKeys.ValidKeys.Contains(sportKey))
    {
      return NotFound($"Sport {sportKey} is not supported.");
    }
    var sportEvents = await oddsAggregatorService.GetAggregatedOddsAsync(sportKey, ct);
    return Ok(sportEvents);
  }
}