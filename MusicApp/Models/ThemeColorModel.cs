using MusicApp.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MusicApp.Models
{
    public class ThemeColorModel : NotifyBase
    {
        //主题选中颜色
        private ThemeSelect _themeSelect;
        public ThemeSelect ThemeSelect
        {
            get
            { return this._themeSelect; }
            set
            {
                _themeSelect = value;
                DoNotify();
            }
        }

        //程序资源
        private List<ResourceDictionary> _dictionaryList;
        public List<ResourceDictionary> DictionaryList
        {
            get
            { return this._dictionaryList; }
            set
            {
                _dictionaryList = value;
                DoNotify();
            }
        }

        //是否打开选择主题框
        private bool _isOpen;
        public bool IsOpen
        {
            get
            { return this._isOpen; }
            set
            {
                _isOpen = value;
                DoNotify();
            }
        }

    }

    public enum ThemeSelect
    {
        BlackColor = 1,
        DefaultColor = 2,
    }
}
