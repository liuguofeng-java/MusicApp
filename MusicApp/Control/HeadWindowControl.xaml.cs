using MusicApp.ViewModels;
using MusicApp.Views;
using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MusicApp.Control
{
    /// <summary>
    /// 主窗体头部,控制程序关闭\最小化\最大化等按钮 的交互逻辑
    /// </summary>
    public partial class HeadWindowControl : UserControl
    {

        private MainWindow mainWindow = ControlBean.getInstance().mainWindow;
        public HeadWindowControl()
        {
            InitializeComponent();

            //缩小
            ZoomOutWindowBut.Click += (s, e) =>
            {
                mainWindow.WindowState = WindowState.Minimized;
            };
            //最大化
            ZoomWindowBut.Click += (s, e) => ZoomWindow(s, e);
            //关闭应用
            CloseWindowBut.Click += (s, e) =>
            {
                mainWindow.Visibility = Visibility.Collapsed;
            };

            //点击图标时
            Logo.Click += (s, e) =>
            {
                //单选框变为选中状态
                UIElementCollection element = mainWindow.sideNavBarWindow.radioButs.Children;
                ((RadioButton)element[0]).IsChecked = true;
                SideNavBarWindowControlViewModel.setModle();
            };

            //点击皮肤显示
            Skin.Click += (s, e) =>
            {
                themeColor.PopupContrainer.IsOpen = true;
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
            if (mainWindow.WindowState == WindowState.Maximized)
            {
                mainWindow.WindowState = WindowState.Normal;
                ZoomWindowBut.Content = "\xe65d";
            }
            else
            {
                mainWindow.WindowState = WindowState.Maximized;
                ZoomWindowBut.Content = "\xe653";
            }
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
                mainWindow.DragMove();
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
