using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 搜索结果
/// </summary>
namespace MusicApp.Models.Vo
{
    public class AlbumsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 天外来物
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Artist artist { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long publishTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long copyrightId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long picId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long mark { get; set; }
    }

    public class ArtistsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 天下
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string picUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> @alias { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long albumSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long picId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fansGroup { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string img1v1Url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long img1v1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string trans { get; set; }
    }

    public class Artist
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string picUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> @alias { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long albumSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long picId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fansGroup { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string img1v1Url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long img1v1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string trans { get; set; }
    }

    public class Album
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 天外来物
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Artist artist { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long publishTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long size { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long copyrightId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long picId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long mark { get; set; }
    }

    public class SearchSongsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 天外来物
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ArtistsItem> artists { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Album album { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long duration { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long copyrightId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> @alias { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long rtype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long ftype { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long mvid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long fee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long mark { get; set; }
    }

    /*public class PlaylistsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 英文歌的天花板
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string coverImgUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string creator { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string subscribed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long trackCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long userId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long playCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long bookCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public long specialType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string officialTags { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string action { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string actionType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string recommendText { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string score { get; set; }
        /// <summary>
        /// 史上最全英文歌，没有之一！！！
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string highQuality { get; set; }
    }*/

    public class Result
    {
        /// <summary>
        /// 
        /// </summary>
        public List<AlbumsItem> albums { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ArtistsItem> artists { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SearchSongsItem> songs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<PlaylistsItem> playlists { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> order { get; set; }
    }

    public class SearchDataResultModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Result result { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int code { get; set; }
    }

}
