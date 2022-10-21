using MusicApp.Common;
using MusicApp.Models;
using MusicApp.Models.Vo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace MusicApp.Control
{
    /// <summary>
    /// 歌曲详情,展示正在播放音乐的歌词等信息 的交互逻辑
    /// </summary>
    public partial class SongDetailControl : UserControl
    {

        private DispatcherTimer timer;

        private SongDetailModel songDetailModel;

        private ControlBean bean = ControlBean.getInstance();
        public SongDetailControl()
        {
            InitializeComponent();
            songDetailModel = new SongDetailModel();

            //解决ListBox不能滚动的问题
            LyricList.PreviewMouseWheel += (s, e) =>
            {
                if (!e.Handled)
                {
                    // ListView拦截鼠标滚轮事件
                    e.Handled = true;
                    // 激发一个鼠标滚轮事件，冒泡给外层ListView接收到
                    var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                    eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                    eventArg.Source = s;
                    var parent = ((ListBox)s).Parent as UIElement;
                    parent.RaiseEvent(eventArg);
                }
            };
        }

        /// <summary>
        /// 初始化歌词
        /// </summary>
        /// <param name="songModel"></param>
        public void InitLyrics(SongPlayListModel songModel)
        {
            new Thread(() => 
            {
                //保存歌词
                if (songModel.lyric == null )
                {
                    string lyricsRes = HttpUtil.HttpRequset(HttpUtil.serveUrl + "/lyric?id=" + songModel.songId);
                    if (lyricsRes == null)
                    {
                        return;
                    }
                    LyricsResultModel detailModel = JsonConvert.DeserializeObject<LyricsResultModel>(lyricsRes);
                    string lyrics = detailModel.lrc.lyric;//歌词数据
                    songModel.lyric = lyrics;
                    InitJsonData.WriteJsonFile();//手动更新缓存
                }

                List<LyricItme> lyricItmes = new List<LyricItme>();
                //把歌词分开
                List<string> lists = new List<string>(songModel.lyric.Split("\n"));
                lists.RemoveAt(lists.Count - 1); //删除最后一个空数据

                for (int i = 0; i < lists.Count; i++)
                {
                    var item = lists[i];
                    LyricItme l = new LyricItme();
                    if (i == 0) //是否第一个
                    {
                        l.isFirstOne = true;
                        l.firstOneMargin = "0 " + (ListBoxContrainer.ActualHeight / 2) + " 0 0";
                    }
                    if (i == lists.Count - 1) //是否最后一个
                    {
                        l.lastOne = true;
                        l.lastMargin = "0 0 0 " + (ListBoxContrainer.ActualHeight / 2);
                    }
                    //计算歌词的时间
                    int index = item.IndexOf("]");
                    l.value = item.Substring(index + 1);
                    l.formatTime = item.Substring(1, index - 1);
                    //计算秒
                    var dateStr = l.formatTime.Split(":");
                    l.time = (Convert.ToInt32(dateStr[0]) * 60) + Convert.ToDouble(dateStr[1]);
                    lyricItmes.Add(l);
                }

                //更新数据
                songDetailModel.author = songModel.author;
                songDetailModel.songName = songModel.songName;
                this.Dispatcher.Invoke(new Action(delegate
                {
                    LyricList.ItemsSource = lyricItmes;
                    DataContext = songDetailModel;
                }));

                //开启定时器
                StartTimer();
            }).Start();

        }




        private void StartTimer()
        {
            //计时器更新进度条
            this.Dispatcher.Invoke(new Action(delegate
            {
                if (timer != null) timer.Stop();
                if (timer == null)
                {
                    timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromMilliseconds(500);
                }
                timer.Tick += new EventHandler((s, e) =>
                {
                    List<LyricItme> itemsSource = (List<LyricItme>)LyricList.ItemsSource;
                    double totalSeconds = bean.playerControl.PlayMedia.Position.TotalSeconds;
                    for (int i = 0; i < itemsSource.Count; i++)
                    {
                        if (totalSeconds <= itemsSource[i].time 
                            && itemsSource[i].time - totalSeconds < 1
                            && !string.IsNullOrEmpty(itemsSource[i].value))
                        {
                            itemsSource[i].isFocus = true;//找到这点

                            var x = LyricScrollViewer.ScrollableHeight / itemsSource.Count;
                            LyricScrollViewer.ScrollToVerticalOffset(x * i);
                            for (int j = 0; j < i; j++)//把之前设置false
                            {
                                if (itemsSource[j].isFocus == true)
                                {
                                    itemsSource[j].isFocus = false;
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


