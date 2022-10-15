using MusicApp.Common;
using MusicApp.Models.Vo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
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

            //初始化列表
            PlayListBox.ItemsSource = bean.jsonData.songPlayList;
        }

        /// <summary>
        /// 获取待播放歌曲播放列表
        /// </summary>
        /// <param name="idList">歌曲id</param>
        public void GetSongPlayList(List<string> idList)
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
            SongDetailModel detailModel = JsonConvert.DeserializeObject<SongDetailModel>(songDetailRes);

            //获取歌曲url
            string songUrlRes = HttpUtil.HttpRequset(HttpUtil.serveUrl + "/song/url?id=" + ids);
            if (songUrlRes == null)
            {
                return;
            }
            PlayerModel playerModel = JsonConvert.DeserializeObject<PlayerModel>(songUrlRes);

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
            bean.jsonData.songPlayList = list;
            PlayListBox.ItemsSource = list;

            //开始播放
            bean.playerControl.StartPlay(list[0]);
        }
    }
}
