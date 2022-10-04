using MusicApp.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MusicApp.Models
{
    public class MainWindowModel: NotifyBase
    {
        /// <summary>
        /// 当前页面
        /// </summary>
        private FrameworkElement _page;

        public FrameworkElement Page
        {
            get
            { return this._page;}
            set
            {
                _page = value;
                DoNotify();
            }
        }
    }
}
