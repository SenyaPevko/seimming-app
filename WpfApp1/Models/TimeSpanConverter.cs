using System.Globalization;
using System.Windows.Data;

namespace WpfApp1.Models;

public class TimeSpanConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is TimeSpan timeSpan)
        {
            return $"{timeSpan.Minutes} мин {timeSpan.Seconds} сек {timeSpan.Milliseconds} мс";
        }

        return string.Empty;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var input = value?.ToString()?.Trim();
        if (string.IsNullOrWhiteSpace(input))
            return TimeSpan.Zero;

        try
        {
            int minutes = 0, seconds = 0, milliseconds = 0;

            var parts = input.Split([" ", "мин", "сек", "мс"], StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < parts.Length; i++)
            {
                if (int.TryParse(parts[i], out var number))
                {
                    switch (i)
                    {
                        case 0:
                            minutes = number;
                            break;
                        case 1:
                            seconds = number;
                            break;
                        case 2:
                            milliseconds = number;
                            break;
                    }
                }
            }

            return new TimeSpan(0, 0 ,minutes, seconds, milliseconds);
        }
        catch
        {
            return TimeSpan.Zero;
        }
    }
}