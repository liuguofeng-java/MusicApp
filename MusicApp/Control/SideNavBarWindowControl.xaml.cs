using MusicApp.Common;
using MusicApp.Models;
using MusicApp.ViewModels;
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
    /// SideNavBarWindowControl.xaml 的交互逻辑
    /// </summary>
    public partial class SideNavBarWindowControl : UserControl
    {
        public static SetModel SetModel;
        public Window _parentWindow { get; set; }
        public SideNavBarWindowControl()
        {
            InitializeComponent();
            //委托调用初始化页面
            SetModel = (object page) =>
            {
                ((MainWindowModel)_parentWindow.DataContext).Page = (FrameworkElement)page;
            };

            DataContext = new SideNavBarWindowControlViewModel();

            
        }

    }
}
