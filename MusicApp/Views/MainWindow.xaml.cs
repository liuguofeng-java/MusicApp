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

    }
}
