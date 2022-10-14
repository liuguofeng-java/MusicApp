using MusicApp.Control;
using MusicApp.Models;
using MusicApp.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp
{
    /// <summary>
    /// 储存用户控件实例
    /// </summary>
    public class ControlBean
    {
        private static ControlBean bean = new ControlBean();

        private ControlBean() {}
        public static ControlBean getInstance()
        {
            return bean;
        }

        /// <summary>
        /// 全局缓存数据
        /// </summary>
        private JsonDataModel _jsonData;
        public JsonDataModel jsonData
        {
            get { return this._jsonData; }
            set { _jsonData = value; }
        }

        /// <summary>
        /// 主窗体实例
        /// </summary>
        private MainWindow _mainWindow;
        public MainWindow mainWindow
        {
            get { return this._mainWindow; }
            set { _mainWindow = value; }
        }

        /// <summary>
        /// 侧边栏实例
        /// </summary>
        private SideNavBarWindowControl _sideNavBarWindowControl;
        public SideNavBarWindowControl sideNavBarWindowControl
        {
            get { return this._sideNavBarWindowControl; }
            set { _sideNavBarWindowControl = value; }
        }

        /// <summary>
        /// 底部播放器
        /// </summary>
        private PlayerControl _playerControl;
        public PlayerControl playerControl
        {
            get { return this._playerControl; }
            set { _playerControl = value; }
        }

        /// <summary>
        /// 底部歌曲详情
        /// </summary>
        private SongDetailControl _songDetailControl { get; set; }
        public SongDetailControl songDetailControl
        {
            get { return this._songDetailControl; }
            set { _songDetailControl = value; }
        }

        /// <summary>
        /// 搜索弹窗列表
        /// </summary>
        private SearchListControl _searchListControl;
        public SearchListControl searchListControl
        {
            get { return this._searchListControl; }
            set { _searchListControl = value; }
        }
    }
}
