using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 游客登录
/// </summary>
namespace MusicApp.Models.Vo
{
    //如果好用，请收藏地址，帮忙分享。
    public class AnonimousRedultModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long userId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long createTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cookie { get; set; }
    }

}
