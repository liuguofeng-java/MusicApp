using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Data;
using System.Xml.Serialization;

namespace MusicApp.Common
{
    /// <summary>
    /// CommandParameter 多参数传递
    /// </summary>
    public class ObjectConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return values.Clone();
        }

        public object[] ConvertBack(object value, Type[] targetTypes,
          object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }


    }

}
