using MusicApp.Common;
using MusicApp.Models;
using System;
using System.Reflection;
using System.Windows.Controls;

namespace MusicApp.ViewModels
{
    /// <summary>
    /// 侧边栏
    /// </summary>
    public class SideNavBarWindowControlViewModel
    {
        public CommandBase MenusCommand { get; set; }

        public SideNavBarWindowControlViewModel()
        {
            //选中页面
            MenusCommand = new CommandBase();
            MenusCommand.DoExecute = new Action<object>((o) =>
            {
                setModle(o.ToString());
            });
            MenusCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });
        }

        /// <summary>
        /// 选中页面
        /// </summary>
        /// <param name="pageName"></param>
        public static void setModle(string pageName = "FoundMusicPage")
        {
            Type type = Type.GetType("MusicApp.PageView." + pageName);
            ConstructorInfo constructorInfo = type.GetConstructor(Type.EmptyTypes);

            
            ((MainWindowModel)ControlBean.getInstance().mainWindow.DataContext).Page = (UserControl)constructorInfo.Invoke(null);
        }


    }
}
