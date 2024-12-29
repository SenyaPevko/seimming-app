namespace WpfApp1.Models;

public static class RankChecker
{
    public static string? CheckRank(Athlete athlete)
    {
        if (string.IsNullOrEmpty(athlete.Competition) || string.IsNullOrEmpty(athlete.Gender) ||
            string.IsNullOrEmpty(athlete.Discipline))
        {
            return null; // Несоответствие минимальным требованиям
        }

        if (CheckMSMK(athlete) || CheckMSMKWithoutCompetition(athlete))
            return Rank.MSMK;
        if (CheckMS(athlete) || CheckMSWithoutCompetition(athlete))
            return Rank.MS;
        if (CheckKMS(athlete) || CheckKMSWithoutCompetition(athlete))
            return Rank.KMS;

        return null;
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
                (athlete.Place is >= 1 and <= 3 &&
                 (athlete.Discipline.IsRelay() || athlete.Discipline.IsOpenWater())) ||
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
                athlete is
                {
                    Discipline: Discipline.OpenWater5km, TimeDifference.TotalMinutes: <= 10, Place: >= 1 and <= 8
                } ||
                athlete is
                {
                    Discipline: Discipline.OpenWater10km, TimeDifference.TotalMinutes: <= 20, Place: >= 1 and <= 8
                } ||
                athlete is
                {
                    Discipline: Discipline.OpenWater16km, TimeDifference.TotalMinutes: <= 32, Place: >= 1 and <= 8
                } ||
                athlete is
                {
                    Discipline: Discipline.OpenWater25kmAndMore, TimeDifference.TotalMinutes: <= 50,
                    Place: >= 1 and <= 8
                } ||
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
                athlete is
                {
                    Age: >= 18 and <= 19, Discipline: Discipline.OpenWater10km, Place: >= 1 and <= 6,
                    TimeDifference.TotalMinutes: <= 20
                } ||
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
        if (athlete.Competition == Competition.OtherMS)
            return false;

        return athlete.Competition switch
        {
            Competition.RussianCup =>
                athlete is
                {
                    Discipline: Discipline.OpenWater5km, TimeDifference.TotalMinutes: <= 10, Place: >= 2 and <= 3
                } ||
                athlete is
                {
                    Discipline: Discipline.OpenWater10km, TimeDifference.TotalMinutes: <= 20, Place: >= 1 and <= 6
                } ||
                athlete is
                {
                    Discipline: Discipline.OpenWater16km, TimeDifference.TotalMinutes: <= 32, Place: >= 1 and <= 6
                } ||
                athlete is
                {
                    Discipline: Discipline.OpenWater25kmAndMore, TimeDifference.TotalMinutes: <= 50,
                    Place: >= 1 and <= 6
                } ||
                // TODO: **Условие: участие не менее 10 эстафетных команд
                athlete is { Discipline: Discipline.OpenWaterRelay4x1250mMixed, Place: >= 1 and <= 6 },
            Competition.RussianFirstChampionship =>
                athlete is
                {
                    Age: >= 18 and <= 19, Discipline: Discipline.OpenWater10km, Place: >= 7 and <= 10,
                    TimeDifference.TotalMinutes: <= 20
                } ||
                athlete is
                {
                    Age: >= 16 and <= 17, Discipline: Discipline.OpenWater7_5km, Place: >= 1 and <= 8,
                    TimeDifference.TotalMinutes: <= 15
                } ||
                athlete is
                {
                    Age: >= 14 and <= 15, Discipline: Discipline.OpenWater5km, Place: >= 1 and <= 6,
                    TimeDifference.TotalMinutes: <= 10
                } ||
                // TODO: **Условие: участие не менее 10 эстафетных команд
                athlete is
                {
                    Age: >= 14 and <= 19, Discipline: Discipline.OpenWaterRelay4x1250mMixed, Place: >= 2 and <= 3
                } ||
                // TODO: **Условие: участие не менее 10 эстафетных команд
                athlete is
                {
                    Age: >= 14 and <= 16, Discipline: Discipline.OpenWaterRelay4x1250mMixed, Place: >= 1 and <= 3
                },
            Competition.OtherAllRussianCompetitionsECP =>
                athlete is
                {
                    Discipline: Discipline.OpenWater5km, Place: >= 2 and <= 3, TimeDifference.TotalMinutes: <= 10
                },
            _ => false
        };
    }

