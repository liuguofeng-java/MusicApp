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
    /// 在主窗体底部显示的控制面板 的交互逻辑
    /// </summary>
    public partial class BottomPlayerWindowControl : UserControl
    {
        private ControlBean bean = ControlBean.getInstance();

        public BottomPlayerWindowControl()
        {
            InitializeComponent();


            //点击主窗体关闭正在播放列表
            bean.mainWindow.BaseBorder.MouseDown += (s, e) =>
            {
                var contrainer = bean.songPlayListControl.SongPlayListContrainer;
                contrainer.Visibility = Visibility.Collapsed;
            };

            //点击列表按钮显示列表
            PlayListBut.Click += (s, e) =>
            {
                var contrainer = bean.songPlayListControl.SongPlayListContrainer;
                if (contrainer.Visibility == Visibility.Visible)
                {
                    contrainer.Visibility = Visibility.Collapsed;
                }
                else
                {
                    contrainer.Visibility = Visibility.Visible;
                }
            };
        }
    }
}
