using MusicApp.Models.PageView.ChildPage;
using MusicApp.ViewModels.PageView.ChildPage;
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

namespace MusicApp.PageView.ChildPage
{
    /// <summary>
    /// SongOfDay.xaml 的交互逻辑
    /// </summary>
    public partial class SongListOfDayPage : UserControl
    {
        public SongListOfDayPage()
        {
            InitializeComponent();
            var model = new SongListOfDayViewPageModel();
            DataContext = model;
        }
    }
}
