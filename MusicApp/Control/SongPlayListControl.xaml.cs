using MusicApp.Common;
using MusicApp.Models.Vo;
using MusicApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
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
    /// 歌曲播放列表,主窗体右下方 的交互逻辑
    /// </summary>
    public partial class SongPlayListControl : UserControl
    {
        public SongPlayListControl()
        {
            InitializeComponent();
            var model = new SongPlayListViewModel();
            DataContext = model;
        }


        
    }
}
