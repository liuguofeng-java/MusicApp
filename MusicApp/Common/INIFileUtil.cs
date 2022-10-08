using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace MusicApp.Common
{
    public static class INIFileUtil  //配置INI文件读写API
    {
        public static string iniPath = @".\data\my.ini";//ini配置文件路径
        [DllImport("kernel32", CharSet = CharSet.Unicode)] // 写入配置文件的接口
        private static extern long WritePrivateProfileString(
        string section, string key, string val, string filePath);
        [DllImport("kernel32", CharSet = CharSet.Unicode)] // 读取配置文件的接口
        private static extern int GetPrivateProfileString(
        string section, string key, string def,
        StringBuilder retVal, int size, string filePath);
        /// <summary>
        /// 写入ini文件 配置数据： 节点，键值，数据，INI路径
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键值</param>
        /// <param name="value">写入数据</param>
        public static void writeValue(
        string section, string key, string value)
        {
            //创建INI文件
            if (!Directory.Exists(Path.GetDirectoryName(iniPath)))//先判断文件所在目录是否存在，不存在则创建目录。
            {
                Directory.CreateDirectory(Path.GetDirectoryName(iniPath));
                if (!File.Exists(iniPath))//判断文件是否存在，不存在则创建文件
                {
                    File.Create(iniPath);
                }
            }
            WritePrivateProfileString(section, key, value, iniPath);
        }
        /// <summary>
        /// 读取INI配置文件内容
        /// </summary>
        /// <param name="section">节点名称</param>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static string readValue(
        string section, string key)
        {
            StringBuilder sb = new StringBuilder(255);
            //创建INI文件
            if (!Directory.Exists(Path.GetDirectoryName(iniPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(iniPath));
                if (!File.Exists(iniPath))
                {
                    File.Create(iniPath);
                }
            }
            GetPrivateProfileString(section, key, "", sb, 255, iniPath);//读取键值数据到StringBuilder
            return sb.ToString().Trim();
        }
    }

}
