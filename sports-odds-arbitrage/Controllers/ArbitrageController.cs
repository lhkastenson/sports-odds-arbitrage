using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using sports_odds_arbitrage.Services.Interfaces;

namespace sports_odds_arbitrage.Controllers;

[ApiController]
public class ArbitrageController(IOddsAggregatorService oddsAggregatorService, 
                                 IArbitrageDetectionService arbitrageDetectionService) : ControllerBase
{
  [HttpGet("api/arbitrageOpportunity/{sportKey}")]
  public async Task<ActionResult<IReadOnlyCollection<ArbitrageOpportunity>>> GetArbitrageOpportunity([FromRoute] [Required] string sportKey, CancellationToken ct)
  {
    if (!SportKeys.ValidKeys.Contains(sportKey))
    {
      return NotFound($"Sport {sportKey} is not supported.");
    }
    var sportEvents = await oddsAggregatorService.GetAggregatedOddsAsync(sportKey, ct);
    var arbitrageOpportunities = arbitrageDetectionService.DetectArbitrage(sportEvents);
    return Ok(arbitrageOpportunities);
  }
}