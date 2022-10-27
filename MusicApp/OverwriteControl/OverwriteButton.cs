using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MusicApp.OverwriteControl
{
    public class OverwriteButton : Button
    {
        public static object GetImageSource(DependencyObject obj)
        {
            return (int)obj.GetValue(ImageSourceProperty);
        }

        public static void SetImageSource(DependencyObject obj, object value)
        {
            obj.SetValue(ImageSourceProperty, value);
        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.RegisterAttached("ImageSource", typeof(string), typeof(OverwriteButton), new PropertyMetadata(null));

    }
}
