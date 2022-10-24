using MusicApp.Common;
using MusicApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.ViewModels
{
    public class VolumeViewModel
    {
        public VolumeModel Model { get; set; }
        //改变音量事件
        public CommandBase VolumeValueChangedCommand { get; set; }

        //移入音量按钮,打开音量调节
        public CommandBase VolumeButMouseEnterCommand { get; set; }

        //离开border时关闭音量调节
        public CommandBase VolumeMouseLeaveCommand { get; set; }

        //点击静音或者还原
        public CommandBase VolumeButMouseDownCommand { get; set; }
        public VolumeViewModel()
        {
            Model = new VolumeModel();

            //改变音量事件
            VolumeValueChangedCommand = new CommandBase();
            VolumeValueChangedCommand.DoExecute = new Action<object>((o) =>
            {
                if (Model.VolumeValue == 0)
                {
                    Model.VolumeButContent = "\xe610";
                }
                else
                {
                    Model.VolumeButContent = "\xe63c";
                }
                PlayerViewModel.This.Model.VolumeValue = Model.VolumeValue;
                InitJsonData.jsonDataModel.Volume = Model.VolumeValue;
            });
            VolumeValueChangedCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });


            //移入音量按钮,打开音量调节
            VolumeButMouseEnterCommand = new CommandBase();
            VolumeButMouseEnterCommand.DoExecute = new Action<object>((o) =>
            {
                Model.IsOpen = true;
            });
            VolumeButMouseEnterCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //移入音量按钮,打开音量调节
            VolumeMouseLeaveCommand = new CommandBase();
            VolumeMouseLeaveCommand.DoExecute = new Action<object>((o) =>
            {
                Model.IsOpen = false;
            });
            VolumeMouseLeaveCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });

            //点击静音或者还原
            VolumeButMouseDownCommand = new CommandBase();
            VolumeButMouseDownCommand.DoExecute = new Action<object>((o) =>
            {
                if (Model.VolumeValue == 0)
                {
                    Model.VolumeValue = 0.5;
                    Model.VolumeButContent = "\xe63c";
                }
                else
                {
                    Model.VolumeValue = 0;
                    Model.VolumeButContent = "\xe610";
                }
                PlayerViewModel.This.Model.VolumeValue = Model.VolumeValue;
                InitJsonData.jsonDataModel.Volume = Model.VolumeValue;
            });
            VolumeButMouseDownCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });



            //初始化音量
            double? volume = InitJsonData.jsonDataModel.Volume;
            double val = volume == null ? 0.5 : Convert.ToDouble(volume);
            if (volume == null)
            {
               Model.VolumeValue = 0.5;
            }
            if (Model.VolumeValue == 0)
            {
                Model.VolumeButContent = "\xe610";
            }
            Model.VolumeValue = val;
            PlayerViewModel.This.Model.VolumeValue = val;
        }
    }
}
