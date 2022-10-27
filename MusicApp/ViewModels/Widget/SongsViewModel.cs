using MusicApp.Common;
using MusicApp.Models;
using MusicApp.Models.Vo;
using MusicApp.Models.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.ViewModels.Widget
{
    public class SongsViewModel
    {
        public SongsModel Model { get; set; }
        //点击每日推荐歌曲列表
        public CommandBase SongListOfDayClickCommand { get; set; }

        public SongsViewModel()
        {
            Model = new SongsModel();

            //点击每日推荐歌曲列表
            SongListOfDayClickCommand = new CommandBase();
            SongListOfDayClickCommand.DoExecute = new Action<object>((o) => 
            {
                MainWindowViewModel.This.Model.MenusChecked = MenusChecked.SongListOfDay;
            });
            SongListOfDayClickCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });
        }

        
    }
}
