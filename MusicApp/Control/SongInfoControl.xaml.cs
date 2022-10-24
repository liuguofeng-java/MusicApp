using MusicApp.Common;
using MusicApp.Models;
using MusicApp.Models.Vo;
using MusicApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// 歌曲详情 如头像、歌曲名称、作者、等信息,在主窗体底部左侧 的交互逻辑
    /// </summary>
    public partial class SongInfoControl : UserControl
    {
        public SongInfoControl()
        {
            InitializeComponent();

            var model = new SongInfoViewModel();
            DataContext = model;
        }

        

    }
}
