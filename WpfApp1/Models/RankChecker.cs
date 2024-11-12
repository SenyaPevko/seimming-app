namespace WpfApp1.Models;

public static class RankChecker
{
    public static string CheckRank(Athlete athlete)
    {
        if (athlete.Competition == "Чемпионат страны" && athlete.Place <= 3 && athlete.TimeDifference <= 2.0)
        {
            return "Поздравляем! Данный спортсмен может претендовать на звание МС.";
        }
        else if (athlete.Competition == "Чемпионат страны" && athlete.Place <= 6 && athlete.TimeDifference <= 4.0)
        {
            return "Поздравляем! Данный спортсмен может претендовать на звание КМС.";
        }
        else if (athlete.Competition == "Международные соревнования" && athlete.Place <= 5 && athlete.TimeDifference <= 3.0)
        {
            return "Поздравляем! Данный спортсмен может претендовать на звание КМС.";
        }

        return "К сожалению, данный спортсмен не соответствует требованиям для получения звания КМС или МС.";
    }
}