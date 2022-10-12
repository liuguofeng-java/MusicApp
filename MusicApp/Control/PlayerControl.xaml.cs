using MusicApp.Common;
using MusicApp.Models.Vo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MusicApp.Control
{
    /// <summary>
    /// 控制音乐如:暂停进行\下一曲上一曲\音乐进度条 ,主窗体底部中心位置 的交互逻辑
    /// </summary>
    public partial class PlayerControl : UserControl
    {

        private SongDetailControl songDetailControl = ControlBean.getInstance().songDetailControl;

        public List<SongDataItem> list;
        private DispatcherTimer timer;
        public PlayerControl()
        {
            ControlBean.getInstance().playerControl = this;
            InitializeComponent();

            //进行或者暂停按钮按下
            PlayBut.Click += (s, e) =>
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
            };

            //音乐进度条改变时
            MusicProgress.ValueChanged += (s, e) =>
            {
                PlayMedia.Position = TimeSpan.FromSeconds(MusicProgress.Value);
            };
        }

        /// <summary>
        /// 获取歌曲播放路径并放入播放列表
        /// </summary>
        /// <param name="idList">歌曲id</param>
        public void GetSongUrl(List<string> idList)
        {
            //获取歌曲详情\头像\名称\作者
            songDetailControl.GetSongDetail(idList[0]);

            //获取歌曲url
            //格式化id
            StringBuilder builder = new StringBuilder();
            idList.ForEach(item =>
            {
                builder.Append(item + ",");
            });
            string str = builder.ToString();
            string ids = str.Substring(0, str.Length - 1);

            //接收数据
            string result = HttpUtil.HttpRequset(HttpUtil.serveUrl + "/song/url?id=" + ids);
            PlayerModel data = JsonConvert.DeserializeObject<PlayerModel>(result);
            list = data.data;
        }

        /// <summary>
        /// 开始播放
        /// </summary>
        public void StartPlay(SongDataItem songData = null)
        {
            //开始播放歌曲
            if (songData == null)
                songData = list[0];
            PlayMedia.Source = new Uri(songData.url);
            PlayMedia.Play();

            //PlayBut.Content = "\xe87c";
            PlayBut.Content = "\xea81";//更新按钮图标


            //计算歌曲时间
            int second = songData.time / 1000;//总秒数
            int minute = second / 60;//分钟数
            int remSecond = second - (minute * 60);//剩余秒数

            MusicProgress.Maximum = second;//进度条数
            EndProgress.Text = minute + ":" + remSecond;//总时长

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

    }
}
