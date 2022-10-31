using MusicApp.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MusicApp.Models
{
    /// <summary>
    /// 待播放歌曲列表
    /// </summary>
    public class SongPlayListModel : NotifyBase
    {
        /// <summary>
        /// 歌曲列表
        /// </summary>
        private List<SongModel> _songLists;
        public List<SongModel> SongLists
        {
            get {
                if (_songLists == null) _songLists = new List<SongModel>();
                return this._songLists; 
            }
            set
            {
                _songLists = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 是否隐藏列表
        /// </summary>
        private Visibility _playListVisibility;
        public Visibility PlayListVisibility
        {
            get { return this._playListVisibility; }
            set
            {
                _playListVisibility = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 歌曲列表
        /// </summary>
        private string _songPlayCount;
        public string SongPlayCount
        {
            get { return this._songPlayCount; }
            set
            {
                _songPlayCount = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 当前列表选中的数据
        /// </summary>
        private int _selectdIndex;
        public int SelectdIndex
        {
            get { return this._selectdIndex; }
            set
            {
                _selectdIndex = value;
                DoNotify();
            }
        }
    }
}
