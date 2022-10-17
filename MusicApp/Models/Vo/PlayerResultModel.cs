using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 接收歌曲url
/// </summary>
namespace MusicApp.Models.Vo
{
    public class FreeTrialPrivilege
    {
        /// <summary>
        /// 
        /// </summary>
        public string resConsumable { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userConsumable { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string listenType { get; set; }
    }

    public class FreeTimeTrialPrivilege
    {
        /// <summary>
        /// 
        /// </summary>
        public string resConsumable { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string userConsumable { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int remainTime { get; set; }
    }

    public class SongDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int br { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string md5 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int expi { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double gain { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int fee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string uf { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int payed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int flag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string canExtend { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public FreeTrialInfo freeTrialInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string level { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string encodeType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public FreeTrialPrivilege freeTrialPrivilege { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public FreeTimeTrialPrivilege freeTimeTrialPrivilege { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int urlSource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int rightSource { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string podcastCtrp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string effectTypes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int time { get; set; }
    }

    public class FreeTrialInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int start { get; set; }
        public int end { get; set; }
         
    }

    public class PlayerControlModel
    {
        /// <summary>
        /// 
        /// </summary>
        public List<SongDataItem> data { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
    }

}
