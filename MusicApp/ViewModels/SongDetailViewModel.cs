using MusicApp.Common;
using MusicApp.Component.AppCsStyle;
using MusicApp.Control;
using MusicApp.Models;
using MusicApp.Models.Vo;
using MusicApp.Models.Widget;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;


namespace MusicApp.ViewModels
{
    public class SongDetailViewModel
    {
        public static SongDetailViewModel This { get; set; }
        public SongDetailModel Model { get; set; }
        public SongDetailControl ThisWindow { get; set; }

        //歌词循环
        private DispatcherTimer timer;
        
        public SongDetailViewModel(SongDetailControl ThisWindow)
        {
            This = this;
            this.ThisWindow = ThisWindow;
            Model = new SongDetailModel();
        }

        /// <summary>
        /// 获取歌词
        /// </summary>
        /// <param name="songModel"></param>
        public void GetLyrics(SongModel songModel)
        {
            new Thread(() =>
            {
                //保存歌词
                if (songModel.Lyric == null)
                {
                    string lyricsRes = HttpUtil.HttpRequset(HttpUtil.serveUrl + "/lyric?id=" + songModel.SongId);
                    if (lyricsRes == null)
                    {
                        return;
                    }
                    LyricsResultModel detailModel = JsonConvert.DeserializeObject<LyricsResultModel>(lyricsRes);
                    string lyrics = detailModel.lrc.lyric;//歌词数据
                    songModel.Lyric = lyrics;
                    InitJsonData.WriteJsonFile();//手动更新缓存
                }
                //初始化歌词
                Application.Current.Dispatcher.InvokeAsync(new Action(delegate
                {
                    //更新数据
                    Model.Author = songModel.Author;
                    Model.SongName = songModel.SongName;
                    Model.SongId = songModel.SongId;
                    InitLyrics(songModel.Lyric);
                }));
            }).Start();
        }

        /// <summary>
        /// 初始化歌词
        /// </summary>
        /// <param name="lyricStr"></param>
        public void InitLyrics(string lyricStr)
        {
            List<LyricItme> lyricItmes = new List<LyricItme>();//歌词集合
            Regex rx = new Regex(@"(?<=^\[)(\d+:\d+\.\d+).(.+)(?=$)", RegexOptions.Multiline);//正则
            var list = rx.Matches(lyricStr);
            for (int i = 0; i < list .Count; i++)
            {
                LyricItme l = new LyricItme();
                var value = list[i].Value;
                var split = value.IndexOf(":");
                //秒
                var second = int.Parse(value.Substring(0, split));
                //毫秒
                var millisecondStr = value.Substring(split + 1, value.IndexOf("]") - 1 - split);
                var millisecond = double.Parse(millisecondStr);
                //赋值
                l.Time = (second * 60) + millisecond;
                l.FormatTime = value.Substring(0, value.IndexOf("]"));
                l.Value = value.Substring(value.IndexOf("]") + 1);

                if (i == 0) //是否第一个
                {
                    l.IsFirstOne = true;
                    l.FirstOneMargin = "0 " + (ThisWindow.ListBoxContrainer.ActualHeight / 2) + " 0 0";
                }
                if (i == list.Count - 1) //是否最后一个
                {
                    l.LastOne = true;
                    l.LastMargin = "0 0 0 " + (ThisWindow.ListBoxContrainer.ActualHeight / 2);
                }
                lyricItmes.Add(l);
            }

            Model.LyricList = lyricItmes;
            //开启定时器
            StartTimer();
        }

       
        /// <summary>
        /// 开启歌词滚动
        /// </summary>
        private void StartTimer()
        {
            var songId = Model.SongId;
            List<LyricItme> lyricList = Model.LyricList;
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (timer != null)
                {
                    timer.Stop();
                    timer = null;
                }
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(50);
                timer.Tick += new EventHandler((s, e) =>
                {
                    //当前播放器进度
                    double position = PlayerViewModel.This.Model.MediaElement.Position.TotalSeconds;
                    //找到歌词索引
                    var list = lyricList.FindAll(t => t.Time < position);//找到大于当前进度的歌词
                    if (list.Count == 0) return;//找不到歌词(到底了)
                    double time = list.Max(t => t.Time);
                    int index = lyricList.FindIndex(t => t.Time.Equals(time));
                    //当前歌词是空白
                    if (index == -1 || lyricList[index].Value.Equals("")) return;
                    //如果当前歌词已经是焦点就返回
                    if(lyricList[index].IsFocus.Equals(true)) return;
                    PositionLyrics(index);
                });
                timer.Start();
            }));
        }

        /// <summary>
        /// 定位歌词
        /// </summary>
        /// <param name="index"></param>
        public void PositionLyrics(int index)
        {
            List<LyricItme> lyricList = Model.LyricList;

            //把之前焦点设置false
            lyricList.ForEach(item =>
            {
                if (item.IsFocus == true)
                    item.IsFocus = false;
            });
            lyricList[index].IsFocus = true;//找到歌词焦点
            ThisWindow.LyricList.ScrollToCenterOfView(ThisWindow.LyricList.Items[index]);
        }

    }
    



}
