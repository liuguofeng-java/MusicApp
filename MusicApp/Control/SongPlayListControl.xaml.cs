using MusicApp.Common;
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

namespace MusicApp.Control
{
    /// <summary>
    /// 歌曲播放列表,主窗体右下方 的交互逻辑
    /// </summary>
    public partial class SongPlayListControl : UserControl
    {
        private ControlBean bean = ControlBean.getInstance();
        public SongPlayListControl()
        {
            ControlBean.getInstance().songPlayListControl = this;
            InitializeComponent();
            SongPlayListContrainer.Visibility = Visibility.Collapsed;

            if (bean.jsonData.songPlayList == null)
            {
                bean.jsonData.songPlayList = new List<SongPlayListModel>();
            }

            //初始化列表
            PlayListBox.ItemsSource = bean.jsonData.songPlayList;

            //解决ListBox不能滚动的问题
            PlayListBox.PreviewMouseWheel += (s, e) =>
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

            //点击两次播放歌曲
            PlayListBox.MouseDoubleClick += (s, e) =>
            {
                var obj = PlayListBox.Items[PlayListBox.SelectedIndex];
                if (obj == null) return;
                var  item = (SongPlayListModel)obj;
                bean.songPlayListControl.NextSongPlay(item.songId, false, 3);
            };

            //点击清空按钮
            CloseList.MouseDown += (s, e) =>
            {
                bean.jsonData.songPlayList = new List<SongPlayListModel>();
                PlayListBox.ItemsSource = bean.jsonData.songPlayList;
                bean.playerControl.StopPlay();
            };

            SongCount.Text = "总" + bean.jsonData.songPlayList.Count + "首";

        }


        /// <summary>
        /// 播放下一首
        /// </summary>
        /// <param name="songId">当前播放完的歌曲id</param>
        /// <param name="isLast">是否是上一首</param>
        /// <param name="type">1 顺序播放,2 列表循环,3 单曲循环</param>
        public void NextSongPlay(string songId, bool isLast = false, int type = 1)
        {
            //待播放歌曲列表
            List<SongPlayListModel> list = bean.jsonData.songPlayList;
            //待播放列表长度
            int count = list.Count;
            if (count == 0) return;
            if (songId == null)
            {
                bean.playerControl.InitPlay(list[0]);
                return;
            };
            //当前播放完的下标
            int index = list.FindIndex(t => t.songId.Equals(songId));
           
            if (isLast)
            {
                if (index - 1 < 0)
                    bean.playerControl.InitPlay(list[count - 1]);
                else
                    bean.playerControl.InitPlay(list[index - 1]);
                return;
            }
            switch (type)
            {
                case 1:
                    if (count > index + 1)
                        bean.playerControl.InitPlay(list[index + 1]);
                    break;
                case 2:
                    if (count > index + 1)
                        bean.playerControl.InitPlay(list[index + 1]);
                    else
                        bean.playerControl.InitPlay(list[0]);
                    break;
                case 3:
                    bean.playerControl.InitPlay(list[index]);
                    break;
            }
        }

        /// <summary>
        /// 更改列表状态
        /// </summary>
        /// <param name="model"></param>
        public void SetLisBoxColor(SongPlayListModel model)
        {
            //待播放歌曲列表
            List<SongPlayListModel> list = bean.jsonData.songPlayList;
            this.Dispatcher.Invoke(new Action(delegate
            {
                list.ForEach(item =>
                {
                    if (item.isSelected == true)
                    {
                        item.isSelected = false;
                    }
                });
                model.isSelected = true;
            }));
            
        }

        /// <summary>
        /// 添加待播放歌曲播放列表
        /// </summary>
        /// <param name="idList">歌曲id</param>
        public void GetSongPlayList(List<string> idList)
        {
            new Thread(() =>
            {
                //格式化id
                StringBuilder builder = new StringBuilder();
                idList.ForEach(item =>
                {
                    builder.Append(item + ",");
                });
                string str = builder.ToString();
                string ids = str.Substring(0, str.Length - 1);
                //获取歌曲详细,歌曲头像\名称\作者
                string songDetailRes = HttpUtil.HttpRequset(HttpUtil.serveUrl + "/song/detail?ids=" + ids);
                if (songDetailRes == null)
                {
                    return;
                }
                SongDetailResultModel detailModel = JsonConvert.DeserializeObject<SongDetailResultModel>(songDetailRes);

                //获取歌曲url
                string songUrlRes = HttpUtil.HttpRequset(HttpUtil.serveUrl + "/song/url?id=" + ids);
                if (songUrlRes == null)
                {
                    return;
                }
                PlayerControlModel playerModel = JsonConvert.DeserializeObject<PlayerControlModel>(songUrlRes);

                //歌曲url和歌曲详细一致
                if (idList.Count != detailModel.songs.Count || idList.Count != playerModel.data.Count)
                    throw new Exception("获取待播放歌曲播放列表出差了!");

                //待播放歌曲列表
                List<SongPlayListModel> songPlayList = bean.jsonData.songPlayList;
                //临时变量
                List<SongPlayListModel> list = new List<SongPlayListModel>();
                for (int i = 0; i < idList.Count; i++)
                {
                    if (songPlayList == null) songPlayList = new List<SongPlayListModel>();
                    //是否已经存在播放列表
                    SongPlayListModel model = songPlayList.Find(t => t.songId.Equals(idList[i]));
                    if (model != null)
                    {
                        model.songUrl = playerModel.data[i].url;
                        list.Add(model);
                    }
                    else
                    {
                        SongPlayListModel newModel = new SongPlayListModel();
                        newModel.songId = idList[i];
                        newModel.songUrl = playerModel.data[i].url;
                        newModel.picUrl = detailModel.songs[i].al.picUrl;
                        newModel.songName = detailModel.songs[i].name;
                        newModel.author = detailModel.songs[i].ar[0].name;
                        newModel.songTime = playerModel.data[i].time;
                        //计算歌曲时间
                        int second = newModel.songTime / 1000;//总秒数
                        int minute = second / 60;//分钟数
                        int remSecond = second - (minute * 60);//剩余秒数
                        newModel.formatSongTime = (minute.ToString().Length == 1 ? "0" + minute.ToString() : minute.ToString())
                                                  + ":" +
                                                 (remSecond.ToString().Length == 1 ? "0" + remSecond.ToString() : remSecond.ToString());//总时长
                        list.Add(newModel);
                    }

                }
                //之前播放列表添加到新列表中
                songPlayList.ForEach(item =>
                {
                    if (idList.Find(t => t.Equals(item.songId)) == null)
                    {
                        list.Add(item);
                    }
                });

                //重新赋值
                this.Dispatcher.Invoke(new Action(delegate
                {
                    bean.jsonData.songPlayList = list;
                    PlayListBox.ItemsSource = list;
                    SongCount.Text = "总"+ list.Count + "首";
                }));
                    
                //开始播放
                bean.playerControl.InitPlay(list[0]);

            }).Start();

            
        }
    }
}
