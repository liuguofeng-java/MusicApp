using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MusicApp.Common
{
    public class StringUtil
    {

        /// <summary>
        /// 识别urlStr是否是网络路径
        /// </summary>
        /// <param name="urlStr"></param>
        /// <returns></returns>
        public static bool UrlDiscern(string urlStr)
        {
            if (Regex.IsMatch(urlStr, @"((http|ftp|https)://)(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(:[0-9]{1,4})*(/[a-zA-Z0-9\&%_\./-~-]*)?"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 时间戳转string
        /// </summary>
        /// <param name="time">毫秒数</param>
        /// <returns></returns>
        public static string FormatTimeoutToString(int time)
        {
            //计算歌曲时间
            int second = time / 1000;//总秒数
            int minute = second / 60;//分钟数
            int remSecond = second - (minute * 60);//剩余秒数
            return (minute.ToString().Length == 1 ? "0" + minute.ToString() : minute.ToString())
                                      + ":" +
                                     (remSecond.ToString().Length == 1 ? "0" + remSecond.ToString() : remSecond.ToString());//总时长
        }
    }
}
