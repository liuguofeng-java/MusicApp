using MusicApp.Common;
using MusicApp.Models.PageView.ChildPage;
using MusicApp.Models.Vo;
using MusicApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.ViewModels.PageView.ChildPage
{
    public class SongListOfDayViewModel
    {
        public SongListOfDayModel Model { get; set; }
        public SongListOfDayViewModel()
        {
            Model = new SongListOfDayModel();
        }


        /// <summary>
        /// 获取推荐歌曲
        /// </summary>
        public void GetRecommendSong()
        {
            string newsongResult = HttpUtil.HttpRequset(HttpUtil.serveUrl + "/personalized/newsong?limit=30");
            if (newsongResult == null)
                return;
            NewSongModel res = JsonConvert.DeserializeObject<NewSongModel>(newsongResult);
            Console.WriteLine(res);
        }
    }
}
