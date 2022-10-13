using MusicApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
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
    /// SearchTextBoxControl.xaml 的交互逻辑
    /// </summary>
    public partial class SearchTextBoxControl : UserControl
    {
        private MainWindow mainWindow = ControlBean.getInstance().mainWindow;

        public SearchTextBoxControl()
        {
            InitializeComponent();

            //点击主窗体border,隐藏搜索列表控件
            mainWindow.BaseBorder.MouseDown += (s, e) =>
            {
                ControlBean.getInstance().searchListControl.StackPanelContrainer.Visibility = Visibility.Collapsed;
            };
        }

        /// <summary>
        /// 点击搜索按钮时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        /// <summary>
        /// 点击输入框触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputScrollViewer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //点击输入框,显示搜索列表控件
            ControlBean.getInstance().searchListControl.StackPanelContrainer.Visibility = Visibility.Visible;
        }
    }
}
