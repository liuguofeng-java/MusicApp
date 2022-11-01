using MusicApp.PageView.ChildPage;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.PageView
{
    public class PageManager
    {
        public static FoundMusicPage foundMusicPage = new FoundMusicPage();
        public static PodcastPage podcastPage = new PodcastPage();
        public static VideoPage videoPage = new VideoPage();
        public static FocusOnPage focusOnPage = new FocusOnPage();
        public static FMPage fMPage = new FMPage();
        public static LikeMusicPage likeMusicPage = new LikeMusicPage();
        public static LocalAndDownloadPage localAndDownloadPage = new LocalAndDownloadPage();
        public static RecentPlayPage recentPlayPage = new RecentPlayPage();

        //每日推荐
        public static SongListOfDayPage SongListOfDayPage = new SongListOfDayPage();

    }
}
