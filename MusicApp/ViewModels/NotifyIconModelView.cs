using MusicApp.Common;
using MusicApp.Themes;
using MusicApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MusicApp.ViewModels
{
    public class NotifyIconModelView : CommandBase
    {
        public CommandBase ClickCommand { get; set; }

        /// <summary>
        /// 点击右侧任务栏显示主窗体
        /// </summary>
        public NotifyIconModelView()
        {
            ClickCommand = new CommandBase();
            ClickCommand.DoExecute = new Action<object>((o) =>
            {
                NotifyIcon win = (NotifyIcon)o;
                win._parentWindow.Visibility = Visibility.Visible;
                win._parentWindow.Topmost = true;
                win._parentWindow.Topmost = false;
            });
            ClickCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });
        }
    }
}
