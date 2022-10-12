using MusicApp.Common;
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
    /// VolumeControl.xaml 的交互逻辑
    /// </summary>
    public partial class VolumeControl : UserControl
    {
        private PlayerControl playerControl = ControlBean.getInstance().playerControl;
        public VolumeControl()
        {
            InitializeComponent();

            //调节声音时触发
            VolumeProgress.ValueChanged += (s, e) =>
            {
                if (VolumeProgress.Value == 0)
                {
                    VolumeBut.Content = "\xe610";
                }
                else
                {
                    VolumeBut.Content = "\xe63c";
                }
                playerControl.PlayMedia.Volume = VolumeProgress.Value;
                INIFileUtil.writeValue("sys", "volume", VolumeProgress.Value.ToString());
            };
            //移入音量按钮,打开音量调节
            VolumeBut.MouseEnter += (s, e) =>
            {
                VolumePopupContrainer.IsOpen = true;
            };
            //离开border时关闭音量调节
            VolumeStackPanelContrainer.MouseLeave += (s, e) =>
            {
                VolumePopupContrainer.IsOpen = false;
            };
            //点击静音或者还原
            VolumeBorder.MouseDown += (s, e) =>
            {
                if (VolumeProgress.Value == 0)
                {
                    VolumeProgress.Value = 0.5;
                    VolumeBut.Content = "\xe63c";
                }
                else
                {
                    VolumeProgress.Value = 0;
                    VolumeBut.Content = "\xe610";
                }
                playerControl.PlayMedia.Volume = VolumeProgress.Value;
                INIFileUtil.writeValue("sys", "volume", VolumeProgress.Value.ToString());
            };

            //初始化音量
            string volume = INIFileUtil.readValue("sys", "volume");
            double val = string.IsNullOrEmpty(volume) ? 0.5 : Convert.ToDouble(volume);
            if (string.IsNullOrEmpty(volume))
            {
                INIFileUtil.writeValue("sys", "volume", "0.5");
            }
            if (VolumeProgress.Value == 0)
            {
                VolumeBut.Content = "\xe610";
            }
            VolumeProgress.Value = val;
            playerControl.PlayMedia.Volume = val;
        }
    }
}
