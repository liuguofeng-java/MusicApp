using MusicApp.Common;
using MusicApp.Control;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.ViewModels
{
    /// <summary>
    /// 点击'皮肤'弹出选择主题弹框的ViewModel
    /// </summary>
    public class ThemeColorControlViewModel : CommandBase
    {
        public CommandBase SelectButCommand { get; set; }
        public ThemeColorControlViewModel(ThemeColorControl control)
        {
            SelectButCommand = new CommandBase();
            SelectButCommand.DoExecute = new Action<object>((o) =>
            {
                control.GetDictionary((string)o);
            });
            SelectButCommand.DoCanExecute = new Func<object, bool>((o) => { return true; });
        }
    }
}
