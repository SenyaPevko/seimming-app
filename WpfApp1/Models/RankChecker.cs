namespace WpfApp1.Models;

public static class RankChecker
{
    public static bool CheckRank(Athlete athlete)
    {
        if (string.IsNullOrEmpty(athlete.Competition) || string.IsNullOrEmpty(athlete.Gender) ||
            string.IsNullOrEmpty(athlete.Discipline) || string.IsNullOrEmpty(athlete.WantedRank))
        {
            return false; // Несоответствие минимальным требованиям
        }

        return athlete.WantedRank switch
        {
            Rank.MSMK => CheckMSMK(athlete),
            Rank.MS => CheckMS(athlete),
            Rank.KMS => CheckKMS(athlete),
            _ => false
        };
    }

    private static bool CheckMSMK(Athlete athlete)
    {
        if (athlete.Age < 14)
            return false;

        return athlete.Competition switch
        {
            Competition.OlympicGames => 
                athlete.Place is >= 1 and <= 8 && athlete.Discipline.IsRelay(),
            Competition.WorldChampionships =>
                (athlete.Place is >= 1 and <= 6 && athlete.Discipline.IsRelay()) ||
                (athlete.Place is >= 1 and <= 7 && athlete.Discipline.IsOpenWater()),
            Competition.EuropeanChampionships => 
                (athlete.Place is >= 1 and <= 3 && athlete.Discipline.IsRelay()) ||
                (athlete.Place is >= 1 and <= 6 && athlete.Discipline.IsOpenWater()),
            Competition.OtherInternationalCompetitionsECP => 
                (athlete.Place is >= 1 and <= 3 && (athlete.Discipline.IsRelay() || athlete.Discipline.IsOpenWater())) ||
                // TODO: *Условие: если спортивное соревнование проводится по регламенту Кубка мира
                (athlete.Place == 1 && (athlete.Discipline.IsRelay() || athlete.Discipline.IsOpenWater())),
            _ => false
        };
    }
    
    private static bool CheckMS(Athlete athlete)
    {
        if (athlete.Age < 14)
            return false;

        return athlete.Competition switch
        {
            Competition.RussianChampionship => 
                athlete is { Discipline: Discipline.OpenWater5km, TimeDifference: <= 10, Place: >= 1 and <= 8 } ||
                athlete is { Discipline: Discipline.OpenWater10km, TimeDifference: <= 20, Place: >= 1 and <= 8 } ||
                athlete is { Discipline: Discipline.OpenWater16km, TimeDifference: <= 32, Place: >= 1 and <= 8 } ||
                athlete is { Discipline: Discipline.OpenWater25kmAndMore, TimeDifference: <= 50, Place: >= 1 and <= 8 } ||
                // TODO: **Условие: участие не менее 10 эстафетных команд
                athlete is { Discipline: Discipline.OpenWaterRelay4x1250mMixed, Place: 1 },
            Competition.RussianCup => 
                athlete is { Discipline: Discipline.OpenWater5km, Place: 1 } ||
                athlete is { Discipline: Discipline.OpenWater10km, Place: 1 } ||
                athlete is { Discipline: Discipline.OpenWater16km, Place: 1 } ||
                athlete is { Discipline: Discipline.OpenWater25kmAndMore, Place: 1 } ||
                // TODO: **Условие: участие не менее 10 эстафетных команд
                athlete is { Discipline: Discipline.OpenWaterRelay4x1250mMixed, Place: 1 },
            Competition.RussianFirstChampionship => 
                athlete is { Age: >= 18 and <= 19, Discipline: Discipline.OpenWater10km, Place: >= 1 and <= 6, TimeDifference: <= 20 } ||
                // TODO: **Условие: участие не менее 10 эстафетных команд
                athlete is { Age: >= 14 and <= 19, Discipline: Discipline.OpenWaterRelay4x1250mMixed, Place: 1 },
            Competition.OtherAllRussianCompetitionsECP => 
                athlete is { Discipline: Discipline.OpenWater5km, Place: 1 },
            _ => false
        };
    }
    
    private static bool CheckKMS(Athlete athlete)
    {
        if (athlete.Age < 14)
            return false;

        return athlete.Competition switch
        {
            Competition.RussianCup => 
                athlete is { Discipline: Discipline.OpenWater5km, TimeDifference: <= 10, Place: >= 2 and <= 3 } ||
                athlete is { Discipline: Discipline.OpenWater10km, TimeDifference: <= 20, Place: >= 1 and <= 6 } ||
                athlete is { Discipline: Discipline.OpenWater16km, TimeDifference: <= 32, Place: >= 1 and <= 6 } ||
                athlete is { Discipline: Discipline.OpenWater25kmAndMore, TimeDifference: <= 50, Place: >= 1 and <= 6 } ||
                // TODO: **Условие: участие не менее 10 эстафетных команд
                athlete is { Discipline: Discipline.OpenWaterRelay4x1250mMixed, Place: >= 1 and <= 6 },
            Competition.RussianFirstChampionship => 
                athlete is { Age: >= 18 and <= 19, Discipline: Discipline.OpenWater10km, Place: >= 7 and <= 10, TimeDifference: <= 20 } ||
                athlete is { Age: >= 16 and <= 17, Discipline: Discipline.OpenWater7_5km, Place: >= 1 and <= 8, TimeDifference: <= 15 } ||
                athlete is { Age: >= 14 and <= 15, Discipline: Discipline.OpenWater5km, Place: >= 1 and <= 6, TimeDifference: <= 10 } ||
                // TODO: **Условие: участие не менее 10 эстафетных команд
                athlete is { Age: >= 14 and <= 19, Discipline: Discipline.OpenWaterRelay4x1250mMixed, Place: >= 2 and <= 3 } ||
                // TODO: **Условие: участие не менее 10 эстафетных команд
                athlete is { Age: >= 14 and <= 16, Discipline: Discipline.OpenWaterRelay4x1250mMixed, Place: >= 1 and <= 3 },
            Competition.OtherAllRussianCompetitionsECP => 
                athlete is { Discipline: Discipline.OpenWater5km, Place: >= 2 and <= 3, TimeDifference: <= 10 },
            _ => false
        };
    }
}