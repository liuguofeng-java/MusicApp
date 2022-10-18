using MusicApp.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.Models
{
    /// <summary>
    /// 更新播放控件内容
    /// </summary>
    public class PlayerModel : NotifyBase
    {
        /// <summary>
        /// 歌曲进度条进度
        /// </summary>
        private double _playProgress;
        public double playProgress
        {
            get { return this._playProgress; }
            set
            {
                _playProgress = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 是否禁用歌曲进度条
        /// </summary>
        private bool _disabledPlayProgress = false;
        public bool disabledPlayProgress
        {
            get { return this._disabledPlayProgress; }
            set
            {
                _disabledPlayProgress = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 歌曲进度条长度
        /// </summary>
        private double _playProgressLength = 1;
        public double playProgressLength
        {
            get { return this._playProgressLength; }
            set
            {
                _playProgressLength = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 歌曲播放进度
        /// </summary>
        private string _startProgressTiem = "00:00";
        public string startProgressTiem
        {
            get { return this._startProgressTiem; }
            set
            {
                _startProgressTiem = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 歌曲播放时长
        /// </summary>
        private string _endProgressTiem = "00:00";
        public string endProgressTiem
        {
            get { return this._endProgressTiem; }
            set
            {
                _endProgressTiem = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 播放按钮
        /// </summary>
        private string _playButContent = "\xe87c";
        public string playButContent
        {
            get { return this._playButContent; }
            set
            {
                _playButContent = value;
                DoNotify();
            }
        }
    }
}
