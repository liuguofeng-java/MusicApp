using MusicApp.Common;
using MusicApp.Models.Vo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MusicApp.Models
{
    public class SearchListModel : NotifyBase
    {

        /// <summary>
        /// 搜索弹出框排行榜
        /// </summary>
        private List<RankingItem> _rankingList = new List<RankingItem>();

        public List<RankingItem> RankingList
        {
            get
            { return this._rankingList; }
            set
            {
                _rankingList = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 搜索弹出框单曲
        /// </summary>
        private List<SearchSongsItem> _singleList;

        public List<SearchSongsItem> SingleList
        {
            get
            { return this._singleList; }
            set
            {
                _singleList = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 搜索弹出框歌手
        /// </summary>
        private List<ArtistsItem> _artistsList;

        public List<ArtistsItem> ArtistsList
        {
            get
            { return this._artistsList; }
            set
            {
                _artistsList = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 搜索弹出框专辑
        /// </summary>
        private List<AlbumsItem> _albumList;

        public List<AlbumsItem> AlbumList
        {
            get
            { return this._albumList; }
            set
            {
                _albumList = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 搜索弹出框歌单
        /// </summary>
        private List<PlaylistsItem> _songsList;

        public List<PlaylistsItem> SongsList
        {
            get
            { return this._songsList; }
            set
            {
                _songsList = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 是否显示弹出框
        /// </summary>
        private Visibility _gridVisibility;
        public Visibility GridVisibility
        {
            get
            { return this._gridVisibility; }
            set
            {
                _gridVisibility = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 是否显示排行榜
        /// </summary>
        private Visibility _rankingVisibility = Visibility.Visible;
        public Visibility RankingVisibility
        {
            get
            { return this._rankingVisibility; }
            set
            {
                _rankingVisibility = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 是否显示搜索结果
        /// </summary>
        private Visibility _searchVisibility = Visibility.Collapsed;
        public Visibility SearchVisibility
        {
            get
            { return this._searchVisibility; }
            set
            {
                _searchVisibility = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 搜索弹出框单曲
        /// </summary>
        private int _selectdIndex = -1;

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
    }
}
