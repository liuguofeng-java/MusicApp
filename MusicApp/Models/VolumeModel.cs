using MusicApp.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.Models
{
    public class VolumeModel : NotifyBase
    {
        /// <summary>
        /// 音量按钮图标
        /// </summary>
        private string _volumeButContent = "\xe63c";
        public string VolumeButContent
        {
            get { return this._volumeButContent; }
            set
            {
                _volumeButContent = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 音量值
        /// </summary>
        private double _volumeValue = 0.5;
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
        /// 是否弹出音量调节
        /// </summary>
        private bool _isOpen;
        public bool IsOpen
        {
            get { return this._isOpen; }
            set
            {
                _isOpen = value;
                DoNotify();
            }
        }
    }
}
