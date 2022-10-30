using MusicApp.Common;
using MusicApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MusicApp.ViewModels
{
    public class SongInfoViewModel
    {
        public static SongInfoViewModel This { get; set; }
        public SongInfoModel Model { get; set; }

        public SongInfoViewModel()
        {
            This = this;
            Model = new SongInfoModel();

            //播放器事件
            PlayerViewModel.This.PlayDelegate += new Action<SongModel>((o) =>
            {
                //开始和暂停更新
                if (!o.Status.Equals(SongModel.PlayStatus.ClosePlay))
                {
                    //如果已经加载控件
                    if (Model.SongInfoVisibility == Visibility.Visible && o.SongId.Equals(Model.SongModel.SongId)) return;
                    SetSongInfo(o);
                }
                //播放和关闭要隐藏当前控件
                if (o.Status.Equals(SongModel.PlayStatus.StartPlay) || o.Status.Equals(SongModel.PlayStatus.ClosePlay))
                    Model.SongInfoVisibility = Visibility.Hidden;
            });
        }


        /// <summary>
        /// 歌曲 如头像、歌曲名称、作者、赋值
        /// </summary>
        /// <param name="model"></param>
        public void SetSongInfo(SongModel model)
        {
            new Thread(() =>
            {
                Model.SongModel = model;
                Model.SongInfoVisibility = Visibility.Visible;
                Model.SongPicVisibility = Visibility.Visible;
                //保存本地文件的名
                string fileName = model.SongId + ".png";
                string path = Directory.GetCurrentDirectory() + @"\cache\images";

                //存储图片
                if (model.LocalPicUrl == null || StringUtil.UrlDiscern(model.LocalPicUrl) || !File.Exists(model.LocalPicUrl))
                {
                    //如果之前存在过,否则就下载
                    if (File.Exists(path + @"\" + fileName))
                    {
                        model.LocalPicUrl = path + @"\" + fileName;
                    }
                    else
                    {
                        Directory.CreateDirectory(path);//文件夹没有就创建
                        string res = HttpUtil.HttpDownload(model.PicUrl, path, fileName);
                        if (res == null) return;
                        model.LocalPicUrl = res;
                    }
                }
                InitJsonData.WriteJsonFile();//手动更新缓存

                //赋值
                Model.SongPicVisibility = Visibility.Visible;
                SongDetailViewModel.This.InitLyrics(model);
            }).Start();
        }
    }
}
