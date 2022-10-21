using System;
using System.Collections.Generic;
using System.Text;


/// <summary>
/// 歌词返回
/// </summary>
namespace MusicApp.Models.Vo
{
    public class LyricsResultModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string sgc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sfy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string qfy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Lrc lrc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Klyric klyric { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Tlyric tlyric { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Romalrc romalrc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
    }
    public class Lrc
    {
        /// <summary>
        /// 
        /// </summary>
        public int version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lyric { get; set; }
    }

    public class Klyric
    {
        /// <summary>
        /// 
        /// </summary>
        public int version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lyric { get; set; }
    }

    public class Tlyric
    {
        /// <summary>
        /// 
        /// </summary>
        public int version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lyric { get; set; }
    }

    public class Romalrc
    {
        /// <summary>
        /// 
        /// </summary>
        public int version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string lyric { get; set; }
    }

}
