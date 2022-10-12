using MusicApp.Common;
using MusicApp.Themes;
using MusicApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MusicApp.ViewModels
{
    /// <summary>
    /// 程序托盘菜单的ModelView 
    /// </summary>
    public class NotifyIconModelView : CommandBase
    {
        public CommandBase ClickCommand { get; set; }

        private MainWindow mainWindow;
        /// <summary>
        /// 点击右侧任务栏显示主窗体
        /// </summary>
        public NotifyIconModelView()
        {
            mainWindow = ControlBean.getInstance().mainWindow;

            ClickCommand = new CommandBase();
            ClickCommand.DoExecute = new Action<object>((o) =>
            {
                mainWindow.Visibility = Visibility.Visible;
                mainWindow.Topmost = true;
                mainWindow.Topmost = false;
            });
            ClickCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });
        }
    }
}
