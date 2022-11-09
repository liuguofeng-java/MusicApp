using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Models.Vo
{
    public class PlayListResultModel
    {
        /// <summary>
        /// 
        /// </summary>
        public List<PlaylistsItem> playlists { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int total { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string more { get; set; }
        /// <summary>
        /// 全部
        /// </summary>
        public string cat { get; set; }
    }

    public class AvatarDetail
    {
        /// <summary>
        /// 
        /// </summary>
        //public int userType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public int identityLevel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string identityIconUrl { get; set; }
    }

    public class Creator
    {
        /// <summary>
        /// 
        /// </summary>
        public string defaultAvatar { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int province { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int authStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string followed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string avatarUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int accountStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int gender { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int city { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long birthday { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long userId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int userType { get; set; }
        /// <summary>
        /// 网抑晕音约丷
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 音乐是人生的灵魂伴侣。
        /// </summary>
        public string signature { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string detailDescription { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long avatarImgId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long backgroundImgId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string backgroundUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int authority { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mutual { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> expertTags { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object experts { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int djStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int vipType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string remarkName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int authenticationTypes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object avatarDetail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string anchor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string avatarImgIdStr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string backgroundImgIdStr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string avatarImgId_str { get; set; }
    }

    public class SubscribersItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string defaultAvatar { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int province { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int authStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string followed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string avatarUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int accountStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int gender { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int city { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long birthday { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long userId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int userType { get; set; }
        /// <summary>
        /// 做你的小宝
        /// </summary>
        public string nickname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string signature { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string detailDescription { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long avatarImgId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long backgroundImgId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string backgroundUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int authority { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mutual { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> expertTags { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object experts { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int djStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int vipType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string remarkName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int authenticationTypes { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object avatarDetail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string anchor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string avatarImgIdStr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string backgroundImgIdStr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string avatarImgId_str { get; set; }
    }

    public class PlaylistsItem
    {
        /// <summary>
        /// 粤语丨 岁月流情，回味旧日的浪漫故事
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long trackNumberUpdateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
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
        public long updateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int subscribedCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int trackCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int cloudTrackCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string coverImgUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long coverImgId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> tags { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int playCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long trackUpdateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int specialType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int totalDuration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Creator creator { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tracks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SubscribersItem> subscribers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string subscribed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string commentThreadId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string newImported { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int adType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string highQuality { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int privacy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ordered { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string anonimous { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int coverStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string recommendInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int shareCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string coverImgId_str { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string alg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int commentCount { get; set; }
    }

}
