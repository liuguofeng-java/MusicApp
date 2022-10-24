using MusicApp.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MusicApp.Models
{
    public class SongInfoModel : NotifyBase
    {


        /// <summary>
        /// 是否显示控件
        /// </summary>
        private Visibility _songInfoVisibility = Visibility.Hidden;

        public Visibility SongInfoVisibility
        {
            get
            { return this._songInfoVisibility; }
            set
            { 
                _songInfoVisibility = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 是否显示歌曲头像
        /// </summary>
        private Visibility _songPicVisibility = Visibility.Hidden;

        public Visibility SongPicVisibility
        {
            get
            { return this._songPicVisibility; }
            set
            {
                _songPicVisibility = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 当前播放歌曲信息
        /// </summary>
        private SongModel _songModel;

        public SongModel SongModel
        {
            get
            { return this._songModel; }
            set
            {
                _songModel = value;
                DoNotify();
            }
        }
    }
}
