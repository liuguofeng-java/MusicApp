using MusicApp.Common;
using MusicApp.Control;
using MusicApp.Models;
using MusicApp.Models.Vo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace MusicApp.ViewModels
{
    public class SongDetailViewModel
    {
        public static SongDetailViewModel This { get; set; }
        public SongDetailModel Model { get; set; }
        public SongDetailControl ThisWindow { get; set; }

        private DispatcherTimer timer;
        public SongDetailViewModel(SongDetailControl ThisWindow)
        {
            This = this;
            this.ThisWindow = ThisWindow;
            Model = new SongDetailModel();
        }

        /// <summary>
        /// 初始化歌词
        /// </summary>
        /// <param name="songModel"></param>
        public void InitLyrics(SongModel songModel)
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

                List<LyricItme> lyricItmes = new List<LyricItme>();
                //把歌词分开
                List<string> lists = new List<string>(songModel.Lyric.Split("\n"));
                lists.RemoveAt(lists.Count - 1); //删除最后一个空数据

                for (int i = 0; i < lists.Count; i++)
                {
                    var item = lists[i];
                    LyricItme l = new LyricItme();
                    if (i == 0) //是否第一个
                    {
                        l.IsFirstOne = true;
                        l.FirstOneMargin = "0 " + (ThisWindow.ListBoxContrainer.ActualHeight / 2) + " 0 0";
                    }
                    if (i == lists.Count - 1) //是否最后一个
                    {
                        l.LastOne = true;
                        l.LastMargin = "0 0 0 " + (ThisWindow.ListBoxContrainer.ActualHeight / 2);
                    }
                    //计算歌词的时间
                    int index = item.IndexOf("]");
                    l.Value = item.Substring(index + 1);
                    l.FormatTime = item.Substring(1, index - 1);
                    //计算秒
                    var dateStr = l.FormatTime.Split(":");
                    l.Time = (Convert.ToInt32(dateStr[0]) * 60) + Convert.ToDouble(dateStr[1]);
                    lyricItmes.Add(l);
                }

                //更新数据
                Model.Author = songModel.Author;
                Model.SongName = songModel.SongName;
                Model.LyricList = lyricItmes;
                //开启定时器
                StartTimer();
            }).Start();

        }




        private void StartTimer()
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (timer != null) timer.Stop();
                if (timer == null)
                {
                    timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromMilliseconds(500);
                }
                timer.Tick += new EventHandler((s, e) =>
                {
                    List<LyricItme> lyricList = Model.LyricList;
                    double totalSeconds = PlayerViewModel.This.Model.MediaElement.Position.TotalSeconds;
                    for (int i = 0; i < lyricList.Count; i++)
                    {
                        //找到当前歌词
                        if (totalSeconds <= lyricList[i].Time
                            && lyricList[i].Time - totalSeconds < 1
                            && !string.IsNullOrEmpty(lyricList[i].Value))
                        {
                            lyricList[i].IsFocus = true;//找到这点

                            var x = ThisWindow.LyricScrollViewer.ScrollableHeight / lyricList.Count;
                            ThisWindow.LyricScrollViewer.ScrollToVerticalOffset(x * i);
                            for (int j = 0; j < lyricList.Count; j++)//把之前设置false
                            {
                                if (lyricList[j].IsFocus == true && j != i)
                                {
                                    lyricList[j].IsFocus = false;
                                }
                            }
                            break;
                        }
                    }

                });
                timer.Start();
            }));
        }
    }
}
