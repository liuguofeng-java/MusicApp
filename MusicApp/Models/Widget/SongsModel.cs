using MusicApp.Common;
using MusicApp.Models.Vo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MusicApp.Models.Widget
{
    public class SongsModel: NotifyBase
    {
        /// <summary>
        /// 数据源
        /// </summary>
        private List<Playlists> _playlists;
        public List<Playlists> Playlists
        {
            get { return _playlists; }
            set 
            {
                _playlists = value;
                DoNotify();
            }
        }

        /// <summary>
        /// 日历
        /// </summary>
        private string _calendar;
        public string Calendar
        {
            get {
                var dt = DateTime.Now;
                _calendar = dt.ToString("dd");
                return _calendar; }
            set
            {
                _calendar = value;
                DoNotify();
            }
        }
    }

    /// <summary>
    /// 首页歌单列表
    /// </summary>
    public class Playlists
    {
        /// <summary>
        /// 歌曲是否显示
        /// </summary>
        public Visibility SongVisibility { get; set; }
        /// <summary>
        /// 歌单是否显示
        /// </summary>
        public Visibility SongListVisibility { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 歌单id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string CoverImgUrl { get; set; }
        /// <summary>
        /// 详细介绍
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public List<string> Tags { get; set; }
        /// <summary>
        /// 播放量
        /// </summary>
        public int PlayCount { get; set; }
        /// <summary>
        /// 作者信息
        /// </summary>
        public Creator creator { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public List<SubscribersItem> subscribers { get; set; }
        /// <summary>
        /// 评论数
        /// </summary>
        public int CommentCount { get; set; }
    }
}
