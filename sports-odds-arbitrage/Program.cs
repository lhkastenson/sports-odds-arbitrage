using Serilog;
using Microsoft.EntityFrameworkCore;
using sports_odds_arbitrage.Data;
using sports_odds_arbitrage.Configuration;
using sports_odds_arbitrage.Services.Interfaces;
using sports_odds_arbitrage.Services.Providers;
using sports_odds_arbitrage.Services;
using sports_odds_arbitrage.Middleware;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try {

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, config) => config
        .ReadFrom.Configuration(context.Configuration)
        .WriteTo.Console());

    builder.Services.Configure<OddsApiSettings>(
        builder.Configuration.GetSection("OddsApi"));
    builder.Services.Configure<CacheSettings>(
        builder.Configuration.GetSection("Cache"));

    builder.Services.AddDbContext<OddsDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddSingleton<IOddsCacheService, OddsCacheService>();
    builder.Services.AddSingleton<IOddsProvider, MockOddsProvider>();
    builder.Services.AddScoped<IOddsAggregatorService, OddsAggregatorService>();
    builder.Services.AddTransient<IArbitrageDetectionService, ArbitrageDetectionService>();

    var app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<OddsDbContext>();
        await db.Database.EnsureCreatedAsync();
    }

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseMiddleware<ExceptionHandlingMiddleware>();

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}