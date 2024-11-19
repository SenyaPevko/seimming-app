namespace WpfApp1.Models;
public static class RankChecker
{
    public static bool CheckRank(Athlete athlete)
    {
        if (athlete.Age < 14 || string.IsNullOrEmpty(athlete.Competition) || string.IsNullOrEmpty(athlete.Gender) ||
            string.IsNullOrEmpty(athlete.Discipline) || string.IsNullOrEmpty(athlete.WantedRank))
        {
            return false; // Несоответствие минимальным требованиям
        }

        switch (athlete.WantedRank)
        {
            case Rank.MSMK:
                return CheckMSMK(athlete);
            case Rank.MS:
                return CheckMS(athlete);
            case Rank.KMS:
                return CheckKMS(athlete);
            default:
                return false;
        }
    }

    private static bool CheckMSMK(Athlete athlete)
    {
        if (athlete.Age < 14) return false; // МСМК с 14 лет

        switch (athlete.Competition)
        {
            case Competition.OlympicGames:
                return athlete.Place >= 1 && athlete.Place <= 8 && athlete.Discipline.Contains("Эстафета");
            case Competition.WorldChampionships:
                return athlete.Place >= 1 && athlete.Place <= 6 && athlete.Discipline.Contains("Открытая вода");
            case Competition.EuropeanChampionships:
                return athlete.Place >= 1 && athlete.Place <= 3 && athlete.Discipline.Contains("Эстафета");
            case Competition.OtherInternationalCompetitionsECP:
                return athlete.Place == 1 || (athlete.Place <= 3 && athlete.Competition.Contains("Кубок мира"));
            default:
                return false;
        }
    }

    private static bool CheckMS(Athlete athlete)
    {
        if (athlete.Age < 12) return false; // МС с 12 лет

        switch (athlete.Competition)
        {
            case Competition.RussianChampionship:
                if (athlete.Discipline.Contains("Открытая вода"))
                {
                    return athlete.Place <= 8 && CheckTimeDifference(athlete);
                }
                return athlete.Place == 1 && athlete.Discipline.Contains("Эстафета");
            case Competition.RussianCup:
                if (athlete.Discipline.Contains("Открытая вода"))
                {
                    return athlete.Place == 1 && CheckTimeDifference(athlete);
                }
                return athlete.Place == 1 && athlete.Discipline.Contains("Эстафета");
            default:
                return false;
        }
    }

    private static bool CheckKMS(Athlete athlete)
    {
        if (athlete.Age < 10) return false; // КМС с 10 лет

        switch (athlete.Competition)
        {
            case Competition.RussianChampionship:
                return athlete.Place <= 8 && CheckTimeDifference(athlete);
            case Competition.RussianCup:
                return athlete.Place <= 3 && CheckTimeDifference(athlete);
            case Competition.RussianFirstChampionship:
                return CheckRussianFirstChampionship(athlete);
            case Competition.OtherAllRussianCompetitionsECP:
                return athlete.Place <= 3 && CheckTimeDifference(athlete);
            default:
                return false;
        }
    }

    private static bool CheckRussianFirstChampionship(Athlete athlete)
    {
        if (athlete.Gender == Gender.Male && athlete.Age >= 14 && athlete.Age <= 19)
        {
            if (athlete.Discipline.Contains("10 км") && athlete.Age >= 18 && athlete.Age <= 19)
            {
                return athlete.Place <= 10 && CheckTimeDifference(athlete, 20);
            }
            if (athlete.Discipline.Contains("7,5 км") && athlete.Age >= 16 && athlete.Age <= 17)
            {
                return athlete.Place <= 8 && CheckTimeDifference(athlete, 15);
            }
            if (athlete.Discipline.Contains("5 км") && athlete.Age >= 14 && athlete.Age <= 15)
            {
                return athlete.Place <= 6 && CheckTimeDifference(athlete, 10);
            }
        }
        return false;
    }

    private static bool CheckTimeDifference(Athlete athlete, int maxDifference = 50)
    {
        var distance = GetDistanceFromDiscipline(athlete.Discipline);
        switch (distance)
        {
            case 5: return athlete.Time <= athlete.Time - 10;
            case 10: return athlete.Time <= athlete.Time - 20;
            case 16: return athlete.Time <= athlete.Time - 32;
            case 25: return athlete.Time <= athlete.Time - maxDifference;
            default: return false;
        }
    }

    private static int GetDistanceFromDiscipline(string discipline)
    {
        if (discipline.Contains("5 км")) return 5;
        if (discipline.Contains("10 км")) return 10;
        if (discipline.Contains("16 км")) return 16;
        if (discipline.Contains("25 км")) return 25;
        return 0;
    }
}

