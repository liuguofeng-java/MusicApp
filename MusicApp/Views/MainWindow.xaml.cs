using MusicApp.Models;
using MusicApp.ViewModels;
using System;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Shell;

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
            var model = MainWindowViewModel.GetInstance();
            DataContext = model;

            //播放器事件,更新任务栏
            PlayerViewModel.This.PlayDelegate += new Action<SongModel>((o) =>
            {
                switch (o.Status)
                {
                    case SongModel.PlayStatus.StartPlay:
                        model.SetTaskbarStat(o.SongName, false, null);
                        break;
                    case SongModel.PlayStatus.StopPlay:
                        model.SetTaskbarStat(o.SongName, true, null);
                        break;
                    case SongModel.PlayStatus.ClosePlay:
                        model.SetTaskbarStat("网易云音乐", true, null);
                        break;
                }
            });


            //最大化宽度
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;

            //设置窗体直角
            IntPtr hWnd = new WindowInteropHelper(GetWindow(this)).EnsureHandle();
            var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
            var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_DONOTROUND;
            DwmSetWindowAttribute(hWnd, attribute, ref preference, sizeof(uint));

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
                Visibility = Visibility.Collapsed;
            };

            //点击图标时
            Logo.Click += (s, e) =>
            {
                model.Model.MenusChecked = Models.MenusChecked.FoundMusicPage;
            };

        }

        // The enum flag for DwmSetWindowAttribute's second parameter, which tells the function what attribute to set.
        public enum DWMWINDOWATTRIBUTE
        {
            DWMWA_WINDOW_CORNER_PREFERENCE = 33
        }

        // The DWM_WINDOW_CORNER_PREFERENCE enum for DwmSetWindowAttribute's third parameter, which tells the function
        // what value of the enum to set.
        public enum DWM_WINDOW_CORNER_PREFERENCE
        {
            DWMWCP_DEFAULT = 0,
            DWMWCP_DONOTROUND = 1,
            DWMWCP_ROUND = 2,
            DWMWCP_ROUNDSMALL = 3
        }

        // Import dwmapi.dll and define DwmSetWindowAttribute in C# corresponding to the native function.
        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern long DwmSetWindowAttribute(IntPtr hwnd,
                                                         DWMWINDOWATTRIBUTE attribute,
                                                         ref DWM_WINDOW_CORNER_PREFERENCE pvAttribute,
                                                         uint cbAttribute);



        /// <summary>
        /// 最大化\还原
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ZoomWindow(object sender, RoutedEventArgs e)
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

        /// <summary>
        /// 移动\双击最大化
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
                    ZoomWindow(sender, e);
                }
            }
        }


    }
}
