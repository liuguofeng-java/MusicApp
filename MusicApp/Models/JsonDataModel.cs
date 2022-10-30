using MusicApp.Common;
using MusicApp.Models.Vo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.Models
{
    /// <summary>
    /// 全局json配置model
    /// </summary>
    public class JsonDataModel
    {
        /// <summary>
        /// 音量缓存
        /// </summary>
        private double? _volume;
        public double? Volume
        {
            get { return this._volume; }
            set 
            { 
                _volume = value;
                InitJsonData.WriteJsonFile();
            }
        }

        /// <summary>
        /// 主题缓存
        /// </summary>
        private string _themeColor;
        public string ThemeColor
        {
            get { return this._themeColor; }
            set
            {
                _themeColor = value;
                InitJsonData.WriteJsonFile();
            }
        }

        /// <summary>
        /// 待播放歌曲列表
        /// </summary>
        private List<SongModel> _songPlayList;
        public List<SongModel> SongPlayList
        {
            get 
            { return this._songPlayList; }
            set
            {
                _songPlayList = value;
                InitJsonData.WriteJsonFile();
            }
        }

        /// <summary>
        /// 播放模式
        /// </summary>
        private PlayModel _playModelStat = PlayModel.ListLoop;
        public PlayModel PlayModelStat
        {
            get
            { return this._playModelStat; }
            set
            {
                _playModelStat = value;
                InitJsonData.WriteJsonFile();
            }
        }


        /// <summary>
        /// 当前播放音乐实例
        /// </summary>
        private SongModel _songPlay;
        public SongModel SongPlay
        {
            get
            { return this._songPlay; }
            set
            {
                _songPlay = value;
                InitJsonData.WriteJsonFile();
            }
        }

        /// <summary>
        /// 当前播放音乐实例
        /// </summary>
        private AnonimousRedultModel _anonCookie;
        public AnonimousRedultModel AnonCookie
        {
            get
            { return this._anonCookie; }
            set
            {
                _anonCookie = value;
                InitJsonData.WriteJsonFile();
            }
        }

    }

    /// <summary>
    /// 播放模式
    /// </summary>
    public enum PlayModel
    {
        ListLoop,//列表循环
        SimpleLoop,//单曲循环
        RandomPlay,//随机循环
        OrderPlay,//顺序循环
    }

    /// <summary>
    /// 初始化json
    /// </summary>
    public static class InitJsonData
    {
        //程序初始化完成会变成true
        public static bool isInit = false;
        public static JsonDataModel jsonDataModel = new JsonDataModel();
        /// <summary>
        /// 获取json数据
        /// </summary>
        public static void GetJsonFile()
        {
            var jsonStr = JsonFileUtil.GetJsonFile();
            if (string.IsNullOrEmpty(jsonStr))
            {
                jsonStr = JsonConvert.SerializeObject(jsonDataModel);
            }
            jsonDataModel = JsonConvert.DeserializeObject<JsonDataModel>(jsonStr);
        }

        /// <summary>
        /// 写入json数据
        /// </summary>
        public static void WriteJsonFile()
        {
            if (isInit)
            {
                string jsonStr = JsonConvert.SerializeObject(jsonDataModel);
                JsonFileUtil.WriteJsonFile(jsonStr);
            }
        }
    }
}
