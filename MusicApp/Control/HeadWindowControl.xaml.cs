using MusicApp.ViewModels;
using MusicApp.Views;
using System.Windows;
using System.Windows.Controls;

namespace MusicApp.Control
{
    /// <summary>
    /// HeadWindowControl.xaml 的交互逻辑
    /// </summary>
    public partial class HeadWindowControl : UserControl
    {

        private MainWindow mainWindow;
        public HeadWindowControl()
        {
            InitializeComponent();
            mainWindow = ControlBean.getInstance().mainWindow;

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
                mainWindow.themeColor.PopupContrainer.IsOpen = true;
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
    }
}
