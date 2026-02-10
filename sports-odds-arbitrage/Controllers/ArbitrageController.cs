using Microsoft.AspNetCore.Mvc;
using sports_odds_arbitrage.Services.Interfaces;

namespace sports_odds_arbitrage.Controllers;

[ApiController]
public class ArbitrageController(IOddsAggregatorService oddsAggregatorService, 
                                 IArbitrageDetectionService arbitrageDetectionService) : ControllerBase
{
  [Route("api/arbitrageOpportunity/{sportKey}")]
  [HttpGet]
  public async Task<ActionResult<IReadOnlyCollection<ArbitrageOpportunity>>> GetArbitrageOpportunity(string sportKey, CancellationToken ct)
  {
    if (sportKey is null || sportKey.Length == 0)
    {
      return BadRequest("Sport key is required.");
    }
    if (!SportKeys.ValidKeys.Contains(sportKey))
    {
      return NotFound($"Sport {sportKey} is not supported.");
    }
    var sportEvents = await oddsAggregatorService.GetAggregatedOddsAsync(sportKey, ct);
    var arbitrageOpportunities = arbitrageDetectionService.DetectArbitrage(sportEvents);
    return Ok(arbitrageOpportunities);
  }
}