using MusicApp.Common;
using MusicApp.Models;
using MusicApp.Models.Vo;
using MusicApp.Models.Widget;
using MusicApp.ViewModels.PageView.ChildPage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MusicApp.ViewModels.Widget
{

    /// <summary>
    /// 推荐歌歌单列表
    /// </summary>
    public class SongsViewModel
    {
        public SongsModel Model { get; set; }
        //点击详情
        public CommandBase SongDetailsClickCommand { get; set; }
        
        //点击全部播放
        public CommandBase PlayAllClickCommand { get; set; }

        public SongsViewModel()
        {
            Model = new SongsModel();

            //点击详情
            SongDetailsClickCommand = new CommandBase();
            SongDetailsClickCommand.DoExecute = new Action<object>((o) => 
            {
                var flag = o.ToString();
                if (flag.Equals("#"))
                {
                    MainWindowViewModel.GetInstance().Model.MenusChecked = MenusChecked.SongListOfDayPage;
                }
                else
                {
                    MainWindowViewModel.GetInstance().Model.SetMenusChecked(MenusChecked.SongListDetailsPage,flag);
                }
            });
            SongDetailsClickCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //点击全部播放
            PlayAllClickCommand = new CommandBase();
            PlayAllClickCommand.DoExecute = new Action<object>((o) =>
            {
                if (o == null)
                {
                    SongListOfDayViewPageModel.This.GetRecommendSong(true);
                }
                else
                {
                    var songListDetails = new SongListDetailsViewModel(o.ToString());
                    songListDetails.PlayAllClick();
                }
            });
            PlayAllClickCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });


            GetSongs();
        }

        /// <summary>
        /// 初始化歌单
        /// </summary>
        public void GetSongs()
        {
            //接收数据
            string result = HttpUtil.HttpRequset(HttpUtil.serveUrl + "/top/playlist?limit=9");
            if (result == null)
            {
                return;
            }
            PlayListResultModel data = JsonConvert.DeserializeObject<PlayListResultModel>(result);
            Model.Playlists = new List<Playlists>();
            Model.Playlists.Add(new Playlists
            {
                SongListVisibility = Visibility.Collapsed,
                SongVisibility = Visibility.Visible,
            });
            data.playlists.ForEach(item =>
            {
                Model.Playlists.Add(new Playlists
                {
                    SongListVisibility = Visibility.Visible,
                    SongVisibility = Visibility.Collapsed,
                    Name = item.name,
                    Id = item.id,
                    UserId = item.userId,
                    CoverImgUrl = item.coverImgUrl,
                    Description = item.description,
                    Tags = item.tags,
                    PlayCount = item.playCount/10000,
                    CommentCount = item.commentCount,
                });
            });
        }


    }
}
