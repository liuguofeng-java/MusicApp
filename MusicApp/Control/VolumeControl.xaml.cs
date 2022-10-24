using MusicApp.Common;
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
    /// 调节程序音量按钮,点击按钮弹出调节音量进度条 的交互逻辑
    /// </summary>
    public partial class VolumeControl : UserControl
    {
        public VolumeControl()
        {
            InitializeComponent();
            var model = new VolumeViewModel();
            DataContext = model;
        }
    }
}
