using MusicApp.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MusicApp.Models
{
    /// <summary>
    /// 更新播放控件内容
    /// </summary>
    public class PlayerModel : NotifyBase
    {
        /// <summary>
        /// 当前播放器
        /// </summary>
        private MediaElement _mediaElement;
        public MediaElement MediaElement
        {
            get { return this._mediaElement; }
            set
            {
                _mediaElement = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 当前播放器音量
        /// </summary>
        private double _volumeValue;
        public double VolumeValue
        {
            get { return this._volumeValue; }
            set
            {
                _volumeValue = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 当前播放的音乐
        /// </summary>
        private SongModel _songPlayModel;
        public SongModel SongPlayModel
        {
            get { return this._songPlayModel; }
            set
            {
                _songPlayModel = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 更新进度条定时器
        /// </summary>
        private DispatcherTimer _timer;
        public DispatcherTimer Timer
        {
            get { return this._timer; }
            set
            {
                _timer = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 歌曲进度条进度
        /// </summary>
        private double _playProgress;
        public double PlayProgress
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
        public bool DisabledPlayProgress
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
        public double PlayProgressLength
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
        public string StartProgressTiem
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
        public string EndProgressTiem
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
        public string PlayButContent
        {
            get { return this._playButContent; }
            set
            {
                _playButContent = value;
                DoNotify();
            }
        }


        /// <summary>
        /// 播放模式
        /// </summary>
        private PlayModelStat _playModelStat;
        public PlayModelStat PlayModelStat
        {
            get { return this._playModelStat; }
            set
            {
                _playModelStat = value;
                DoNotify();
            }
        }
    }

    public class PlayModelStat
    {
        /// <summary>
        /// 模式名称
        /// </summary>
        public PlayModel Name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 提示
        /// </summary>
        public string Message { get; set; }
    }
}
