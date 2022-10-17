using System;
using System.Collections.Generic;
using System.Text;


/// <summary>
/// 热搜榜数据实体
/// </summary>
namespace MusicApp.Models.Vo
{
    public class RankingItem
    {
        /// <summary>
        /// 排名
        /// </summary>
        public int num { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        public string color { get; set; }
        /// <summary>
        /// 林俊杰
        /// </summary>
        public string searchWord { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int score { get; set; }
        /// <summary>
        /// 当之无愧的行走CD机！
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int source { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int iconType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string iconUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string alg { get; set; }
    }

    public class RankingResultModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<RankingItem> data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string message { get; set; }
    }


}
