namespace WpfApp1.Models;

public class CompetitionRequirement
{
    public required string Competition { get; set; }
    public required string Discipline { get; set; }
    public required RankCriteria MsmkCriteria { get; set; }
    public required RankCriteria MsCriteria { get; set; }
    public required RankCriteria KmsCriteria { get; set; }
}