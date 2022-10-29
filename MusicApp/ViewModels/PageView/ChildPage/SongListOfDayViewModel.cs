using MusicApp.Common;
using MusicApp.Models.PageView.ChildPage;
using MusicApp.Models.Vo;
using MusicApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MusicApp.ViewModels.PageView.ChildPage
{
    public class SongListOfDayViewModel
    {
        public static SongListOfDayViewModel This { get; set; }
        public SongListOfDayModel Model { get; set; }

        //全部播放
        public CommandBase PlayAllClickCommand { get; set; }
        public SongListOfDayViewModel()
        {
            This = this;
            Model = new SongListOfDayModel();

            //全部播放
            PlayAllClickCommand = new CommandBase();
            PlayAllClickCommand.DoExecute = new Action<object>((o) => PlayAllClick());
            PlayAllClickCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            GetRecommendSong();
        }

        /// <summary>
        /// 播放全部
        /// </summary>
        public void PlayAllClick()
        {
            SongPlayListViewModel.This.Model.SongLists = new List<Models.SongModel>();
            var list = new List<string>();
            Model.ListSource.ForEach(item =>
            {
                list.Add(item.id.ToString());
            });
            SongPlayListViewModel.This.GetSongPlayList(list);
        }

        /// <summary>
        /// 获取推荐歌曲
        /// </summary>
        /// <param name="isPlayAll">是否播放全部</param>
        public void GetRecommendSong(bool isPlayAll = false)
        {
            new Thread(() =>
            {
                string newsongResult = HttpUtil.HttpRequset(HttpUtil.serveUrl + "/personalized/newsong?limit=30");
                if (newsongResult == null)
                    return;
                NewSongModel res = JsonConvert.DeserializeObject<NewSongModel>(newsongResult);
                var itemList = res.result;
                for (int i = 0; i < itemList.Count; i++)
                {
                    var num = (i + 1).ToString();
                    itemList[i].num = num.Length == 1 ? "0" + num : num;
                    itemList[i].formatTime = StringUtil.FormatTimeoutToString(itemList[i].song.duration);
                }
                Model.ListSource = res.result;

                if (isPlayAll)
                {
                    PlayAllClick();
                }
            }).Start();
        }
    }
}
