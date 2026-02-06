using Microsoft.EntityFrameworkCore;
using sports_odds_arbitrage.Data.Entities;

namespace sports_odds_arbitrage.Data;

public class OddsDbContext : DbContext
{
  public OddsDbContext(DbContextOptions<OddsDbContext> options) : base(options) { }

  public DbSet<SportEventEntity> SportEvents => Set<SportEventEntity>();
  public DbSet<OddsSnapshotEntity> OddsSnapshots => Set<OddsSnapshotEntity>();
  public DbSet<ArbitrageOpportunityEntity> ArbitrageOpportunities => Set<ArbitrageOpportunityEntity>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<SportEventEntity>(e =>
    {
      e.HasIndex(x => x.ExternalId).IsUnique();
      e.HasIndex(x => x.SportKey);
    });

    modelBuilder.Entity<OddsSnapshotEntity>(e =>
    {
      e.HasIndex(x => x.CapturedAt);
    });

    modelBuilder.Entity<ArbitrageOpportunityEntity>(e =>
    {
      e.HasIndex(x => x.DetectedAt);
    });
  }
}