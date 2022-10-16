using MusicApp.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.Models.Vo
{
    /// <summary>
    /// 待播放歌曲列表
    /// </summary>
    public class SongPlayListModel : NotifyBase
    {
        /// <summary>
        /// 歌曲id
        /// </summary>
        private string _songId;
        public string songId
        {
            get { return this._songId; }
            set
            {
                _songId = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 歌曲url
        /// </summary>
        private string _songUrl;
        public string songUrl
        {
            get { return this._songUrl; }
            set
            {
                _songUrl = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 本地下载后的mp3路径
        /// </summary>
        private string _localSongUrl;
        public string localSongUrl
        {
            get { return this._localSongUrl; }
            set
            {
                _localSongUrl = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 歌曲图片
        /// </summary>
        private string _picUrl;
        public string picUrl
        {
            get { return this._picUrl; }
            set
            {
                _picUrl = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 本地下载后歌曲名片的路径
        /// </summary>
        private string _localPicUrl;
        public string localPicUrl
        {
            get { return this._localPicUrl; }
            set
            {
                _localPicUrl = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 歌曲名称
        /// </summary>
        private string _songName;
        public string songName
        {
            get { return this._songName; }
            set
            {
                _songName = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 作者
        /// </summary>
        private string _author;
        public string author
        {
            get { return this._author; }
            set
            {
                _author = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 音乐时长
        /// </summary>
        private int _songTime;
        public int songTime
        {
            get { return this._songTime; }
            set
            {
                _songTime = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 格式化后音乐时长
        /// </summary>
        private string _formatSongTime;
        public string formatSongTime
        {
            get { return this._formatSongTime; }
            set
            {
                _formatSongTime = value;
                DoNotify();
            }
        }

    }
}
