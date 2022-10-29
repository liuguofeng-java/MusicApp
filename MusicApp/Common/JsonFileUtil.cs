using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace MusicApp.Common
{
    public static class JsonFileUtil  //配置INI文件读写API
    {
        private static string iniPath = @".\data\my.json";//json配置文件路径


        /// <summary>
        /// 将序列化的json字符串内容写入Json文件，并且保存
        /// </summary>
        /// <param name="jsonConents">Json内容</param>
        public static void WriteJsonFile(string jsonConents)
        {
            //不存在就创建
            if (!Directory.Exists(Path.GetDirectoryName(iniPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(iniPath));
                if (!File.Exists(iniPath))
                {
                    File.Create(iniPath).Close();
                }
            }
            using (FileStream fs = new FileStream(iniPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                fs.Seek(0, SeekOrigin.Begin);
                fs.SetLength(0);
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLine(jsonConents);
                }
            }
        }

        /// <summary>
        /// 获取到本地的Json文件并且解析返回对应的json字符串
        /// </summary>
        /// <returns>Json内容</returns>
        public static string GetJsonFile()
        {
            //不存在就创建
            if (!Directory.Exists(Path.GetDirectoryName(iniPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(iniPath));
                if (!File.Exists(iniPath))
                {
                    File.Create(iniPath).Close();
                }
            }
            string json = string.Empty;
            using (FileStream fs = new FileStream(iniPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.UTF8))
                {
                    json = sr.ReadToEnd().ToString();
                }
            }
            return json;
        }
    }
}
