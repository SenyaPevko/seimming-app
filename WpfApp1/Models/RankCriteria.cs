namespace WpfApp1.Models;

public class RankCriteria
{
    public int MinPlace { get; set; }
    public int? MaxPlace { get; set; }
    public TimeSpan? MaxTimeDifferenceFromWinner { get; set; }
    public int MinRelayTeams { get; set; }
    public bool RequiresElectronicTiming { get; set; }
}