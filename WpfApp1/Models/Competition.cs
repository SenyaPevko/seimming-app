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
    
    public const string OtherMS = "Соревнования со статусом не ниже статуса первенства федерального округа, двух и более федеральных округов, первенств г. Москвы и г. Санкт-Петербурга";
    public const string OtherKMS1 = "Соревнования со статусом не ниже статуса других официальных спортивных соревнований субъекта Российской Федерации (за исключением официальных физкультурных мероприятий субъекта Российской Федерации)";
    
    public static List<string> All =
    [
        OlympicGames,
        WorldChampionships,
        EuropeanChampionships,
        OtherInternationalCompetitionsECP,
        RussianChampionship,
        RussianCup,
        RussianFirstChampionship,
        OtherAllRussianCompetitionsECP,
        OtherMS,
        OtherKMS1
    ];
}