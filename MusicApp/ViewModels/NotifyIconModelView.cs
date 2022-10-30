using MusicApp.Common;
using MusicApp.Models;
using MusicApp.Views;
using System;
using System.Threading;
using System.Windows;

namespace MusicApp.ViewModels
{
    /// <summary>
    /// 程序托盘菜单的ModelView 
    /// </summary>
    public class NotifyIconModelView
    {
        public NotifyIconModel Model { get; set; }
        //点击托盘
        public CommandBase ClickCommand { get; set; }

        //点击播放或暂停
        public CommandBase PlayClickCommand { get; set; }
        //上一首
        public CommandBase LastClickCommand { get; set; }
        //下一首
        public CommandBase NextClickCommand { get; set; }

        //选择播放模式
        public CommandBase CheckPlayModelClickCommand { get; set; }

        //点击退出
        public CommandBase CloseClickCommand { get; set; }

        /// <summary>
        /// 点击右侧任务栏显示主窗体
        /// </summary>
        public NotifyIconModelView()
        {
            Model = new NotifyIconModel();

            //点击托盘
            ClickCommand = new CommandBase();
            ClickCommand.DoExecute = new Action<object>((o) => showWindow());
            ClickCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //播放
            PlayClickCommand = new CommandBase();
            PlayClickCommand.DoExecute = new Action<object>((o) => PlayerViewModel.This.PlayButClick());
            PlayClickCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //上一首
            LastClickCommand = new CommandBase();
            LastClickCommand.DoExecute = new Action<object>((o) => PlayerViewModel.This.PlayLastClick());
            LastClickCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //下一首
            NextClickCommand = new CommandBase();
            NextClickCommand.DoExecute = new Action<object>((o) => PlayerViewModel.This.PlayNextClick());
            NextClickCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //选择播放模式
            CheckPlayModelClickCommand = new CommandBase();
            CheckPlayModelClickCommand.DoExecute = new Action<object>((o) =>
            {
                var playModel = (PlayModel)Enum.Parse(typeof(PlayModel),o.ToString());
                PlayerViewModel.This.SetPlayModelStat(playModel);
            });
            CheckPlayModelClickCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //点击退出
            CloseClickCommand = new CommandBase();
            CloseClickCommand.DoExecute = new Action<object>((o) =>
            {
                Application.Current.Shutdown();
            });
            CloseClickCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            var playThis =  PlayerViewModel.This;
            //播放器事件,更新托盘
            playThis.PlayDelegate += new Action<SongModel>((o) =>
            {
                switch (o.Status)
                {
                    case SongModel.PlayStatus.StartPlay:
                        Model.SongName = o.SongName;
                        Model.IsIsEnabledSongInfo = true;
                        break;
                    case SongModel.PlayStatus.StopPlay:
                        Model.SongName = o.SongName;
                        Model.IsIsEnabledSongInfo = true;
                        break;
                    case SongModel.PlayStatus.ClosePlay:
                        Model.SongName = "没有正在播放的音乐";
                        Model.IsIsEnabledSongInfo = false;
                        break;
                }
            });

            //播放器模式事件触发
            playThis.PlayModelDelegate += new Action<PlayModelStat>((o) =>
            {
                Model.PlayModelStat = o;
            });
        }


        /// <summary>
        /// 显示主窗体
        /// </summary>
        public void showWindow()
        {
            Window mainWindow = Application.Current.MainWindow;
            if (mainWindow.WindowState == WindowState.Minimized)
            {
                mainWindow.WindowState = WindowState.Normal;
            }
            mainWindow.Visibility = Visibility.Visible;
            mainWindow.Topmost = true;
            Thread.Sleep(100);
            mainWindow.Topmost = false;
        }
    }
}
