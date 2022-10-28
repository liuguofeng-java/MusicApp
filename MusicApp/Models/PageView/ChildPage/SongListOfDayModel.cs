using MusicApp.Common;
using MusicApp.Models.Vo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MusicApp.Models.PageView.ChildPage
{
    public class SongListOfDayModel : NotifyBase
    {

        /// <summary>
        /// 数据元
        /// </summary>
        private List<ResultItem> _listSource;
        public List<ResultItem> ListSource
        {
            get
            { return this._listSource; }
            set
            {
                _listSource = value;
                DoNotify();
            }
        }
    }
}
