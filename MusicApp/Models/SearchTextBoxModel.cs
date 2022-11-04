using MusicApp.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace MusicApp.Models
{
    public class SearchTextBoxModel : NotifyBase
    {

        /// <summary>
        /// 输入框延时器
        /// </summary>
        private Timer _timer;

        public Timer Timer
        {
            get
            { return this._timer; }
            set
            { _timer = value; }
        }

        /// <summary>
        /// 输入框延时器
        /// </summary>
        private string _value;

        public string Value
        {
            get
            { return this._value; }
            set
            {
                _value = value;
                DoNotify();
            }
        }
    }
}
