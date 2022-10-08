using MusicApp.Common;
using MusicApp.Control;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.ViewModels
{
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
