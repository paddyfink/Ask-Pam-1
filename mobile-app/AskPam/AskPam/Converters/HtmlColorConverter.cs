using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AskPam.Converters
{
    public class HtmlColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return value;
            Color color;
            try
            {
                color = Color.FromHex(value.ToString().Replace("#", ""));
            }
            catch (Exception ex)
            {
                color = Xamarin.Forms.Color.DarkGray;
            }
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
