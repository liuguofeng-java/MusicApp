using MusicApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MusicApp.Models.SongModel;

namespace MusicApp.Models
{
    public class NotifyIconModel : NotifyBase
    {
        /// <summary>
        /// 是否禁用歌曲信息
        /// </summary>
        private bool _isIsEnabledSongInfo = true;
        public bool IsIsEnabledSongInfo
        {
            get { return this._isIsEnabledSongInfo; }
            set
            {
                _isIsEnabledSongInfo = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 是否禁用歌曲信息
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
    }
}
