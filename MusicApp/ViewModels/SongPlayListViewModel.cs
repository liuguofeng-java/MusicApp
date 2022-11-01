using MusicApp.Common;
using MusicApp.Control;
using MusicApp.Models;
using MusicApp.Models.Vo;
using MusicApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Media.Media3D;

namespace MusicApp.ViewModels
{
    public class SongPlayListViewModel
    {
        public static SongPlayListViewModel This { get; set; }
        public SongPlayListModel Model { get; set; }

        //播放歌曲
        public CommandBase PlaySongClickCommand { get; set; }

        //点击删除一个
        public CommandBase DeleteSongClickCommand { get; set; }

        //清空列表
        public CommandBase ClosePlayListCommand { get; set; }
        public SongPlayListViewModel()
        {
            This = this;
            Model = new SongPlayListModel();

            //播放歌曲
            PlaySongClickCommand = new CommandBase();
            PlaySongClickCommand.DoExecute = new Action<object>((o) => 
            {
                var item = Model.SongLists[(int)o];
                if (item == null) return;
                NextSongPlay(item.SongId, false, PlayModel.SimpleLoop);
            });
            PlaySongClickCommand.DoCanExecute = new Func<object, bool>((o) => {return true; });

            //点击删除一个
            DeleteSongClickCommand = new CommandBase();
            DeleteSongClickCommand.DoExecute = new Action<object>((o) =>
            {
                var list = new List<SongModel>(Model.SongLists);
                list.RemoveAt((int)o);
                Model.SongLists = list;
            });
            DeleteSongClickCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //清空列表
            ClosePlayListCommand = new CommandBase();
            ClosePlayListCommand.DoExecute = new Action<object>((o) =>
            {
                Model.SongLists = new List<SongModel>();
                PlayerViewModel.This.StopPlay();
            });
            ClosePlayListCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });


            //点击主窗体关闭正在播放列表
            MainWindowViewModel.GetInstance().BaseBorderMouseDownDelegate += new Action<object>((o) =>
            {
                Model.PlayListVisibility = Visibility.Collapsed;
            });

            //默认是关闭状态
            Model.PlayListVisibility = Visibility.Collapsed;
            //总条数
            Model.SongPlayCount = "总" + Model.SongLists.Count + "首";
            //初始化数据
            Model.SongLists = InitJsonData.jsonDataModel.SongPlayList;

            //播放器事件
            PlayerViewModel.This.PlayDelegate += new Action<SongModel>((o) =>
            {
                SetLisBoxColor(o);
            });
        }

        /// <summary>
        /// 播放下一首
        /// </summary>
        /// <param name="songId">当前播放完的歌曲id</param>
        /// <param name="isLast">是否是上一首</param>
        /// <param name="type"> ListLoop=列表循环 ,SimpleLoop=单曲循环 ,RandomPlay=随机循环 ,OrderPlay=顺序循环</param>
        public void NextSongPlay(string songId, bool isLast = false, PlayModel type = PlayModel.ListLoop)
        {
            //待播放列表长度
            int count = Model.SongLists.Count;
            if (count == 0) return;
            if (songId == null)
            {
                PlayerViewModel.This.InitPlay(Model.SongLists[0]);
                return;
            };
            //当前播放完的下标
            int index = Model.SongLists.FindIndex(t => t.SongId.Equals(songId));

            if (isLast)
            {
                if (index - 1 < 0)
                    PlayerViewModel.This.InitPlay(Model.SongLists[count - 1]);
                else
                    PlayerViewModel.This.InitPlay(Model.SongLists[index - 1]);
                return;
            }
            switch (type)
            {
                case PlayModel.ListLoop:
                    if (count > index + 1)
                        PlayerViewModel.This.InitPlay(Model.SongLists[index + 1]);
                    else
                        PlayerViewModel.This.InitPlay(Model.SongLists[0]);
                    break;
                case PlayModel.SimpleLoop:
                    PlayerViewModel.This.InitPlay(Model.SongLists[index]);
                    break;

                case PlayModel.RandomPlay:
                    int random = new Random().Next(0,count + 1);
                    PlayerViewModel.This.InitPlay(Model.SongLists[random]);
                    break;
                case PlayModel.OrderPlay:
                    if (count > index + 1)
                        PlayerViewModel.This.InitPlay(Model.SongLists[index + 1]);
                    break;

            }
        }

        /// <summary>
        /// 更改列表状态
        /// </summary>
        /// <param name="model"></param>
        public void SetLisBoxColor(SongModel model)
        {
            //待播放歌曲列表
            List<SongModel> list = Model.SongLists;
            list.ForEach(item =>
            {
                if (item.IsSelected == true)
                {
                    item.IsSelected = false;
                }
            });
            SongModel songModel = list.Find(t => t.SongId.Equals(model.SongId));
            if (songModel == null) return;
            songModel.IsSelected = true;

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

                //待播放歌曲列表
                List<SongModel> songPlayList = Model.SongLists;
                //临时变量
                List<SongModel> list = new List<SongModel>();
                for (int i = 0; i < idList.Count; i++)
                {
                    if (songPlayList == null) songPlayList = new List<SongModel>();
                    //是否已经存在播放列表
                    SongModel model = songPlayList.Find(t => t.SongId.Equals(idList[i]));
                    var detail = detailModel.songs.Find(t => t.id.ToString().Equals(idList[i]));
                    if (model != null)
                    {
                        list.Add(model);
                    }
                    else
                    {
                        SongModel newModel = new SongModel();
                        newModel.SongId = idList[i];
                        newModel.PicUrl = detail.al.picUrl;
                        newModel.SongName = detail.name;
                        newModel.Author = detail.ar[0].name;
                        newModel.SongTime = detail.dt;
                        newModel.FormatSongTime = StringUtil.FormatTimeoutToString(newModel.SongTime);
                        list.Add(newModel);
                    }

                }
                //之前播放列表添加到新列表中
                songPlayList.ForEach(item =>
                {
                    if (idList.Find(t => t.Equals(item.SongId)) == null)
                    {
                        list.Add(item);
                    }
                });

                //重新赋值
                Model.SongLists = list;
                Model.SongPlayCount = "总" + Model.SongLists.Count + "首";

                //保存待播放歌曲列表
                InitJsonData.jsonDataModel.SongPlayList = Model.SongLists;

                //开始播放
                PlayerViewModel.This.InitPlay(list[0]);

            }).Start();
        }


    }
}