    private static bool CheckMSMKWithoutCompetition(Athlete athlete)
    {
        if (athlete.Age < 14)
            return false;

        if (athlete.Competition is Competition.OtherMS or Competition.OtherKMS1)
            return false;

        return athlete.Discipline switch
        {
            // Бассейн 25 метров
            Discipline.Pool25mFreestyle50m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 21, 9) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 23, 79),
            Discipline.Pool25mFreestyle100m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 46, 15) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 51, 85),
            Discipline.Pool25mFreestyle200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 41, 97) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 53, 34),
            Discipline.Pool25mFreestyle400m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(3, 38, 57) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(4, 0, 45),
            Discipline.Pool25mFreestyle800m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(7, 40, 18) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(8, 18, 63),
            Discipline.Pool25mFreestyle1500m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(14, 40, 13) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(16, 2, 75),
            Discipline.Pool25mBackstroke50m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 23, 1) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 26, 7),
            Discipline.Pool25mBackstroke100m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 49, 74) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 56, 16),
            Discipline.Pool25mBackstroke200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 50, 94) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 3, 13),
            Discipline.Pool25mBreaststroke50m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 26, 6) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 29, 47),
            Discipline.Pool25mBreaststroke100m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 56, 98) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 4, 22),
            Discipline.Pool25mBreaststroke200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 4, 57) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 20, 45),
            Discipline.Pool25mButterfly50m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 22, 19) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 25, 14),
            Discipline.Pool25mButterfly100m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 49, 67) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 56, 46),
            Discipline.Pool25mButterfly200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 50, 71) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 5, 41),
            Discipline.Pool25mMedley100m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 51, 87) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 58, 65),
            Discipline.Pool25mMedley200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 53, 1) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 7, 19),
            Discipline.Pool25mMedley400m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(4, 4, 33) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(4, 32, 31),
            Discipline.Pool25mRelay4x50mFreestyle =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 21, 9) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 23, 79),
            Discipline.Pool25mRelay4x100mFreestyle =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 46, 15) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 51, 85),
            Discipline.Pool25mRelay4x200mFreestyle =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 41, 97) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 53, 34),
            Discipline.Pool25mRelay4x50mMedley =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 23, 1) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 26, 7),
            Discipline.Pool25mRelay4x100mMedley =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 49, 67) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 56, 16),

            // Бассейн 50 метров
            Discipline.Pool50mFreestyle50m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 21, 72) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 24, 38),
            Discipline.Pool50mFreestyle100m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 47, 71) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 53, 22),
            Discipline.Pool50mFreestyle200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 45, 54) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 56, 17),
            Discipline.Pool50mFreestyle400m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(3, 45, 87) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(4, 3, 21),
            Discipline.Pool50mFreestyle800m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(7, 46, 65) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(8, 22, 36),
            Discipline.Pool50mFreestyle1500m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(14, 53, 59) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(16, 7, 45),
            Discipline.Pool50mBackstroke50m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 24, 51) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 27, 49),
            Discipline.Pool50mBackstroke100m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 52, 91) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 58, 77),
            Discipline.Pool50mBackstroke200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 56, 52) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 7, 89),
            Discipline.Pool50mBreaststroke50m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 26, 81) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 30, 8),
            Discipline.Pool50mBreaststroke100m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 59, 2) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 6, 7),
            Discipline.Pool50mBreaststroke200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 8, 86) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 23, 92),
            Discipline.Pool50mButterfly50m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 22, 91) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 25, 56),
            Discipline.Pool50mButterfly100m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 51, 15) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 57, 3),
            Discipline.Pool50mButterfly200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 54, 79) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 7, 62),
            Discipline.Pool50mMedley200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 57, 43) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 10, 60),
            Discipline.Pool50mMedley400m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(4, 11, 27) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(4, 37, 56),
            Discipline.Pool50mRelay4x100mFreestyle =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 47, 71) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 53, 22),
            Discipline.Pool50mRelay4x200mFreestyle =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 45, 54) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 56, 17),
            Discipline.Pool50mRelay4x100mMedley =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 52, 91) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 58, 77),
            _ => false,
        };
    }

    private static bool CheckMSWithoutCompetition(Athlete athlete)
    {
        if (athlete.Age < 12)
            return false;
        if (athlete is { Competition: Competition.OtherMS, Age: < 16 or > 18 })
            return false;
        if (athlete.Competition == Competition.OtherKMS1)
            return false;

        return athlete.Discipline switch
        {
            Discipline.Pool25mFreestyle50m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 22, 45) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 25, 75),
            Discipline.Pool25mFreestyle100m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 50, 0) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 56, 0),
            Discipline.Pool25mFreestyle200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 49, 66) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 3, 45),
            Discipline.Pool25mFreestyle400m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(3, 56, 0) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(4, 20, 0),
            Discipline.Pool25mFreestyle800m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(8, 17, 0) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(9, 0, 0),
            Discipline.Pool25mFreestyle1500m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(15, 28, 50) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(17, 12, 50),
            Discipline.Pool25mBackstroke50m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 25, 89) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 28, 65),
            Discipline.Pool25mBackstroke100m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 57, 0) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 3, 60),
            Discipline.Pool25mBackstroke200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 4, 75) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 17, 95),
            Discipline.Pool25mBreaststroke50m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 28, 25) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 32, 45),
            Discipline.Pool25mBreaststroke100m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 3, 0) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 12, 0),
            Discipline.Pool25mBreaststroke200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 18, 45) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 34, 45),
            Discipline.Pool25mButterfly50m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 23, 95) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 27, 30),
            Discipline.Pool25mButterfly100m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 54, 0) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 1, 50),
            Discipline.Pool25mButterfly200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 2, 95) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 16, 95),
            Discipline.Pool25mMedley100m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 56, 50) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 4, 50),
            Discipline.Pool25mMedley200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 5, 95) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 20, 95),
            Discipline.Pool25mMedley400m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(4, 28, 0) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(4, 58, 0),
            Discipline.Pool25mRelay4x50mFreestyle =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 22, 65) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 25, 95),
            Discipline.Pool25mRelay4x100mFreestyle =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 50, 40) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 56, 40),
            Discipline.Pool25mRelay4x200mFreestyle =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 51, 75) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 4, 25),
            Discipline.Pool25mRelay4x50mMedley =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 26, 0) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 28, 85),
            Discipline.Pool25mRelay4x100mMedley =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 57, 40) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 4, 0),
            Discipline.Pool50mFreestyle50m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 23, 20) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 26, 50),
            Discipline.Pool50mFreestyle100m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 51, 50) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 57, 50),
            Discipline.Pool50mFreestyle200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 53, 95) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 6, 45),
            Discipline.Pool50mFreestyle400m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(4, 2, 0) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(4, 26, 0),
            Discipline.Pool50mFreestyle800m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(8, 25, 0) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(9, 8, 0),
            Discipline.Pool50mFreestyle1500m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(15, 51, 0) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(17, 35, 0),
            Discipline.Pool50mBackstroke50m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 26, 65) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 29, 0),
            Discipline.Pool50mBackstroke100m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 58, 50) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 6, 0),
            Discipline.Pool50mBackstroke200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 7, 75) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 20, 95),
            Discipline.Pool50mBreaststroke50m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 29, 0) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 33, 20),
            Discipline.Pool50mBreaststroke100m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 4, 50) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 13, 50),
            Discipline.Pool50mBreaststroke200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 21, 45) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 37, 45),
            Discipline.Pool50mButterfly50m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 24, 70) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 28, 5),
            Discipline.Pool50mButterfly100m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 55, 50) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 3, 0),
            Discipline.Pool50mButterfly200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 5, 95) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 19, 95),
            Discipline.Pool50mMedley200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 9, 75) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 24, 75),
            Discipline.Pool50mMedley400m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(4, 34, 0) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(5, 3, 0),
            Discipline.Pool50mRelay4x100mFreestyle =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 51, 50) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 57, 50),
            Discipline.Pool50mRelay4x200mFreestyle =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 54, 75) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 7, 25),
            Discipline.Pool50mRelay4x100mMedley =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 58, 50) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 6, 0),
            _ => false
        };
    }

    private static bool CheckKMSWithoutCompetition(Athlete athlete)
    {
        if (athlete.Age < 14)
            return false;

        return athlete.Discipline switch
        {
            Discipline.Pool25mFreestyle50m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 23, 20) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 26, 55),
            Discipline.Pool25mFreestyle100m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 53, 30) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 0, 0),
            Discipline.Pool25mFreestyle200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 57, 45) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 11, 75),
            Discipline.Pool25mFreestyle400m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(4, 8, 50) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(4, 30, 0),
            Discipline.Pool25mFreestyle800m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(8, 50, 0) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(9, 30, 0),
            Discipline.Pool25mFreestyle1500m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(17, 6, 50) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(18, 21, 50),
            Discipline.Pool25mBackstroke50m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 27, 35) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 29, 85),
            Discipline.Pool25mBackstroke100m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 0, 40) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 8, 50),
            Discipline.Pool25mBackstroke200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 11, 45) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 25, 95),
            Discipline.Pool25mBreaststroke50m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 30, 0) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 34, 25),
            Discipline.Pool25mBreaststroke100m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 6, 90) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 16, 0),
            Discipline.Pool25mBreaststroke200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 26, 45) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 43, 45),
            Discipline.Pool25mButterfly50m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 24, 95) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 28, 45),
            Discipline.Pool25mButterfly100m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 58, 0) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 5, 0),
            Discipline.Pool25mButterfly200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 9, 95) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 24, 45),
            Discipline.Pool25mMedley100m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 1, 50) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 9, 50),
            Discipline.Pool25mMedley200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 14, 45) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 29, 45),
            Discipline.Pool25mMedley400m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(4, 43, 0) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(5, 15, 50),
            Discipline.Pool25mRelay4x50mFreestyle =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 23, 40) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 26, 75),
            Discipline.Pool25mRelay4x100mFreestyle =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 53, 70) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 0, 40),
            Discipline.Pool25mRelay4x200mFreestyle =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 58, 25) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 12, 55),
            Discipline.Pool25mRelay4x50mMedley =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 27, 55) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 30, 5),
            Discipline.Pool25mRelay4x100mMedley =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 0, 80) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 8, 90),

            // Для бассейна 50 метров
            Discipline.Pool50mFreestyle50m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 23, 95) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 27, 30),
            Discipline.Pool50mFreestyle100m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 54, 90) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 6, 1),
            Discipline.Pool50mFreestyle200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 0, 65) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 14, 76),
            Discipline.Pool50mFreestyle400m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(4, 14, 50) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(4, 41, 0),
            Discipline.Pool50mFreestyle800m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(8, 58, 0) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(9, 42, 0),
            Discipline.Pool50mFreestyle1500m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(17, 29, 0) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(18, 44, 0),
            Discipline.Pool50mBackstroke50m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 28, 15) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 30, 70),
            Discipline.Pool50mBackstroke100m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 2, 0) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 10, 0),
            Discipline.Pool50mBackstroke200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 15, 45) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 28, 95),
            Discipline.Pool50mBreaststroke50m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 30, 50) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 35, 0),
            Discipline.Pool50mBreaststroke100m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 8, 50) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 17, 50),
            Discipline.Pool50mBreaststroke200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 29, 45) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 46, 40),
            Discipline.Pool50mButterfly50m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 25, 70) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 29, 20),
            Discipline.Pool50mButterfly100m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 59, 50) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 6, 50),
            Discipline.Pool50mButterfly200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 13, 95) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 27, 45),
            Discipline.Pool50mMedley200m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 17, 25) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 33, 25),
            Discipline.Pool50mMedley400m =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(4, 48, 0) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(5, 20, 50),
            Discipline.Pool50mRelay4x100mFreestyle =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(0, 54, 90) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 6, 1),
            Discipline.Pool50mRelay4x200mFreestyle =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 1, 45) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(2, 15, 55),
            Discipline.Pool50mRelay4x100mMedley =>
                athlete is { Gender: Gender.Male } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 2, 0) ||
                athlete is { Gender: Gender.Female } && athlete.Time.TotalMilliseconds <= ToMilliseconds(1, 10, 0),
            _ => false
        };
    }

    private static double ToMilliseconds(int minutes, int seconds, int milliseconds)
    {
        return minutes * 60 + seconds * 1000 + milliseconds;
    }
}