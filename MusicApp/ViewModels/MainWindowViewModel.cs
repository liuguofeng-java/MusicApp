using MusicApp.Common;
using MusicApp.Models;
using System;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace MusicApp.ViewModels
{
    /// <summary>
    /// 主窗体
    /// </summary>
    public class MainWindowViewModel
    {
        //事件委托
        public Action<object> BaseBorderMouseDownDelegate { get; set; }
        public static MainWindowViewModel This { get; set; }
        public MainWindowModel Model { get; set; }

        //点击最底层border
        public CommandBase BaseBorderMouseDownCommand { get; set; }

        //点击带播放列表
        public CommandBase SongPlayListClickCommand { get; set; }
        public MainWindowViewModel()
        {
            This = this;
            Model = new MainWindowModel();
            //初始化第一页
            Model.MenusChecked = MenusChecked.FoundMusicPage;

            //点击最底层border
            BaseBorderMouseDownCommand = new CommandBase();
            BaseBorderMouseDownCommand.DoExecute = new Action<object>((o) => 
            {
                BaseBorderMouseDownDelegate.Invoke(o);
            });
            BaseBorderMouseDownCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //点击带播放列表
            SongPlayListClickCommand = new CommandBase();
            SongPlayListClickCommand.DoExecute = new Action<object>((o) =>
            {
                var songPlayListView = SongPlayListViewModel.This;

                if (songPlayListView.Model.PlayListVisibility == Visibility.Visible)
                {
                    songPlayListView.Model.PlayListVisibility = Visibility.Collapsed;
                }
                else
                {
                    songPlayListView.Model.PlayListVisibility = Visibility.Visible;
                }
            });
            SongPlayListClickCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });
        }


    }
}
