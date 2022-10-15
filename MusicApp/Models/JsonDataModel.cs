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
        public double? volume
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
        private int? _themeColor;
        public int? themeColor
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
        private List<SongPlayListModel> _songPlayList;
        public List<SongPlayListModel> songPlayList
        {
            get 
            { return this._songPlayList; }
            set
            {
                _songPlayList = value;
                InitJsonData.WriteJsonFile();
            }
        }

    }

    /// <summary>
    /// 初始化json
    /// </summary>
    public static class InitJsonData
    {
        //程序初始化完成会变成true
        public static bool isInit = false;
        private static ControlBean controlBean = ControlBean.getInstance();
        /// <summary>
        /// 获取json数据
        /// </summary>
        public static void GetJsonFile()
        {
            var jsonStr = JsonFileUtil.GetJsonFile();
            if (string.IsNullOrEmpty(jsonStr))
            {
                jsonStr = "{}";
            }
            controlBean.jsonData = JsonConvert.DeserializeObject<JsonDataModel>(jsonStr);
        }

        /// <summary>
        /// 写入json数据
        /// </summary>
        public static void WriteJsonFile()
        {
            if (isInit)
            {
                string jsonStr = JsonConvert.SerializeObject(controlBean.jsonData);
                JsonFileUtil.WriteJsonFile(jsonStr);
            }
        }
    }
}
