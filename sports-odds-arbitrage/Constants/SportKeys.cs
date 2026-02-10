public static class SportKeys
{
  public const string NFL = "americanfootball_nfl";
  public const string NCAAB = "basketball_ncaab";
  public const string NHL = "icehockey_nhl";
  public const string AHL = "icehockey_ahl";

  public static readonly HashSet<string> ValidKeys = [NFL, NCAAB, AHL, NHL];
}