using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Models.Vo
{
    /// <summary>
    /// 歌单歌曲
    /// </summary>
    public class PlaylistSongsResultModel
    {
        /// <summary>
        /// 
        /// </summary>
        public List<SongsItem> songs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<PrivilegesItem> privileges { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
    }
}
