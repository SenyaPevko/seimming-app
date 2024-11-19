namespace WpfApp1.Models;

public class Competition
{
    public const string OlympicGames = "Игры Олимпиады";
    public const string WorldChampionships = "Чемпионат мира";
    public const string EuropeanChampionships = "Чемпионат Европы";
    public const string OtherInternationalCompetitionsECP = "Другие международные соревнования (ЕКП)";
    public const string RussianChampionship = "Чемпионат России";
    public const string RussianCup = "Кубок России";
    public const string RussianFirstChampionship = "Первенство России";
    public const string OtherAllRussianCompetitionsECP = "Другие всероссийские соревнования (ЕКП)";
    
    public static List<string> All =
    [
        OlympicGames,
        WorldChampionships,
        EuropeanChampionships,
        OtherInternationalCompetitionsECP,
        RussianChampionship,
        RussianCup,
        RussianFirstChampionship,
        OtherAllRussianCompetitionsECP
    ];
}