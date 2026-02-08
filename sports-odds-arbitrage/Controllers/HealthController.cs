using Microsoft.AspNetCore.Mvc;

namespace sports_odds_arbitrage.Controllers;

[ApiController]
public class HealthController() : ControllerBase
{
  [Route("api/healthcheck")]
  [HttpGet]
  public ActionResult<object> HealthCheck(CancellationToken ct)
  {
    return Ok(new {StatusCode = "healthy", Timestamp = DateTimeOffset.Now});
  }
}