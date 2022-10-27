using MusicApp.Models;
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

                var anonCookie = InitJsonData.jsonDataModel.AnonCookie;
                if (anonCookie != null)
                {
                    //request.Headers.Add("cookie", anonCookie.cookie);
                    //request.Headers.Add("cookie", "MUSIC_R_T=0; Max-Age=2147483647; Expires=Mon, 13 Nov 2090 06:34:33 GMT; Path=/weapi/feedback;;MUSIC_R_T=0; Max-Age=2147483647; Expires=Mon, 13 Nov 2090 06:34:33 GMT; Path=/neapi/feedback;;MUSIC_A_T=1666345976223; Max-Age=2147483647; Expires=Mon, 13 Nov 2090 06:34:33 GMT; Path=/openapi/clientlog;;MUSIC_A_T=1666345976223; Max-Age=2147483647; Expires=Mon, 13 Nov 2090 06:34:33 GMT; Path=/wapi/feedback;;MUSIC_A_T=1666345976223; Max-Age=2147483647; Expires=Mon, 13 Nov 2090 06:34:33 GMT; Path=/api/clientlog;;MUSIC_R_T=0; Max-Age=2147483647; Expires=Mon, 13 Nov 2090 06:34:33 GMT; Path=/weapi/clientlog;;MUSIC_SNS=; Max-Age=0; Expires=Wed, 26 Oct 2022 03:20:26 GMT; Path=/;MUSIC_A_T=1666345976223; Max-Age=2147483647; Expires=Mon, 13 Nov 2090 06:34:33 GMT; Path=/neapi/clientlog;;MUSIC_R_T=0; Max-Age=2147483647; Expires=Mon, 13 Nov 2090 06:34:33 GMT; Path=/eapi/clientlog;;MUSIC_R_T=0; Max-Age=2147483647; Expires=Mon, 13 Nov 2090 06:34:33 GMT; Path=/api/clientlog;;MUSIC_R_T=0; Max-Age=2147483647; Expires=Mon, 13 Nov 2090 06:34:33 GMT; Path=/wapi/clientlog;;MUSIC_A_T=1666345976223; Max-Age=2147483647; Expires=Mon, 13 Nov 2090 06:34:33 GMT; Path=/api/feedback;;MUSIC_A_T=1666345976223; Max-Age=2147483647; Expires=Mon, 13 Nov 2090 06:34:33 GMT; Path=/weapi/feedback;;MUSIC_A=bf8bfeabb1aa84f9c8c3906c04a04fb864322804c83f5d607e91a04eae463c9436bd1a17ec353cf7f7396e98af8ee20db78dc09c1560e9e0993166e004087dd3e6e5e6737d8336ac209e07b04bd0eab172b4ad082b73226b47d5567ad0d91258807e650dd04abd3fb8130b7ae43fcc5b; Max-Age=1296000; Expires=Thu, 10 Nov 2022 03:20:26 GMT; Path=/;;MUSIC_A_T=1666345976223; Max-Age=2147483647; Expires=Mon, 13 Nov 2090 06:34:33 GMT; Path=/eapi/feedback;;MUSIC_R_T=0; Max-Age=2147483647; Expires=Mon, 13 Nov 2090 06:34:33 GMT; Path=/neapi/clientlog;;MUSIC_A_T=1666345976223; Max-Age=2147483647; Expires=Mon, 13 Nov 2090 06:34:33 GMT; Path=/eapi/clientlog;;MUSIC_R_T=0; Max-Age=2147483647; Expires=Mon, 13 Nov 2090 06:34:33 GMT; Path=/eapi/feedback;;MUSIC_A_T=1666345976223; Max-Age=2147483647; Expires=Mon, 13 Nov 2090 06:34:33 GMT; Path=/weapi/clientlog;;MUSIC_A_T=1666345976223; Max-Age=2147483647; Expires=Mon, 13 Nov 2090 06:34:33 GMT; Path=/wapi/clientlog;;__csrf=541b14240c96cb226cd10abe2df6296d; Max-Age=1296010; Expires=Thu, 10 Nov 2022 03:20:36 GMT; Path=/;;MUSIC_R_T=0; Max-Age=2147483647; Expires=Mon, 13 Nov 2090 06:34:33 GMT; Path=/api/feedback;;MUSIC_A_T=1666345976223; Max-Age=2147483647; Expires=Mon, 13 Nov 2090 06:34:33 GMT; Path=/neapi/feedback;;MUSIC_R_T=0; Max-Age=2147483647; Expires=Mon, 13 Nov 2090 06:34:33 GMT; Path=/openapi/clientlog;;MUSIC_R_T=0; Max-Age=2147483647; Expires=Mon, 13 Nov 2090 06:34:33 GMT; Path=/wapi/feedback;");
                    request.Headers.Add("cookie", "_ntes_nnid=ba54e5859380a7cdc85306c5337d6268,1661408221966; _ntes_nuid=ba54e5859380a7cdc85306c5337d6268; NMTID=00O8F4LDDaBJoGFFEQRgEuz7Gdl2HEAAAGC06WkWg; WEVNSM=1.0.0; WNMCID=tahbgu.1661408222514.01.0; WM_TID=murq69aa5pxFAVQVUQKVCDDQzEwWEoHd; __snaker__id=KaVnwG147z0ri6Kx; _9755xjdesxxd_=32; YD00000558929251%3AWM_TID=Kp%2BcNV3rejZBVRRBAFbEW0DhisRHjWs1; WM_NI=e%2FCZoT%2BRXageKNhlaiYJYjQAL%2Fx4ljVG4Ar84Dg7O2nfDJK5uaBI%2BSG%2FxNYAg7HxNoND%2Brhjt%2F4uQkOP4Jsm1YwGMBtpSCk4eYAybycelG2aZhEfxCGeXrmdePTkBIOoejc%3D; WM_NIKE=9ca17ae2e6ffcda170e2e6eea3bc70fb9c9bd2c254f39e8ab3d44b839b9eadc4478799bb8cd8458899879bc52af0fea7c3b92aa7f18dbbee60e98e9882e66f8992a88de4639b96978bd44b90f1b8d5ef629699fca2f134f89ca9d8ce3aaeba8cabca3fb7bf8eaaaa5985f1b8afdc4ba5e98899cd658e9ce593eb62b8f1fc96d4618d90fcabf267f387b9a8d04ab8b09da4ea40a7878f9ae173a1bba4a8b16b9ab88b87fb4985bb9893f070a7909c8cc1438db1ab8be237e2a3; YD00000558929251%3AWM_NI=bSBD5IvGOU29PT%2FMDfoYV1%2F7Q8L6P24gpQtv%2BpR2jnsHEQU5KSlfpxvOtwEG%2F8%2FugyYRr4rVifhnxc3AmuQVst5%2FO2h%2BEV8Jly1U2JS%2FnireHeJcsGh%2BginEVQ%2BQo%2FBmOU4%3D; YD00000558929251%3AWM_NIKE=9ca17ae2e6ffcda170e2e6eeb1fb3db7e8ff87f752b3b48ba2c54a978b9ab0c1708789bf8dc54198befbd2e22af0fea7c3b92aa28d8ea5b843b69981b6b133948ce1aec76ead879ad2ae3d93e884d7d33af8b9c0b2f77ee9b09ed3e966b1988bb2d83f898da487f374b691bc98c84dede8e196e6739b8ebed5f15eabbaa88ffb69f19fa396d45eb1acfbd0ce3aacf58bb7bb42b0a7ffb6b14291f1a28af16f8cbb8684f24dacf5a98aed4f968fe5b2ee2582b381b6b737e2a3; JSESSIONID-WYYY=s6lI4eJbIywOlpp0C8kp55jaAJuVeDpnZwviB6dKDDI6PY9Uw62YyHKxwn71KFgPUKr1TwoXmUFo8ouRBbRfRgd5k%2BR%2F%5CerxdnV7vnybVTByMit%5CWzmDYjn3bxlJ%5C%2FOqarat20HMYziHJQthIOwu6%2Br%2Fm8cB07kGFzbNyigYIzqU37xa%3A1666756043884; _iuqxldmzr_=33; gdxidpyhxdE=iUSQtPHXsqpCuyt1Tt1gY17gD3LIvidIqQaeBkvy5poaiV7CI5Z8Wa6v02WGl%5CeGGQ8PPX1ME6ZuXGkvWrNjN6YC8f%5C1vuAV0kuEMeAJ%2B2mM5YI6C3sYH%2BLSnU6gj6ZX%2Fzy1CkTTcYkZ7NkUB52N%2B%5C024TGjQ2A%2BEOXG8BR%2FXBXwwxla%3A1666755984925; __csrf=0ef07ff0e8a2de49cb2e6594c0aaf594; MUSIC_U=7e4e4c1b2f5df1a0bdd5cff5373a97fcf7396e98af8ee20d6e75af6161c3da3c993166e004087dd33077f9d762a168c5acc0ee5a1574c8be17bed7bc7cb7a4cba788ce457fe91694d4dbf082a8813684; ntes_kaola_ad=1");
                }

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
