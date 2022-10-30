using MusicApp.Common;
using MusicApp.Models;
using MusicApp.Models.Vo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using static MusicApp.Models.SongModel;

namespace MusicApp.ViewModels
{
    public class PlayerViewModel
    {
        // 播放器事件委托(停止或者开始)
        public Action<SongModel> PlayDelegate { get; set; }

        public static PlayerViewModel This { get; set; }
        public PlayerModel Model { get; set; }

        //MediaElement控件加载成功
        public CommandBase MediaLoadedCommand { get; set; }
        
        //音乐播放完毕事件
        public CommandBase MediaEndedCommand { get; set; }

        //手动改变音乐进度条时
        public CommandBase MusicProgressChangedCommand { get; set; }

        //点击暂停/继续
        public CommandBase PlayButClickCommand { get; set; }

        //播放上一首
        public CommandBase PlayLastClickCommand { get; set; }

        //播放下一首
        public CommandBase PlayNextClickCommand { get; set; }

        public PlayerViewModel()
        {
            This = this;
            Model = new PlayerModel();
            //MediaElement控件加载成功
            MediaLoadedCommand = new CommandBase();
            MediaLoadedCommand.DoExecute = new Action<object>((o) =>
            {
                Model.MediaElement = (MediaElement)o;
                //初始化数据
                var songPlay = InitJsonData.jsonDataModel.SongPlay;
                //默认播放第一首
                if (songPlay != null)
                {
                    songPlay.Status = SongModel.PlayStatus.StopPlay;
                    InitPlay(songPlay);
                    Model.SongPlayModel = songPlay;
                }
            });
            MediaLoadedCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //音乐播放完毕事件
            MediaEndedCommand = new CommandBase();
            MediaEndedCommand.DoExecute = new Action<object>((o) =>
            {
                var songId = Model.SongPlayModel.SongId;
                StopPlay();
                SongPlayListViewModel.This.NextSongPlay(songId, false); //播放下一首
            });
            MediaEndedCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //手动改变音乐进度条时
            MusicProgressChangedCommand = new CommandBase();
            MusicProgressChangedCommand.DoExecute = new Action<object>((o) =>
            {
                var time = (double)o;
                //后退
                if (Model.MediaElement.Position.TotalSeconds - Model.PlayProgress > 1)
                {
                    Model.MediaElement.Position = TimeSpan.FromSeconds(Model.PlayProgress);
                }
                //前进
                else if (time - Model.PlayProgress > 1 && Model.PlayProgress != 0)
                {
                    Model.MediaElement.Position = TimeSpan.FromSeconds(time);
                    Model.PlayProgress = time;
                }
            });
            MusicProgressChangedCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //点击暂停/继续
            PlayButClickCommand = new CommandBase();
            PlayButClickCommand.DoExecute = new Action<object>((o) => PlayButClick());
            PlayButClickCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //上一首
            PlayLastClickCommand = new CommandBase();
            PlayLastClickCommand.DoExecute = new Action<object>((o) => PlayLastClick());
            PlayLastClickCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //下一首
            PlayNextClickCommand = new CommandBase();
            PlayNextClickCommand.DoExecute = new Action<object>((o) => PlayNextClick());
            PlayNextClickCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });
        }


        /// <summary>
        /// 准备播放
        /// </summary>
        /// <param name="model">音乐信息</param>
        public void InitPlay(SongModel model)
        {
            //点击了相同的歌
            if (Model.SongPlayModel != null && model.SongId.Equals(Model.SongPlayModel.SongId))
            {
                PlayButClick();
                return;
            }

            //下载音乐可能会慢,卡ui线程
            new Thread(() =>
            {
                //初始化控件数据
                Model.PlayProgress = 0;
                Model.StartProgressTiem = "00:00";
                Model.EndProgressTiem = "00:00";
                Model.PlayProgressLength = 1;
                Model.DisabledPlayProgress = false;
                Model.PlayButContent = "\xe87c";
                //先停止正在播放的音乐
                Application.Current.Dispatcher.Invoke(new Action(delegate
                {
                    //关闭进度条定时器
                    if (Model.Timer != null)
                    {
                        Model.Timer.Stop();
                    }
                    //停止音乐
                    Model.MediaElement.Stop();
                }));
                //更新当前歌曲信息
                Model.SongPlayModel = model;
                Model.SongPlayModel.IsLoading = true;

                //触发播放事件
                PlayDelegate?.Invoke(model);

                //下载歌曲到本地
                GetSongUrl(model);
                if (model.LocalSongUrl == null)
                    return;

                //更新歌曲
                Application.Current.Dispatcher.Invoke(new Action(delegate
                {
                    Model.MediaElement.Source = new Uri(model.LocalSongUrl);
                    //是否要播放
                    if (model.Status.Equals(SongModel.PlayStatus.StartPlay))
                    {
                        Model.PlayButContent = "\xea81";//更新按钮图标
                        Model.MediaElement.Play();
                    }
                    Model.DisabledPlayProgress = true;//进度条放开
                    Model.PlayProgress = 0;//初始化进度
                    Model.PlayProgressLength = model.SongTime / 1000; ;//进度条数
                    Model.EndProgressTiem = model.FormatSongTime;//总时长

                    //计时器更新进度条
                    Model.Timer = new DispatcherTimer();
                    Model.Timer.Interval = TimeSpan.FromMilliseconds(1000);
                    Model.Timer.Tick += new EventHandler((s, e) =>
                    {
                        //播放进度
                        var totalSeconds = Model.MediaElement.Position.TotalSeconds;
                        //更新进度条
                        Model.PlayProgress = totalSeconds;
                        Model.StartProgressTiem = StringUtil.FormatTimeoutToString(((int)totalSeconds * 1000));
                    });
                    Model.Timer.Start();
                }));

                //保存当前音乐实例到本地
                InitJsonData.jsonDataModel.SongPlay = Model.SongPlayModel;
                Model.SongPlayModel.IsLoading = false;
            }).Start();
        }

        /// <summary>
        /// 进行或者暂停按钮按下
        /// </summary>
        public void PlayButClick()
        {
            if (Model.SongPlayModel == null) return;
            if (Model.SongPlayModel.IsLoading) return;
            Application.Current.Dispatcher.Invoke(new Action(delegate
            {
                if (Model.MediaElement.Position.TotalSeconds == 0)
                {
                    Model.MediaElement.Play();
                    Model.PlayButContent = "\xea81";//更新按钮图标
                    //更新事件
                    Model.SongPlayModel.Status = PlayStatus.StartPlay;
                    PlayDelegate?.Invoke(Model.SongPlayModel);
                    return;
                };
                if (Model.PlayButContent.Equals("\xe87c"))
                {
                    Model.PlayButContent = "\xea81";
                    Model.SongPlayModel.Status = PlayStatus.StartPlay;
                    Model.MediaElement.Play();
                }
                else
                {
                    Model.PlayButContent = "\xe87c";
                    Model.SongPlayModel.Status = PlayStatus.StopPlay;
                    Model.MediaElement.Pause();
                }
                //更新事件
                PlayDelegate?.Invoke(Model.SongPlayModel);
            }));
        }
        /// <summary>
        /// 上一首
        /// </summary>
        public void PlayLastClick()
        {
            if (Model.SongPlayModel == null) return;
            SongPlayListViewModel.This.NextSongPlay(Model.SongPlayModel.SongId, true, 2);
        }

        /// <summary>
        /// 下一首
        /// </summary>
        public void PlayNextClick()
        {
            if (Model.SongPlayModel == null) return;
            SongPlayListViewModel.This.NextSongPlay(Model.SongPlayModel.SongId, false, 2);
        }

        /// <summary>
        /// 停止播放
        /// </summary>
        public void StopPlay()
        {
            //停止歌曲
            Model.MediaElement.Stop();
            Model.PlayButContent = "\xe87c"; //更新图标
            Model.DisabledPlayProgress = false;//禁用进度条

            Model.SongPlayModel = null;

            //更新事件
            PlayDelegate?.Invoke(new SongModel()
            {
                Status = PlayStatus.ClosePlay,
            });
        }


        /// <summary>
        /// 获取歌曲url
        /// </summary>
        /// <param name="model">播放歌曲信息</param>
        /// <returns></returns>
        private void GetSongUrl(SongModel model)
        {

            //保存本地文件的名
            string fileName = model.SongId + ".mp3";
            string path = Directory.GetCurrentDirectory() + @"\cache\songs";

            //存储图片
            if (model.LocalSongUrl == null || StringUtil.UrlDiscern(model.LocalSongUrl) || !File.Exists(model.LocalSongUrl))
            {
                //如果之前存在过,否则就下载
                if (File.Exists(path + @"\" + fileName))
                {
                    model.LocalSongUrl = path + @"\" + fileName;
                }
                else
                {
                    try
                    {
                        Directory.CreateDirectory(path);//文件夹没有就创建
                                                        //获取歌曲url
                        string songUrlRes = HttpUtil.HttpRequset(HttpUtil.serveUrl + "/song/url?id=" + model.SongId);
                        if (songUrlRes == null)
                        {
                            throw new Exception("获取url失败!");
                            return;
                        }
                        PlayerControlModel playerModel = JsonConvert.DeserializeObject<PlayerControlModel>(songUrlRes);

                        //下载失败就播放下一首
                        string res = HttpUtil.HttpDownload(playerModel.data[0].url, path, fileName);
                        if (res == null)
                        {
                            throw new Exception("下载歌曲失败!");
                        };
                        model.LocalSongUrl = res;
                    }
                    catch
                    {
                        SongPlayListViewModel.This.NextSongPlay(Model.SongPlayModel.SongId, false, 2);

                    }
                }
            }
            InitJsonData.WriteJsonFile();//手动更新缓存

            //如果保存失败
            if (model.LocalSongUrl == null || !File.Exists(model.LocalSongUrl))
            {
                model.LocalSongUrl = null;
            }
        }
    }
}
