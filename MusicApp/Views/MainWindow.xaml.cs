using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace MusicApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            //缩小
            ZoomOutWindowBut.Click += (s, e) =>
            {
                WindowState = WindowState.Minimized;
            };
            //最大化
            ZoomWindowBut.Click += (s, e) => ZoomWindow(s, e);
            //关闭应用
            CloseWindowBut.Click += (s, e) =>
            {
                Application.Current.Shutdown();
            };
        }

        /// <summary>
        /// 移动\最大化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WindowMove_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //移动窗体事件
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                DragMove();
            }
            //双击事件放大缩小
            var element = (FrameworkElement)sender;
            if (e.ClickCount == 1)
            {
                var timer = new Timer(500);
                timer.AutoReset = false;
                timer.Elapsed += new ElapsedEventHandler((o, ex) => element.Dispatcher.Invoke(new Action(() =>
                {
                    var timer2 = (Timer)element.Tag;
                    timer2.Stop();
                    timer2.Dispose();
                })));
                timer.Start();
                element.Tag = timer;
            }
            if (e.ClickCount > 1)
            {
                var timer = element.Tag as Timer;
                if (timer != null)
                {
                    timer.Stop();
                    timer.Dispose();
                    ZoomWindow(sender,e);
                }
            }
        }

        /// <summary>
        /// 最大化\还原
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZoomWindow(object sender, RoutedEventArgs e)
        {
            //判断是否以及最大化，最大化就还原窗口，否则最大化
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                ZoomWindowBut.Content = "\xe65d";
            }
            else
            {
                WindowState = WindowState.Maximized;
                ZoomWindowBut.Content = "\xe653";
            }
        }


    }
}
