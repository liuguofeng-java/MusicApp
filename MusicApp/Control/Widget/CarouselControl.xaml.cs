using MusicApp.Common;
using MusicApp.Models.Vo;
using MusicApp.ViewModels;
using MusicApp.ViewModels.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// 发现音乐->个性推荐 的轮播图 的交互逻辑
    /// </summary>
    public partial class CarouselControl : UserControl
    {
        public CarouselControl()
        {
            InitializeComponent();

            var model = new CarouselViewModel(this);
            DataContext = model;

            
        }

        

    }
}
