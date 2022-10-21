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

namespace MusicApp.Control
{
    /// <summary>
    /// 控制音乐如:暂停进行\下一曲上一曲\音乐进度条 ,主窗体底部中心位置 的交互逻辑
    /// </summary>
    public partial class PlayerControl : UserControl
    {

        private ControlBean bean = ControlBean.getInstance();

        private SongPlayListModel playModel;

        private DispatcherTimer timer;
        public PlayerControl()
        {
            ControlBean.getInstance().playerControl = this;
            InitializeComponent();

            DataContext  = new PlayerModel();


            //音乐进度条改变时
            MusicProgress.ValueChanged += (s, e) =>
            {
                PlayMedia.Position = TimeSpan.FromSeconds(MusicProgress.Value);
            };


            //默认加载播放列表第一首
            var playList = bean.jsonData.songPlayList;
            if (playList != null && playList.Count != 0)
            {
                InitPlay(playList[0], false);
            }

            //播放结束事件
            PlayMedia.MediaEnded += (s, e) =>
            {
                StopPlay();
            };

            //上一首
            LastSong.Click += (s, e) =>
            {
                if (playModel == null) return;
                bean.songPlayListControl.NextSongPlay(playModel.songId, true, 2);
            };

            //下一首
            NextSong.Click += (s, e) =>
            {
                if (playModel == null) return;
                bean.songPlayListControl.NextSongPlay(playModel.songId, false, 2);
            };

        }

        /// <summary>
        /// 准备播放
        /// </summary>
        /// <param name="model">音乐信息</param>
        /// <param name="isStartPlay">是否播放</param>
        public void InitPlay(SongPlayListModel model, bool isStartPlay = true)
        {
            PlayerModel dataContext = null;
            new Thread(() =>
            {
                //点击了相同的歌
                if (playModel != null && model.songId.Equals(playModel.songId))
                {
                    PlayBut_Click(null, null);
                    return;
                }

                //先要停止歌曲
                this.Dispatcher.Invoke(new Action(delegate
                {
                    dataContext = (PlayerModel)DataContext;
                    if (timer != null)
                    {
                        timer.Stop();
                    }
                    //初始化控件数据
                    dataContext.playProgress = 0;
                    dataContext.startProgressTiem = "00:00";
                    dataContext.endProgressTiem = "00:00";
                    dataContext.playProgressLength = 1;
                    dataContext.disabledPlayProgress = false;
                    dataContext.playButContent = "\xe87c";
                    bean.songInfoControl.StackPanelContrainer.Visibility = Visibility.Collapsed;
                    PlayMedia.Stop();
                }));
                this.playModel = model;

                //初始化列表颜色
                bean.songPlayListControl.SetLisBoxColor(model);
                //歌曲详情赋值
                bean.songInfoControl.SetSongInfo(model);

                //开始播放歌曲
                GetSongUrl(model);
                if (model.localSongUrl == null)
                    return;


                this.Dispatcher.Invoke(new Action(delegate
                {
                    //更新歌曲url
                    PlayMedia.Source = new Uri(model.localSongUrl);
                    if (isStartPlay)
                    {
                        dataContext.playButContent = "\xea81";//更新按钮图标
                        PlayMedia.Play();
                    }
                    dataContext.disabledPlayProgress = true;//进度条放开
                    dataContext.playProgress = 0;//初始化进度
                    dataContext.playProgressLength = model.songTime / 1000; ;//进度条数
                    dataContext.endProgressTiem = model.formatSongTime;//总时长


                    //计时器更新进度条
                    timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromMilliseconds(1000);
                    timer.Tick += new EventHandler((s, e) =>
                    {
                        var totalSeconds = PlayMedia.Position.TotalSeconds;
                        int second = (int)totalSeconds;//总秒数
                        int minute = second / 60;//分钟数
                        int remSecond = second - (minute * 60);//剩余秒数

                        dataContext.playProgress = totalSeconds;
                        dataContext.startProgressTiem = (minute.ToString().Length == 1 ? "0" + minute.ToString() : minute.ToString())
                                                + ":" +
                                                (remSecond.ToString().Length == 1 ? "0" + remSecond.ToString() : remSecond.ToString());
                    });
                    timer.Start();
                }));

            }).Start();
        }

        /// <summary>
        /// 进行或者暂停按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayBut_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(delegate
            {
                PlayerModel dataContext = (PlayerModel)DataContext;
                if (PlayMedia.Position.TotalSeconds == 0)
                {
                    if (playModel != null)
                    {
                        PlayMedia.Play();
                        dataContext.playButContent = "\xea81";//更新按钮图标
                    }
                    return;
                };
                if (dataContext.playButContent.Equals("\xe87c"))
                {
                    dataContext.playButContent = "\xea81";
                    PlayMedia.Play();
                }
                else
                {
                    dataContext.playButContent = "\xe87c";
                    PlayMedia.Pause();
                }
            }));

        }

        /// <summary>
        /// 停止播放
        /// </summary>
        public void StopPlay()
        {
            PlayMedia.Stop();
            ((PlayerModel)DataContext).playButContent = "\xe87c";
            bean.songInfoControl.StackPanelContrainer.Visibility = Visibility.Collapsed;
            bean.songPlayListControl.NextSongPlay(playModel.songId, false);
            playModel = null;
        }


        /// <summary>
        /// 获取歌曲url
        /// </summary>
        /// <param name="model">播放歌曲信息</param>
        /// <returns></returns>
        private void GetSongUrl(SongPlayListModel model)
        {

            //保存本地文件的名
            string fileName = model.songId + ".mp3";
            string path = Directory.GetCurrentDirectory() + @"\cache\songs";

            //存储图片
            if (model.localSongUrl == null || StringUtil.UrlDiscern(model.localSongUrl) || !File.Exists(model.localSongUrl))
            {
                //如果之前存在过,否则就下载
                if (File.Exists(path + @"\"+  fileName))
                {
                    model.localSongUrl = path + @"\" + fileName;
                }
                else
                {
                    Directory.CreateDirectory(path);//文件夹没有就创建
                    string res = HttpUtil.HttpDownload(model.songUrl, path, fileName);
                    if (res == null) return;
                    model.localSongUrl = res;
                }
            }
            InitJsonData.WriteJsonFile();//手动更新缓存

            //如果保存失败
            if (model.localSongUrl == null || !File.Exists(model.localSongUrl))
            {
                model.localSongUrl = null;
            }
        }
    }
}
