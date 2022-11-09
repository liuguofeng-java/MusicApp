using MusicApp.Common;
using MusicApp.Models.Vo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MusicApp.Models.PageView.ChildPage
{
    public class SongListDetailsModel : NotifyBase
    {

        /// <summary>
        /// 歌单id
        /// </summary>
        private string _songsId;
        public string SongsId
        {
            get { return this._songsId; }
            set
            {
                _songsId = value;
                DoNotify();
            }
        }


        /// <summary>
        /// 介绍文本高度
        /// </summary>
        private double _noteTextHeight = 20;
        public double NoteTextHeight
        {
            get { return this._noteTextHeight; }
            set
            {
                _noteTextHeight = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 介绍文本点击展开按钮
        /// </summary>
        private string _noteButText = "\xe65c";
        public string NoteButText
        {
            get { return this._noteButText; }
            set
            {
                _noteButText = value;
                DoNotify();
            }
        }

        

        /// <summary>
        /// 歌单信息 名称/简介等
        /// </summary>
        private Playlist _playlist;
        public Playlist Playlist
        {
            get { return this._playlist; }
            set
            {
                _playlist = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 歌曲列表
        /// </summary>
        private List<SongsItem> _songList;
        public List<SongsItem> SongList
        {
            get { return this._songList; }
            set
            {
                _songList = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 列表选中下标
        /// </summary>
        private int _selectdIndex;
        public int SelectdIndex
        {
            get { return this._selectdIndex; }
            set
            {
                _selectdIndex = value;
                DoNotify();
            }
        }


        /// <summary>
        /// 歌曲列表的加载是否显示
        /// </summary>
        private Visibility _tableLoadVisibility = Visibility.Visible;
        public Visibility TableLoadVisibility
        {
            get { return this._tableLoadVisibility; }
            set
            {
                _tableLoadVisibility = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 歌曲列表是否显示error信息
        /// </summary>
        private Visibility _tableErrorVisibility = Visibility.Collapsed;
        public Visibility TableErrorVisibility
        {
            get { return this._tableErrorVisibility; }
            set
            {
                _tableErrorVisibility = value;
                DoNotify();
            }
        }


        /// <summary>
        /// 基础信息加载是否显示
        /// </summary>
        private Visibility _baseLoadVisibility = Visibility.Visible;
        public Visibility BaseLoadVisibility
        {
            get { return this._baseLoadVisibility; }
            set
            {
                _baseLoadVisibility = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 基础信息是否显示error信息
        /// </summary>
        private Visibility _baseErrorVisibility = Visibility.Collapsed;
        public Visibility BaseErrorVisibility
        {
            get { return this._baseErrorVisibility; }
            set
            {
                _baseErrorVisibility = value;
                DoNotify();
            }
        }


        /// <summary>
        /// 排序头信息
        /// </summary>
        private List<string> _headInfos = new List<string>
        {
            "\xe632默认排序",
            "\xe632默认排序",
            "\xe632默认排序",
            "\xe632",
        };
        public List<string> HeadInfos
        {
            get { return this._headInfos; }
            set
            {
                _headInfos = value;
                DoNotify();
            }
        }
        /// <summary>
        /// 临时歌曲列表排序用
        /// </summary>
        private List<SongsItem> _temSongList;
        public List<SongsItem> TemSongList
        {
            get { return this._temSongList; }
            set
            {
                _temSongList = value;
                DoNotify();
            }
        }


    }

}
