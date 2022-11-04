using MusicApp.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.Models
{
    /// <summary>
    /// 歌曲详情
    /// </summary>
    public class SongDetailModel : NotifyBase
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
        /// 歌词信息
        /// </summary>
        private List<LyricItme> _lyricList;
        public List<LyricItme> LyricList
        {
            get { return this._lyricList; }
            set
            {
                _lyricList = value;
                DoNotify();
            }
        }
    }

    /// <summary>
    /// 歌词每句
    /// </summary>
    public class LyricItme : NotifyBase
    {
        /// <summary>
        /// 歌词
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 时间进度
        /// </summary>
        public double Time { get; set; }

        /// <summary>
        /// 格式化时间进度
        /// </summary>
        public string FormatTime { get; set; }


        /// <summary>
        /// 是否第一个
        /// </summary>
        public bool IsFirstOne { get; set; }

        /// <summary>
        /// 第一个的外边界
        /// </summary>
        public string FirstOneMargin { get; set; }

        /// <summary>
        /// 是否最后一个
        /// </summary>
        public bool LastOne { get; set; }

        /// <summary>
        /// 最后一个的外边界
        /// </summary>
        public string LastMargin { get; set; }

        /// <summary>
        /// 是否是焦点
        /// </summary>
        private bool _isFocus;
        public bool IsFocus
        {
            get { return this._isFocus; }
            set
            {
                _isFocus = value;
                DoNotify();
            }
        }

        
    }


}
