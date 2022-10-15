using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.Models.Vo
{
    /// <summary>
    /// 待播放歌曲列表
    /// </summary>
    public class SongPlayListModel
    {
        /// <summary>
        /// 歌曲id
        /// </summary>
        public string songId { get; set; }

        /// <summary>
        /// 歌曲url
        /// </summary>
        public string songUrl { get; set; }

        /// <summary>
        /// 歌曲图片
        /// </summary>
        public string picUrl { get; set; }

        /// <summary>
        /// 本地下载后的路径
        /// </summary>
        public string localPicUrl { get; set; }

        /// <summary>
        /// 歌曲名称
        /// </summary>
        public string songName { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string author { get; set; }

        /// <summary>
        /// 音乐时长
        /// </summary>
        public int songTime { get; set; }

    }
}
