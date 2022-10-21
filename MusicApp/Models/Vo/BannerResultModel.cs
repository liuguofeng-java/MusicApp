using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

/// <summary>
/// 接收轮播图实体
/// </summary>
namespace MusicApp.Models.Vo
{
    public class BannerResultModel
    {
        /// <summary>
        /// 
        /// </summary>
        public List<BannersItem> banners { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
    }

    public class BannersItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string imageUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Int64 targetId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string adid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int targetType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string titleColor { get; set; }
        /// <summary>
        /// 新碟首发
        /// </summary>
        public string typeTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string exclusive { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string monitorImpress { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string monitorClick { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string monitorType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string monitorImpressList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string monitorClickList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string monitorBlackList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string extMonitor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string extMonitorInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string adSource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string adLocation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string adDispatchJson { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string encodeId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string program { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string @event { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string video { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object song { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string scm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bannerBizType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BitmapImage image { get; set; }
    }
}
