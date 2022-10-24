using MusicApp.Common;
using MusicApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MusicApp.Control
{
    /// <summary>
    /// 点击'皮肤'弹出选择主题弹框,主窗体在头部 的交互逻辑
    /// </summary>
    public partial class ThemeColorControl : UserControl
    {
        public ThemeColorControl()
        {
            InitializeComponent();

            var model = new ThemeColorControlViewModel();
            DataContext = model;

            
        }

        



    }
}
