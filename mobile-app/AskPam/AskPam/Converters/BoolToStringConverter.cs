using AskPam.Resources;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace AskPam.Converters
{
    public class BoolToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return AppResources.BotOff;

            return (bool)value ? AppResources.BotOn : AppResources.BotOff;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
