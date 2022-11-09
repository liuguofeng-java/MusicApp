using MusicApp.Common;
using MusicApp.Models;
using MusicApp.Models.PageView.ChildPage;
using MusicApp.Models.Vo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Windows.Documents;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace MusicApp.ViewModels.PageView.ChildPage
{
    public class SongListDetailsViewModel
    {
        public SongListDetailsModel Model { get; set; }

        //展开介绍
        public CommandBase NoteButClickCommand { get; set; }
        //全部播放
        public CommandBase PlayAllClickCommand { get; set; }
        //播放选中的一个
        public CommandBase PlaySongClickCommand { get; set; }
        //排序
        public CommandBase SortClickCommand { get; set; }
        public SongListDetailsViewModel(string songsId)
        {
            Model = new SongListDetailsModel();
            Model.SongsId = songsId;

            //展开介绍
            NoteButClickCommand = new CommandBase();
            NoteButClickCommand.DoExecute = new Action<object>((o) =>
            {
                if (Model.NoteTextHeight.Equals(20))
                {
                    Model.NoteTextHeight = -1;
                    Model.NoteButText = "\xeb6d";
                }
                else
                {
                    Model.NoteTextHeight = 20;
                    Model.NoteButText = "\xe65c";
                }
            });
            NoteButClickCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //全部播放
            PlayAllClickCommand = new CommandBase();
            PlayAllClickCommand.DoExecute = new Action<object>((o) => PlayAllClick());
            PlayAllClickCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //播放选中的一个
            PlaySongClickCommand = new CommandBase();
            PlaySongClickCommand.DoExecute = new Action<object>((o) =>
            {
                SongPlayListViewModel.This.GetSongPlayList(new List<string>
                {
                    Model.SongList[(int)o].id.ToString()
                });
            });
            PlaySongClickCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //排序
            SortClickCommand = new CommandBase();
            SortClickCommand.DoExecute = new Action<object>((o) => SortClick(Convert.ToInt32(o)));
            SortClickCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //初始化数据
            InitData();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        public void InitData()
        {
            //歌曲详情
            new Thread(() =>
            {
                string playlistResult = HttpUtil.HttpRequset(HttpUtil.serveUrl + "/playlist/detail?id=" + Model.SongsId);
                if (playlistResult == null)
                {
                    Model.BaseErrorVisibility = Visibility.Visible;
                    return;
                }
                Model.BaseLoadVisibility = Visibility.Collapsed;
                PlaylistDetailResultModel playlist = JsonConvert.DeserializeObject<PlaylistDetailResultModel>(playlistResult);

                Model.Playlist = playlist.playlist;

                DateTime dtStart = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
                long lTime = Model.Playlist.createTime * 10000;
                TimeSpan timeSpan = new TimeSpan(lTime);
                DateTime targetDt = dtStart.Add(timeSpan).AddHours(8);
                Model.Playlist.createDataTime = targetDt.ToString("yyyy-MM-dd创建");
            }).Start();

            //全部歌曲
            new Thread(() =>
            {
                string playlistSongSResult = HttpUtil.HttpRequset(HttpUtil.serveUrl + "/playlist/track/all?limit=1000&offset=1&id=" + Model.SongsId);
                if (playlistSongSResult == null)
                {
                    Model.TableErrorVisibility = Visibility.Visible;
                    return;
                }
                PlaylistSongsResultModel playlistSongsResult = JsonConvert.DeserializeObject<PlaylistSongsResultModel>(playlistSongSResult);
                var itemList = playlistSongsResult.songs;

                for (int i = 0; i < itemList.Count; i++)
                {
                    var num = (i + 1).ToString();
                    itemList[i].num = num.Length == 1 ? "0" + num : num;
                    itemList[i].formatTime = StringUtil.FormatTimeoutToString(itemList[i].dt);
                }
                Model.SongList = itemList;
                Model.TableLoadVisibility = Visibility.Collapsed;
            }).Start();

        }

        /// <summary>
        /// 全部播放
        /// </summary>
        public void PlayAllClick()
        {
            new Thread(() =>
            {
                //自旋等待请求到结果
                if (Model.SongList == null)
                {
                    Thread.Sleep(50);
                    PlayAllClick();
                    return;
                }
                SongPlayListViewModel.This.Model.SongLists = new List<Models.SongModel>();
                var list = new List<string>();
                Model.SongList.ForEach(item =>
                {
                    list.Add(item.id.ToString());
                });
                SongPlayListViewModel.This.GetSongPlayList(list);
            }).Start();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="number">第几个</param>
        public void SortClick(int number)
        {

            var list = new List<SongsItem>(Model.SongList);

            var headInfos = new List<string>(Model.HeadInfos);
            if (Model.TemSongList == null)
            {
                Model.TemSongList = new List<SongsItem>(Model.SongList);
            }

            switch (number)
            {
                case 1:
                    if (headInfos[number - 1].Equals("\xe632默认排序"))
                    {
                        list = list.OrderBy(t => t.name).ToList();
                        headInfos[number - 1] = "\xe660正排序";
                    }
                    else if (headInfos[number - 1].Equals("\xe660正排序"))
                    {
                        list = list.OrderByDescending(t => t.name).ToList();
                        headInfos[number - 1] = "\xe65f倒排序";
                    }
                    else
                    {
                        list = new List<SongsItem>(Model.TemSongList);
                        headInfos[number - 1] = "\xe632默认排序";
                    }
                    break;
                case 2:
                    if (headInfos[number - 1].Equals("\xe632默认排序"))
                    {
                        list = list.OrderBy(t => t.ar[0].name).ToList();
                        headInfos[number - 1] = "\xe660正排序";
                    }
                    else if (headInfos[number - 1].Equals("\xe660正排序"))
                    {
                        list = list.OrderByDescending(t => t.ar[0].name).ToList();
                        headInfos[number - 1] = "\xe65f倒排序";
                    }
                    else
                    {
                        list = new List<SongsItem>(Model.TemSongList);
                        headInfos[number - 1] = "\xe632默认排序";
                    }
                    break;
                case 3:
                    if (headInfos[number - 1].Equals("\xe632默认排序"))
                    {
                        list = list.OrderBy(t => t.al.name).ToList();
                        headInfos[number - 1] = "\xe660正排序";
                    }
                    else if (headInfos[number - 1].Equals("\xe660正排序"))
                    {
                        list = list.OrderByDescending(t => t.al.name).ToList();
                        headInfos[number - 1] = "\xe65f倒排序";
                    }
                    else
                    {
                        list = new List<SongsItem>(Model.TemSongList);
                        headInfos[number - 1] = "\xe632默认排序";
                    }
                    break;
                case 4:
                    if (headInfos[number - 1].Equals("\xe632"))
                    {
                        list = list.OrderBy(t => t.formatTime).ToList();
                        headInfos[number - 1] = "\xe660";
                    }
                    else if (headInfos[number - 1].Equals("\xe660"))
                    {
                        list = list.OrderByDescending(t => t.formatTime).ToList();
                        headInfos[number - 1] = "\xe65f";
                    }
                    else
                    {
                        list = new List<SongsItem>(Model.TemSongList);
                        headInfos[number - 1] = "\xe632";
                    }
                    break;
            }

            for (int i = 0; i < list.Count; i++)
            {
                var num = (i + 1).ToString();
                list[i].num = num.Length == 1 ? "0" + num : num;
            }

            Model.SongList = list;
            Model.HeadInfos = headInfos;


        }

    }
}
