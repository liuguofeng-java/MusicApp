using MusicApp.Common;
using MusicApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace MusicApp.ViewModels
{
    public class PlayerViewModel
    {

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
                    InitPlay(songPlay, false);
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
                if (time - Model.PlayProgress > 1 && Model.PlayProgress != 0)
                {
                    Model.MediaElement.Position = TimeSpan.FromSeconds(time);
                    Model.PlayProgress = TimeSpan.FromSeconds(time).TotalSeconds;
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
        /// <param name="isStartPlay">是否播放</param>
        public void InitPlay(SongModel model, bool isStartPlay = true)
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
                //先要停止歌曲
                if (Model.Timer != null)
                {
                    Model.Timer.Stop();
                }
                //初始化控件数据
                Model.PlayProgress = 0;
                Model.StartProgressTiem = "00:00";
                Model.EndProgressTiem = "00:00";
                Model.PlayProgressLength = 1;
                Model.DisabledPlayProgress = false;
                Model.PlayButContent = "\xe87c";
                //先隐藏歌曲信息控件
                SongInfoViewModel.This.Model.SongInfoVisibility = Visibility.Hidden;
                //先停止正在播放的音乐
                Application.Current.Dispatcher.Invoke(new Action(delegate
                {
                    Model.MediaElement.Stop();
                }));
                //更新当前歌曲信息
                Model.SongPlayModel = model;

                //初始化列表颜色
                SongPlayListViewModel.This.SetLisBoxColor(model);
                //歌曲详情赋值
                SongInfoViewModel.This.SetSongInfo(model, isStartPlay);
                //下载歌曲到本地
                GetSongUrl(model);
                if (model.LocalSongUrl == null)
                    return;

                //更新歌曲
                Application.Current.Dispatcher.Invoke(new Action(delegate
                {
                    Model.MediaElement.Source = new Uri(model.LocalSongUrl);
                    //是否要播放
                    if (isStartPlay)
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
                        var totalSeconds = Model.MediaElement.Position.TotalSeconds;
                        int second = (int)totalSeconds;//总秒数
                        int minute = second / 60;//分钟数
                        int remSecond = second - (minute * 60);//剩余秒数

                        Model.PlayProgress = totalSeconds;
                        Model.StartProgressTiem = (minute.ToString().Length == 1 ? "0" + minute.ToString() : minute.ToString())
                                                + ":" +
                                                (remSecond.ToString().Length == 1 ? "0" + remSecond.ToString() : remSecond.ToString());
                    });
                    Model.Timer.Start();
                }));

                //保存当前音乐实例到本地
                InitJsonData.jsonDataModel.SongPlay = Model.SongPlayModel;
            }).Start();
        }

        /// <summary>
        /// 进行或者暂停按钮按下
        /// </summary>
        public void PlayButClick()
        {
            Application.Current.Dispatcher.Invoke(new Action(delegate
            {
                if (Model.MediaElement.Position.TotalSeconds == 0)
                {
                    if (Model.SongPlayModel != null)
                    {
                        Model.MediaElement.Play();
                        Model.PlayButContent = "\xea81";//更新按钮图标
                    }
                    return;
                };
                if (Model.PlayButContent.Equals("\xe87c"))
                {
                    Model.PlayButContent = "\xea81";
                    Model.MediaElement.Play();
                }
                else
                {
                    Model.PlayButContent = "\xe87c";
                    Model.MediaElement.Pause();
                }
                //设置主窗体任务栏
                MainWindowViewModel.This.SetTaskbarStat(Model.SongPlayModel.SongName,
                    Model.PlayButContent.Equals("\xe87c"), 
                    new BitmapImage(new Uri(Model.SongPlayModel.LocalPicUrl)));
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
            SongPlayListViewModel.This.SetLisBoxColor(new SongModel());//初始化列表颜色
            SongInfoViewModel.This.Model.SongInfoVisibility = Visibility.Hidden;//隐藏歌曲信息
            Model.SongPlayModel = null;
            //设置主窗体任务栏
            MainWindowViewModel.This.SetTaskbarStat("网易云", true, null);
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
                    Directory.CreateDirectory(path);//文件夹没有就创建
                    string res = HttpUtil.HttpDownload(model.SongUrl, path, fileName);
                    if (res == null) return;
                    model.LocalSongUrl = res;
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
