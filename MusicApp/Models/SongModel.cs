using MusicApp.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.Models
{
    public class SongModel : NotifyBase
    {
        /// <summary>
        /// 歌曲id
        /// </summary>
        private string _songId;
        public string SongId
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
        public string SongUrl
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
        public string LocalSongUrl
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
        public string PicUrl
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
        public string LocalPicUrl
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
        public string SongName
        {
            get { return this._songName; }
            set
            {
                _songName = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 歌词
        /// </summary>
        private string _lyric;
        public string Lyric
        {
            get { return this._lyric; }
            set
            {
                _lyric = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 作者
        /// </summary>
        private string _author;
        public string Author
        {
            get { return this._author; }
            set
            {
                _author = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 作者
        /// </summary>
        private string _album;
        public string Album
        {
            get { return this._album; }
            set
            {
                _album = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 音乐时长
        /// </summary>
        private int _songTime;
        public int SongTime
        {
            get { return this._songTime; }
            set
            {
                _songTime = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 真实音乐时长
        /// </summary>
        private int _realSongTime;
        public int RealSongTime
        {
            get { return this._realSongTime; }
            set
            {
                _realSongTime = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 格式化后音乐时长
        /// </summary>
        private string _formatSongTime;
        public string FormatSongTime
        {
            get { return this._formatSongTime; }
            set
            {
                _formatSongTime = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 列表颜色
        /// </summary>
        private bool _isSelected = false;
        public bool IsSelected
        {
            get { return this._isSelected; }
            set
            {
                _isSelected = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 是否加载中 
        /// </summary>
        private bool _isLoading;
        public bool IsLoading
        {
            get { return this._isLoading; }
            set
            {
                _isLoading = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 播放状态 
        /// </summary>
        private PlayStatus _status = PlayStatus.StartPlay;
        public PlayStatus Status
        {
            get { return this._status; }
            set
            {
                _status = value;
                DoNotify();
            }
        }


        /// <summary>
        /// 播放状态
        /// </summary>
        public enum PlayStatus
        {
            StartPlay,//正在播放
            StopPlay,//停止播放
            ClosePlay //彻底关闭
        }
    }



}
