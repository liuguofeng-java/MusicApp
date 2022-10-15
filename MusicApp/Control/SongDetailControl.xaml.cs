using MusicApp.Common;
using MusicApp.Models.Vo;
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
    public partial class SongDetailControl : UserControl
    {
        public SongDetailControl()
        {
            ControlBean.getInstance().songDetailControl = this;
            InitializeComponent();
        }

        public void SetSongDetail(SongPlayListModel model)
        {
            StackPanelContrainer.Visibility = Visibility.Visible;
            SongPic.Visibility = Visibility.Collapsed;
            new Thread(async () =>
            {
                //保存本地文件的名
                string fileName = "localPicUrl" + model.songId + ".png";
                string path = Directory.GetCurrentDirectory() + @"\cache\images";

                //存储图片
                if (model.localPicUrl == null || StringUtil.UrlDiscern(model.localPicUrl) || !File.Exists(model.localPicUrl))
                {
                    Directory.CreateDirectory(path);//文件夹没有就创建
                    string res = await HttpUtil.HttpDownload(model.picUrl, path, fileName);
                    model.localPicUrl = res;
                }

                //如果保存失败
                if (model.localPicUrl == null)
                {
                    model.localPicUrl = model.picUrl;
                }
                else//保存成功
                {
                    //是否存在,有可能被清理
                    if (!File.Exists(model.localPicUrl))
                    {
                        model.localPicUrl = model.picUrl;
                    }
                }
                //赋值
                this.Dispatcher.Invoke(new Action(delegate
                {
                    DataContext = model;
                    SongPic.Visibility = Visibility.Visible;
                }));

            }).Start();
        }
    }
}
