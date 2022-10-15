using MusicApp.Common;
using MusicApp.Models.Vo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows;

namespace MusicApp.Control
{
    /// <summary>
    /// 控制音乐如:暂停进行\下一曲上一曲\音乐进度条 ,主窗体底部中心位置 的交互逻辑
    /// </summary>
    public partial class PlayerControl : UserControl
    {

        private ControlBean bean = ControlBean.getInstance();

        private SongPlayListModel playModel;

        public List<SongDataItem> list;
        private DispatcherTimer timer;
        public PlayerControl()
        {
            ControlBean.getInstance().playerControl = this;
            InitializeComponent();

            //音乐进度条改变时
            MusicProgress.ValueChanged += (s, e) =>
            {
                PlayMedia.Position = TimeSpan.FromSeconds(MusicProgress.Value);
            };

            //默认加载播放列表第一首
            var playList = bean.jsonData.songPlayList;
            if (playList != null && playList.Count != 0)
            {
                StartPlay(playList[0]);
            }
        }


        /// <summary>
        /// 开始播放
        /// </summary>
        /// <param name="model">音乐信息</param>
        public void StartPlay(SongPlayListModel model)
        {
            //点击了相同的歌
            if (playModel != null && model.songId.Equals(playModel.songId))
            {
                PlayBut_Click(null, null);
                return;
            }
            this.playModel = model;

            //歌曲详情赋值
            bean.songDetailControl.StackPanelContrainer.Visibility = Visibility.Visible;
            bean.songDetailControl.DataContext = model;

            //开始播放歌曲
            PlayMedia.Source = new Uri(model.songUrl);
            PlayMedia.Play();

            //PlayBut.Content = "\xe87c";
            PlayBut.Content = "\xea81";//更新按钮图标


            //计算歌曲时间
            int second = model.songTime / 1000;//总秒数
            int minute = second / 60;//分钟数
            int remSecond = second - (minute * 60);//剩余秒数

            MusicProgress.Maximum = second;//进度条数
            EndProgress.Text = (minute.ToString().Length == 1 ? "0" + minute.ToString() : minute.ToString())
                                      + ":" +
                                     (remSecond.ToString().Length == 1 ? "0" + remSecond.ToString() : remSecond.ToString());//总时长

            //开始计算
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler((s, e) =>
            {
                MusicProgress.Value = PlayMedia.Position.TotalSeconds;
                int second = (int)PlayMedia.Position.TotalSeconds;//总秒数
                int minute = second / 60;//分钟数
                int remSecond = second - (minute * 60);//剩余秒数
                StartProgress.Text = (minute.ToString().Length == 1 ? "0" + minute.ToString() : minute.ToString())
                                      + ":" + 
                                     (remSecond.ToString().Length == 1 ? "0" + remSecond.ToString(): remSecond.ToString());
            });
            timer.Start();
        }

        /// <summary>
        /// 进行或者暂停按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlayBut_Click(object sender, RoutedEventArgs e)
        {
            if (PlayMedia.Position.TotalSeconds == 0) return;
            if (PlayBut.Content.Equals("\xe87c"))
            {
                PlayBut.Content = "\xea81";
                PlayMedia.Play();
            }
            else
            {
                PlayBut.Content = "\xe87c";
                PlayMedia.Pause();
            }
        }
    }
}
