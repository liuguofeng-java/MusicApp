using MusicApp.Common;
using MusicApp.Models;
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
    public partial class SongInfoControl : UserControl
    {
        public SongInfoControl()
        {
            ControlBean.getInstance().songInfoControl = this;
            InitializeComponent();
        }

        /// <summary>
        /// 歌曲 如头像、歌曲名称、作者、赋值
        /// </summary>
        /// <param name="model"></param>
        public void SetSongInfo(SongPlayListModel model)
        {
            new Thread(() =>
            {
                this.Dispatcher.Invoke(new Action(delegate
                {
                    DataContext = model;
                    StackPanelContrainer.Visibility = Visibility.Visible;
                    SongPic.Visibility = Visibility.Collapsed;
                }));
                //保存本地文件的名
                string fileName = "localPicUrl" + model.songId + ".png";
                string path = Directory.GetCurrentDirectory() + @"\cache\images";

                //存储图片
                if (model.localPicUrl == null || StringUtil.UrlDiscern(model.localPicUrl) || !File.Exists(model.localPicUrl))
                {
                    //如果之前存在过,否则就下载
                    if (File.Exists(path + @"\" + fileName))
                    {
                        model.localPicUrl = path + @"\" + fileName;
                    }
                    else
                    {
                        Directory.CreateDirectory(path);//文件夹没有就创建
                        string res = HttpUtil.HttpDownload(model.picUrl, path, fileName);
                        if (res == null) return;
                        model.localPicUrl = res;
                    }
                }
                InitJsonData.WriteJsonFile();//手动更新缓存

                //赋值
                this.Dispatcher.Invoke(new Action(delegate
                {
                    SongPic.Visibility = Visibility.Visible;
                }));
                SongDetail.InitLyrics(model);

            }).Start();
        }

    }
}
