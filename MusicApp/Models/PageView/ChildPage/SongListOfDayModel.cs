using MusicApp.Common;
using MusicApp.Models.Vo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MusicApp.Models.PageView.ChildPage
{
    public class SongListOfDayPageModel : NotifyBase
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

        /// <summary>
        /// 列表选中的下标
        /// </summary>
        private int _selectdIndex;
        public int SelectdIndex
        {
            get
            { return this._selectdIndex; }
            set
            {
                _selectdIndex = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 日历
        /// </summary>
        private string _calendar;
        public string Calendar
        {
            get
            {
                var dt = DateTime.Now;
                _calendar = dt.ToString("dd");
                return _calendar;
            }
            set
            {
                _calendar = value;
                DoNotify();
            }
        }
    }
}
