﻿using MusicApp.Common;
using MusicApp.Control;
using MusicApp.Models;
using MusicApp.Models.Vo;
using MusicApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;

namespace MusicApp.ViewModels
{
    public class SongPlayListViewModel
    {
        public static SongPlayListViewModel This { get; set; }
        public SongPlayListModel Model { get; set; }

        //双击列表播放歌曲
        public CommandBase PlayListMouseDoubleClickCommand { get; set; }

        //清空列表
        public CommandBase ClosePlayListCommand { get; set; }
        public SongPlayListViewModel()
        {
            This = this;
            Model = new SongPlayListModel();

            //双击列表播放歌曲
            PlayListMouseDoubleClickCommand = new CommandBase();
            PlayListMouseDoubleClickCommand.DoExecute = new Action<object>((o) => 
            {
                int selectdindex = (int)o;
                var item = Model.SongLists[selectdindex];
                if (item == null) return;
                NextSongPlay(item.SongId, false, 3);
            });
            PlayListMouseDoubleClickCommand.DoCanExecute = new Func<object, bool>((o) => {return true; });


            //清空列表
            ClosePlayListCommand = new CommandBase();
            ClosePlayListCommand.DoExecute = new Action<object>((o) =>
            {
                Model.SongLists = new List<SongModel>();
                PlayerViewModel.This.StopPlay();
            });
            ClosePlayListCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });


            //点击主窗体关闭正在播放列表
            MainWindowViewModel.This.BaseBorderMouseDownDelegate += new Action<object>((o) =>
            {
                Model.PlayListVisibility = Visibility.Collapsed;
            });

            //默认是关闭状态
            Model.PlayListVisibility = Visibility.Collapsed;
            //总条数
            Model.SongPlayCount = "总" + Model.SongLists.Count + "首";
            //初始化数据
            Model.SongLists = InitJsonData.jsonDataModel.SongPlayList;
        }



        /// <summary>
        /// 播放下一首
        /// </summary>
        /// <param name="songId">当前播放完的歌曲id</param>
        /// <param name="isLast">是否是上一首</param>
        /// <param name="type">1 顺序播放,2 列表循环,3 单曲循环</param>
        public void NextSongPlay(string songId, bool isLast = false, int type = 1)
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
                case 1:
                    if (count > index + 1)
                        PlayerViewModel.This.InitPlay(Model.SongLists[index + 1]);
                    break;
                case 2:
                    if (count > index + 1)
                        PlayerViewModel.This.InitPlay(Model.SongLists[index + 1]);
                    else
                        PlayerViewModel.This.InitPlay(Model.SongLists[0]);
                    break;
                case 3:
                    PlayerViewModel.This.InitPlay(Model.SongLists[index]);
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
                List<SongModel> songPlayList = Model.SongLists;
                //临时变量
                List<SongModel> list = new List<SongModel>();
                for (int i = 0; i < idList.Count; i++)
                {
                    if (songPlayList == null) songPlayList = new List<SongModel>();
                    //是否已经存在播放列表
                    SongModel model = songPlayList.Find(t => t.SongId.Equals(idList[i]));
                    if (model != null)
                    {
                        model.SongUrl = playerModel.data[i].url;
                        list.Add(model);
                    }
                    else
                    {
                        SongModel newModel = new SongModel();
                        newModel.SongId = idList[i];
                        newModel.SongUrl = playerModel.data[i].url;
                        newModel.PicUrl = detailModel.songs[i].al.picUrl;
                        newModel.SongName = detailModel.songs[i].name;
                        newModel.Author = detailModel.songs[i].ar[0].name;
                        newModel.SongTime = playerModel.data[i].time;
                        //计算歌曲时间
                        int second = newModel.SongTime / 1000;//总秒数
                        int minute = second / 60;//分钟数
                        int remSecond = second - (minute * 60);//剩余秒数
                        newModel.FormatSongTime = (minute.ToString().Length == 1 ? "0" + minute.ToString() : minute.ToString())
                                                  + ":" +
                                                 (remSecond.ToString().Length == 1 ? "0" + remSecond.ToString() : remSecond.ToString());//总时长
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
