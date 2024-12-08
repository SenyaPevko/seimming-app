namespace WpfApp1.Models;

using System;
using System.Globalization;
using System.Windows.Data;

public class TimeSpanConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is TimeSpan timeSpan)
        {
            return $"{(int)timeSpan.TotalHours} ч {timeSpan.Minutes} мин {timeSpan.Seconds} сек";
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
            int hours = 0, minutes = 0, seconds = 0;

            var parts = input.Split([" ", "ч", "мин", "сек"], StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < parts.Length; i++)
            {
                if (int.TryParse(parts[i], out var number))
                {
                    switch (i)
                    {
                        case 0:
                            hours = number;
                            break;
                        case 1:
                            minutes = number;
                            break;
                        case 2:
                            seconds = number;
                            break;
                    }
                }
            }

            return new TimeSpan(hours, minutes, seconds);
        }
        catch
        {
            return TimeSpan.Zero;
        }
    }
}