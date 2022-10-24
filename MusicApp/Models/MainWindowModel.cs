using MusicApp.Common;
using MusicApp.PageView;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MusicApp.Models
{
    public class MainWindowModel : NotifyBase
    {
        /// <summary>
        /// 当前页面
        /// </summary>
        private FrameworkElement _page;

        public FrameworkElement Page
        {
            get
            { return this._page; }
            set
            {
                _page = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 侧边菜单选中状态
        /// </summary>
        private MenusChecked _menusChecked;

        public MenusChecked MenusChecked
        {
            get
            { return this._menusChecked; }
            set
            {
                _menusChecked = value;
                switch (_menusChecked)
                {
                    case MenusChecked.FoundMusicPage:
                        Page = PageManager.foundMusicPage;
                        break;
                    case MenusChecked.PodcastPage:
                        Page = PageManager.podcastPage;
                        break;
                    case MenusChecked.VideoPage:
                        Page = PageManager.videoPage;
                        break;
                    case MenusChecked.FocusOnPage:
                        Page = PageManager.focusOnPage;
                        break;
                    case MenusChecked.FMPage:
                        Page = PageManager.fMPage;
                        break;
                    case MenusChecked.LikeMusicPage:
                        Page = PageManager.likeMusicPage;
                        break;
                    case MenusChecked.LocalAndDownloadPage:
                        Page = PageManager.localAndDownloadPage;
                        break;
                    case MenusChecked.RecentPlayPage:
                        Page = PageManager.recentPlayPage;
                        break;


                }
                DoNotify();
            }
        }

    }

    public enum MenusChecked
    {
        FoundMusicPage,
        PodcastPage,
        VideoPage,
        FocusOnPage,
        LivePage,
        FMPage,
        LikeMusicPage,
        LocalAndDownloadPage,
        RecentPlayPage
    }
}
