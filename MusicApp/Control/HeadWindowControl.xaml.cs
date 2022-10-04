using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicApp.Control
{
    /// <summary>
    /// HeadWindowControl.xaml 的交互逻辑
    /// </summary>
    public partial class HeadWindowControl : UserControl
    {

        public Window _parentWindow;
        public HeadWindowControl()
        {
            InitializeComponent();
            //缩小
            ZoomOutWindowBut.Click += (s, e) =>
            {
                _parentWindow.WindowState = WindowState.Minimized;
            };
            //最大化
            ZoomWindowBut.Click += (s, e) => ZoomWindow(s, e);
            //关闭应用
            CloseWindowBut.Click += (s, e) =>
            {
                _parentWindow.Visibility = Visibility.Collapsed;
            };
        }

        

        /// <summary>
        /// 最大化\还原
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ZoomWindow(object sender, RoutedEventArgs e)
        {
            //判断是否以及最大化，最大化就还原窗口，否则最大化
            if (_parentWindow.WindowState == WindowState.Maximized)
            {
                _parentWindow.WindowState = WindowState.Normal;
                ZoomWindowBut.Content = "\xe65d";
            }
            else
            {
                _parentWindow.WindowState = WindowState.Maximized;
                ZoomWindowBut.Content = "\xe653";
            }
        }
    }
}
