namespace WpfApp1.Models;

public static class DisciplineChecker
{
    public static bool IsRelay(this string discipline)
    {
        return discipline.Contains("Эстафета");
    }
    
    public static bool IsOpenWater(this string discipline)
    {
        return discipline.Contains("Открытая вода");
    }
}