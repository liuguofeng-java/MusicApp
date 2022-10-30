using MusicApp.Common;
using MusicApp.Models.Vo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows;
using System.Threading;
using System.IO;
using MusicApp.Models;
using MusicApp.ViewModels;

namespace MusicApp.Control
{
    /// <summary>
    /// 控制音乐如:暂停进行\下一曲上一曲\音乐进度条 ,主窗体底部中心位置 的交互逻辑
    /// </summary>
    public partial class PlayerControl : UserControl
    {
        public PlayerControl()
        {
            InitializeComponent();

            var model = new PlayerViewModel();
            DataContext = model;
        }

    }
}
