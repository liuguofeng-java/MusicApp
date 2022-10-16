using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MusicApp.Common
{
    public class HttpUtil
    {
        public const string serveUrl = "http://120.46.163.203:3000";
        /// <summary>
        /// http 下载
        /// </summary>
        /// <param name="url">下载路径</param>
        /// <param name="timeout">超时时间</param>
        public static string HttpDownload(String url, String path, String fileName, int timeout = 10000)
        {
            try
            {
                HttpWebRequest request = null;

                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    request = WebRequest.Create(url) as HttpWebRequest;
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    request.ProtocolVersion = HttpVersion.Version11;

                    // 这里设置了协议类型。
                    //ServicePointManager.SecurityProtocol = (SecurityProtocolType)(SecurityProtocolTypes.Ssl3 | SecurityProtocolTypes.Tls | SecurityProtocolTypes.Tls11 | SecurityProtocolTypes.Tls12);
                    request.KeepAlive = false;
                    ServicePointManager.CheckCertificateRevocationList = true;
                    ServicePointManager.DefaultConnectionLimit = 100;
                    ServicePointManager.Expect100Continue = false;
                }
                else
                {
                    request = (HttpWebRequest)WebRequest.Create(url);
                }

                request.Method = "GET";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36";
                request.Timeout = timeout;

                //获取网页响应结果
                HttpWebResponse response;
                Stream responseStream;

                response = request.GetResponse() as HttpWebResponse;
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        break;

                    default:
                        response.Close();
                        throw new Exception("向服务器发送请求失败");
                }

                String des = response.GetResponseHeader("Content-Disposition");

                switch (response.ContentType)
                {
                    case "application/force-download":
                        //强制下载类型- Content-Disposition  存在文件名        attachment;filename=test0514.bin
                        //提取文件名
                        {
                            String[] strs = des.Split(';');
                            strs = strs[1].Split('=');

                        }
                        break;

                    case "application/octet-stream":
                        //返回二进制流
                        {
                            String[] strs = des.Split('/');
                        }
                        break;

                    default:
                        //返回二进制流
                        //可以从URL 提取文件名
                        {
                            String[] strs = des.Split('/');
                        }

                        break;
                }

                responseStream = response.GetResponseStream();

                byte[] bArr = new byte[1024];
                FileStream fs = new FileStream(path + "\\" + fileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);

                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    fs.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }

                fs.Close();
                responseStream.Close();
                response.Close();
                return path + "\\" + fileName;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 发送请求，数据为json
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static String HttpRequset(String url, CookieContainer cookieContainer = null, String data = null, String method = "GET", int timeout = 3000)
        {
            try
            {
                HttpWebRequest request = null;

                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    request = WebRequest.Create(url) as HttpWebRequest;
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    request.ProtocolVersion = HttpVersion.Version11;

                    // 这里设置了协议类型。
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)(SecurityProtocolTypes.Ssl3 | SecurityProtocolTypes.Tls | SecurityProtocolTypes.Tls11 | SecurityProtocolTypes.Tls12);
                    request.KeepAlive = false;
                    ServicePointManager.CheckCertificateRevocationList = true;
                    ServicePointManager.DefaultConnectionLimit = 100;
                    ServicePointManager.Expect100Continue = false;
                }
                else
                {
                    request = (HttpWebRequest)WebRequest.Create(url);
                }

                request.Method = method;
                request.ContentType = "application/json";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
                request.AllowAutoRedirect = true;
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.198 Safari/537.36";
                request.Timeout = timeout;

                if (cookieContainer != null)
                    request.CookieContainer = cookieContainer;

                if (data != null && data.Equals("") == false)
                {
                    byte[] stream = Encoding.UTF8.GetBytes(data);

                    Stream newStream = request.GetRequestStream();
                    newStream.Write(stream, 0, stream.Length);
                    newStream.Close();
                }

                //获取网页响应结果
                HttpWebResponse response;
                Stream responseStream;
                StreamReader reader;
                string srcString;

                response = request.GetResponse() as HttpWebResponse;
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        break;

                    default:
                        response.Close();
                        throw new Exception("向服务器发送请求失败");
                }

                responseStream = response.GetResponseStream();
                reader = new System.IO.StreamReader(responseStream, Encoding.GetEncoding("UTF-8"));

                srcString = reader.ReadToEnd();

                reader.Close();
                response.Close();

                return srcString;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// HTTP 上传文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        public static String HttpUpload(string url, string[] files)
        {
            // 设置参数
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();

                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";

                string boundary = DateTime.Now.Ticks.ToString("X");                                                             // 随机分隔线
                request.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;

                const string filePartHeaderTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" +
                                    "Content-Type: application/octet-stream\r\n\r\n";

                byte[] beginBoundaryBytes = Encoding.UTF8.GetBytes("--" + boundary + "\r\n");                                   // 边界符开始。【☆】右侧必须要有 \r\n 。
                byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");                               // 边界符结束。【☆】两侧必须要有 --\r\n 。
                byte[] newLineBytes = Encoding.UTF8.GetBytes("\r\n");                                                           //换一行

                MemoryStream memoryStream = new MemoryStream();
                List<string> lstFiles = new List<string>();

                foreach (string fileFullName in files)
                {
                    if (File.Exists(fileFullName))
                    {
                        lstFiles.Add(fileFullName);
                    }
                }

                int ik = 0;
                foreach (var fileFullName in lstFiles)
                {
                    FileInfo fileInfo = new FileInfo(fileFullName);
                    string fileName = fileInfo.Name;

                    string fileHeaderItem = string.Format(filePartHeaderTemplate, "multipartFile", fileName);
                    byte[] fileHeaderItemBytes = Encoding.UTF8.GetBytes(fileHeaderItem);

                    if (ik > 0)
                    {
                        // 第一笔及第一笔之后的数据项之间要增加一个换行 
                        memoryStream.Write(newLineBytes, 0, newLineBytes.Length);
                    }

                    memoryStream.Write(beginBoundaryBytes, 0, beginBoundaryBytes.Length);                                   // 2.1 写入FormData项的开始边界符
                    memoryStream.Write(fileHeaderItemBytes, 0, fileHeaderItemBytes.Length);                                 // 2.2 将文件头写入FormData项中

                    memoryStream.Write(newLineBytes, 0, newLineBytes.Length);

                    int bytesRead;
                    byte[] buffer = new byte[1024];

                    FileStream fileStream = new FileStream(fileFullName, FileMode.Open, FileAccess.Read);
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        memoryStream.Write(buffer, 0, bytesRead);                                                           // 2.3 将文件流写入FormData项中
                    }

                    ik++;
                }

                memoryStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);    // 2.4 写入FormData的结束边界符

                request.ContentLength = memoryStream.Length;
                Stream requestStream = request.GetRequestStream();
                memoryStream.Position = 0;

                byte[] tempBuffer = new byte[memoryStream.Length];
                memoryStream.Read(tempBuffer, 0, tempBuffer.Length);
                memoryStream.Close();

                requestStream.Write(tempBuffer, 0, tempBuffer.Length);  // 将内存流中的字节写入 httpWebRequest 的请求流中
                requestStream.Close();

                //发送请求并获取相应回应数据
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream instream = response.GetResponseStream();
                StreamReader sr = new StreamReader(instream, Encoding.UTF8);
                //返回结果网页（html）代码
                string content = sr.ReadToEnd();

                sr.Close();
                response.Close();

                return content;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// HTTP 上传文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        public static String HttpUpload(string url, String fileName, byte[] fileData, int startPos, int length)
        {
            // 设置参数
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();

                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";

                string boundary = DateTime.Now.Ticks.ToString("X");                                                             // 随机分隔线
                request.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;

                const string filePartHeaderTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" +
                                    "Content-Type: application/octet-stream\r\n\r\n";

                byte[] beginBoundaryBytes = Encoding.UTF8.GetBytes("--" + boundary + "\r\n");                                   // 边界符开始。【☆】右侧必须要有 \r\n 。
                byte[] endBoundaryBytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");                               // 边界符结束。【☆】两侧必须要有 --\r\n 。
                byte[] newLineBytes = Encoding.UTF8.GetBytes("\r\n");                                                           //换一行

                MemoryStream memoryStream = new MemoryStream();
                List<string> lstFiles = new List<string>();

                string fileHeaderItem = string.Format(filePartHeaderTemplate, "multipartFile", fileName);
                byte[] fileHeaderItemBytes = Encoding.UTF8.GetBytes(fileHeaderItem);

                memoryStream.Write(beginBoundaryBytes, 0, beginBoundaryBytes.Length);                                   // 2.1 写入FormData项的开始边界符
                memoryStream.Write(fileHeaderItemBytes, 0, fileHeaderItemBytes.Length);                                 // 2.2 将文件头写入FormData项中

                memoryStream.Write(newLineBytes, 0, newLineBytes.Length);
                memoryStream.Write(fileData, startPos, length);                                                           // 2.3 将文件流写入FormData项中

                memoryStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);    // 2.4 写入FormData的结束边界符

                request.ContentLength = memoryStream.Length;
                Stream requestStream = request.GetRequestStream();
                memoryStream.Position = 0;

                byte[] tempBuffer = new byte[memoryStream.Length];
                memoryStream.Read(tempBuffer, 0, tempBuffer.Length);
                memoryStream.Close();

                requestStream.Write(tempBuffer, 0, tempBuffer.Length);  // 将内存流中的字节写入 httpWebRequest 的请求流中
                requestStream.Close();

                //发送请求并获取相应回应数据
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream instream = response.GetResponseStream();
                StreamReader sr = new StreamReader(instream, Encoding.UTF8);

                //返回结果网页（html）代码
                string content = sr.ReadToEnd();

                sr.Close();
                response.Close();

                return content;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }
    }

    public enum SecurityProtocolTypes
    {
        Ssl3 = 0x30,
        Tls = 0xc0,
        Tls11 = 0x300,
        Tls12 = 0xc00
    }
}
