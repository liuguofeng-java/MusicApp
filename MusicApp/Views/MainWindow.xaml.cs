using MusicApp.Models;
using MusicApp.ViewModels;
using System;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace MusicApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowModel model { get; set; }
        public MainWindow()
        {
            ControlBean.getInstance().mainWindow = this;
            InitializeComponent();

            //最大化宽度
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            

            //绑定数据
            model = new MainWindowModel();
            DataContext = model;

            //初始化页面
            SideNavBarWindowControlViewModel.setModle();


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
                    headWindow.ZoomWindow(sender, e);
                }
            }
        }



        
    }
}
