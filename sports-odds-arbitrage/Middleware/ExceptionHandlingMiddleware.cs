using Microsoft.AspNetCore.Mvc;

namespace sports_odds_arbitrage.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await next(context);
    }
    catch (ArgumentException ex)
    {
      logger.LogWarning(ex, "Bad request for {Path}", context.Request.Path);
      await WriteProblemDetailsAsync(context, 400, "Bad Request", ex.Message);
    }
    catch (KeyNotFoundException ex)
    {
      logger.LogWarning(ex, "Resource not found for {Path}", context.Request.Path);
      await WriteProblemDetailsAsync(context, 404, "Not Found", ex.Message);
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Unhandled exception for {Path}", context.Request.Path);
      await WriteProblemDetailsAsync(context, 500, "Internal Server Error",
        "An unexpected error occurred. Please try again later.");
    }
  }

  private static async Task WriteProblemDetailsAsync(HttpContext context, int statusCode, string title, string detail)
  {
    context.Response.StatusCode = statusCode;
    context.Response.ContentType = "application/problem+json";

    var problemDetails = new ProblemDetails
    {
      Status = statusCode,
      Title = title,
      Detail = detail
    };

    await context.Response.WriteAsJsonAsync(problemDetails);
  }
}
