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
    }

    /// <summary>
    /// 歌词每句
    /// </summary>
    public class LyricItme : NotifyBase
    {
        /// <summary>
        /// 歌词
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// 时间进度
        /// </summary>
        public double time { get; set; }

        /// <summary>
        /// 格式化时间进度
        /// </summary>
        public string formatTime { get; set; }


        /// <summary>
        /// 是否第一个
        /// </summary>
        public bool isFirstOne { get; set; }

        /// <summary>
        /// 第一个的外边界
        /// </summary>
        public string firstOneMargin { get; set; }



        /// <summary>
        /// 是否最后一个
        /// </summary>
        public bool lastOne { get; set; }

        /// <summary>
        /// 最后一个的外边界
        /// </summary>
        public string lastMargin { get; set; }

        /// <summary>
        /// 是否是焦点
        /// </summary>
        private bool _isFocus;
        public bool isFocus
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
