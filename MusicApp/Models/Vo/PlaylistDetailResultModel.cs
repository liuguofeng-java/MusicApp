using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Models.Vo
{
    /// <summary>
    /// 歌单详情
    /// </summary>
    public class PlaylistDetailResultModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string relatedVideos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Playlist playlist { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string urls { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<PrivilegesItem> privileges { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sharedPrivilege { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object resEntrance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fromUsers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int fromUserCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string songFromUsers { get; set; }
    }

   
    public class TracksItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long pst { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long t { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ArItem> ar { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> alia { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long pop { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long st { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long fee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long v { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string crbt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cf { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Al al { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int dt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object h { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public M m { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public L l { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object sq { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object hr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string a { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rtUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ftype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> rtUrls { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long djId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int copyright { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long s_id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long mark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long originCoverType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object originSongSimpleData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tagPicList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string resourceState { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string songJumpInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string entertainmentTags { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int single { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object noCopyrightRcmd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int rtype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rurl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int mst { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int cp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int mv { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long publishTime { get; set; }
    }

    public class TrackIdsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int v { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int t { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long at { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string alg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long uid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rcmdReason { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string f { get; set; }
    }

    public class Playlist
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 盘点曾风靡全球的经典歌曲
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long coverImgId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string coverImgUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string coverImgId_str { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int adType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long userId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long createTime { get; set; }
        public string createDataTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string opRecommend { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string highQuality { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string newImported { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long updateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int trackCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int specialType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long privacy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long trackUpdateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string commentThreadId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int playCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long trackNumberUpdateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int subscribedCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int cloudTrackCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ordered { get; set; }
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
        public string updateFrequency { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long backgroundCoverId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string backgroundCoverUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long titleImage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string titleImageUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string englishTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string officialPlaylistType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string copied { get; set; }
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
        public Creator creator { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<TracksItem> tracks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string videoIds { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string videos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<TrackIdsItem> trackIds { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bannedTrackIds { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int shareCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int commentCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string remixVideo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object sharedUsers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object historySharedUsers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gradeStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string score { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object algTags { get; set; }
    }



   

}
