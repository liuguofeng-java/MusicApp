using MusicApp.Control;
using MusicApp.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp
{
    /// <summary>
    /// 储存用户控件实例
    /// </summary>
    public class ControlBean
    {
        private static ControlBean bean = new ControlBean();

        private ControlBean() {}
        public static ControlBean getInstance()
        {
            return bean;
        }

        /// <summary>
        /// 主窗体实例
        /// </summary>
        public MainWindow mainWindow { get; set; }

        /// <summary>
        /// 侧边栏实例
        /// </summary>
        public SideNavBarWindowControl sideNavBarWindowControl { get; set; }
    }
}
