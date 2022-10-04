using MusicApp.Common;
using MusicApp.Control;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Controls;

namespace MusicApp.ViewModels
{
    public class SideNavBarWindowControlViewModel
    {
        public CommandBase MenusCommand { get; set; }

        public SideNavBarWindowControlViewModel()
        {
            MenusCommand = new CommandBase();
            MenusCommand.DoExecute = new Action<object>((o) =>
            {
                setModle(o.ToString());
            });
            MenusCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });
        }

        public static void setModle(string pageName)
        {
            Type type = Type.GetType("MusicApp.PageView." + pageName);
            ConstructorInfo constructorInfo = type.GetConstructor(Type.EmptyTypes);
            SideNavBarWindowControl.SetModel((UserControl)constructorInfo.Invoke(null));
        }


    }
}
