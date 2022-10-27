using MusicApp.Common;
using MusicApp.ViewModels;
using MusicApp.ViewModels.Widget;
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

namespace MusicApp.Control.Widget
{
    /// <summary>
    /// SongsControl.xaml 的交互逻辑
    /// </summary>
    public partial class SongsControl : UserControl
    {
        public SongsControl()
        {
            InitializeComponent();
            var model = new SongsViewModel();
            DataContext = model;
        }
    }
}
