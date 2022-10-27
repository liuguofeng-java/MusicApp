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

namespace MusicApp.ViewModels
{
    public class SearchListViewModel
    {
        public static SearchListViewModel This { get; set; }
        public SearchListModel Model { get; set; }
        //点击单曲列表
        public CommandBase SingleSelectionChangedCommand { get; set; }
        public SearchListViewModel()
        {
            This = this;
            Model = new SearchListModel();

            //点击单曲列表
            SingleSelectionChangedCommand = new CommandBase();
            SingleSelectionChangedCommand.DoExecute = new Action<object>(SingleSelectionChangedDoExecute);
            SingleSelectionChangedCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //默认隐藏控件
            Model.GridVisibility = Visibility.Collapsed;

            //获取排行
            GetRankingList();

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
            Model.RankingList = data.data;
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

                //单曲数据
                Model.SingleList = data.result.songs;
                //if (data.result.songs == null) SingleText.Visibility = Visibility.Collapsed;

                //歌手
                Model.ArtistsList = data.result.artists;
                //if (data.result.artists == null) ArtistsText.Visibility = Visibility.Collapsed;


                //专辑
                Model.AlbumList = data.result.albums;
                //if (data.result.albums == null) AlbumText.Visibility = Visibility.Collapsed;


                //歌单
                Model.SongsList = data.result.playlists;
                //if (data.result.playlists == null) SongsText.Visibility = Visibility.Collapsed;

            }).Start();
        }

        /// <summary>
        /// 点击单曲列表
        /// </summary>
        /// <param name="o"></param>
        public void SingleSelectionChangedDoExecute(object o)
        {
            var selectedIndex = Model.SelectdIndex;
            if (selectedIndex == -1) return;
            var item = Model.SingleList[selectedIndex];
            Model.SelectdIndex = -1;
            if (item == null) return;
            List<string> idList = new List<string>();
            idList.Add(item.id.ToString());
            SongPlayListViewModel.This.GetSongPlayList(idList);
            Model.GridVisibility = Visibility.Collapsed;
        }

        /// <summary>
        /// 去除历史按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       /* private void closeText_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine();
        }*/

        /// <summary>
        /// 点击历史清除按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /*private void historyBut_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine();
        }*/

    }
}
