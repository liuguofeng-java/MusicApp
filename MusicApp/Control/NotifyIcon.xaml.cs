using MusicApp.ViewModels;
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
    /// 程序托盘菜单 的交互逻辑
    /// </summary>
    public partial class NotifyIcon : UserControl
    {
        public NotifyIcon()
        {

            InitializeComponent();
            var model = new NotifyIconModelView();

            DataContext = model;

        }
    }
}
