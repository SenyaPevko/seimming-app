using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfApp1.Models
{
    public class OpenWaterChecker : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string discipline)
            {
                // Используем существующую логику для проверки на "Открытую воду"
                return discipline.IsOpenWater();
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}