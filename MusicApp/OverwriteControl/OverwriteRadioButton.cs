using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MusicApp.OverwriteControl
{
    public class OverwriteRadioButton : RadioButton
    {
        public static object GetImageSource(DependencyObject obj)
        {
            return (int)obj.GetValue(MyPropertyProperty);
        }

        public static void SetImageSource(DependencyObject obj, object value)
        {
            obj.SetValue(MyPropertyProperty, value);
        }

        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.RegisterAttached("ImageSource", typeof(string), typeof(OverwriteRadioButton), new PropertyMetadata(null));


    }
}
