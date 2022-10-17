using MusicApp.Common;
using MusicApp.Models.Vo;
using MusicApp.Views;
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
    /// 歌曲搜索框,输入弹出搜索结果 的交互逻辑
    /// </summary>
    public partial class SearchListControl : UserControl
    {
        private ControlBean bean = ControlBean.getInstance();
        public SearchListControl()
        {
            ControlBean.getInstance().searchListControl = this;
            InitializeComponent();

            //解决ListBox不能滚动的问题
            RankingListBox.PreviewMouseWheel += (s, e) =>
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

            //默认隐藏控件
            GridContrainer.Visibility = Visibility.Collapsed;

            //获取排行
            GetRankingList();

            //点击歌曲时触发
            SingleListBox.SelectionChanged += (s, e) =>
            {
                var obj  = SingleListBox.Items[SingleListBox.SelectedIndex];

                if (obj == null) return;

                SearchSongsItem item = (SearchSongsItem)obj;
                List<string> idList = new List<string>();
                idList.Add(item.id.ToString());
                bean.songPlayListControl.GetSongPlayList(idList);
                GridContrainer.Visibility = Visibility.Collapsed;

            };
        }

        /// <summary>
        /// 获取排行榜数据
        /// </summary>
        public void GetRankingList()
        {
            //接收数据
            string result = HttpUtil.HttpRequset(HttpUtil.serveUrl + "/search/hot/detail");
            if (result == null)
            {
                return;
            }
            RankingResultModel data = JsonConvert.DeserializeObject<RankingResultModel>(result);
            for (int i = 0; i < data.data.Count; i++)
            {
                data.data[i].num = i + 1;
                data.data[i].color = i >= 3 ? "#5b5b5b" : "#e63838";
            }

            RankingListBox.ItemsSource = data.data;
        }

        /// <summary>
        /// 获取搜索结果
        /// </summary>
        /// <param name="keyword">搜索内容</param>
        public void GetSearchList(string keyword)
        {
            new Thread(() =>
            {
                if (string.IsNullOrEmpty(keyword)) return;
                //接收数据
                string result = HttpUtil.HttpRequset(HttpUtil.serveUrl + "/search/suggest?keywords=" + keyword);
                if (result == null)
                {
                    return;
                }
                SearchDataResultModel data = JsonConvert.DeserializeObject<SearchDataResultModel>(result);

                //都是空不更新数据
                if (data.result.songs == null &&
                    data.result.artists == null &&
                    data.result.albums == null &&
                    data.result.playlists == null)
                {
                    return;
                }

                //更新数据
                this.Dispatcher.Invoke(new Action(delegate
                {
                    //单曲数据
                    SingleListBox.ItemsSource = data.result.songs;
                    if (data.result.songs == null) SingleText.Visibility = Visibility.Collapsed;
                    ListBox listbox = SingleListBox;

                    //歌手
                    ArtistsListBox.ItemsSource = data.result.artists;
                    if (data.result.artists == null) ArtistsText.Visibility = Visibility.Collapsed;


                    //专辑
                    AlbumListBox.ItemsSource = data.result.albums;
                    if (data.result.albums == null) AlbumText.Visibility = Visibility.Collapsed;


                    //歌单
                    SongsListBox.ItemsSource = data.result.playlists;
                    if (data.result.playlists == null) SongsText.Visibility = Visibility.Collapsed;
                }));

            }).Start();
        }

        /// <summary>
        /// 去除历史按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine();
        }

        /// <summary>
        /// 点击历史清除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void historyBut_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine();
        }

        
    }
}
