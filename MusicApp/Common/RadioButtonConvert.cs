using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;

namespace MusicApp.Common
{
    public class RadioButtonConvert : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !value.ToString().Equals(parameter.ToString()))
            {
                return false;
            }
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value != null && value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
}