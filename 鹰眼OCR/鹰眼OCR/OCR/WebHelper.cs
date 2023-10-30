using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace 鹰眼OCR.OCR
{
    class WebHelper
    {
        /// <summary>
        /// 发送post请求
        /// </summary>
        /// <param name="host">要发送请求的链接</param>
        /// <param name="token">验证令牌（可为null）</param>
        /// <param name="param">参数</param>
        /// <returns>响应数据</returns>
        public static string Request(string host, string token, string param, string contentType = null)
        {
            host += string.IsNullOrEmpty(token) ? "" : token;
            var httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromMilliseconds(30000)
            };

            string mediaType = string.IsNullOrEmpty(contentType) ? "application/x-www-form-urlencoded" : contentType;
            var content = new StringContent(param, Encoding.UTF8, mediaType);
            var response = httpClient.PostAsync(host, content).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            return result;
        }

        /// <summary>
        ///  Image转Base64
        /// </summary>
        /// <param name="img"></param>
        /// <returns>转换后的base64数据</returns>
        public static string ImageToBase64(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Png);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        /// <summary>
        /// 文件转Base64
        /// </summary>
        /// <param name="fileName">路径</param>
        /// <returns>转换后的base64数据</returns>
        public static string FileToBase64(string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                byte[] arr = new byte[fs.Length];
                fs.Read(arr, 0, (int)fs.Length);
                string baser64 = Convert.ToBase64String(arr);
                return baser64;
            }
        }

        public static byte[] ImageToBytes(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }

        public static string GetMD5(string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;

            MD5 md5 = MD5.Create();
            // 字符串转换为字节数组并计算哈希数据  
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder builder = new StringBuilder(str.Length);
            // 循环遍历哈希数据的每一个字节并格式化为十六进制字符串  
            for (int i = 0; i < data.Length; i++)
                builder.Append(data[i].ToString("x2"));

            return builder.ToString().ToUpper();
        }

        /// <summary>
        /// 获取Unix时间戳
        /// </summary>
        /// <returns>Unix时间戳（单位：秒）</returns>
        public static string GetTimeSpan()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToString((long)ts.TotalMilliseconds / 1000);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        public static bool DownloadFile(string url, string fileName)
        {
            HttpClient httpClient = new HttpClient();
            FileStream fs = null;
            HttpResponseMessage response = null;
            try
            {
                fs = new FileStream(fileName, FileMode.Create);
                response = httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).Result;
                //var total = response.Content.Headers.ContentLength;
                var responseStream = response.Content.ReadAsStreamAsync().Result;
                byte[] buffer = new byte[4096];// 缓存
                int length;
                while ((length = responseStream.Read(buffer, 0, buffer.Length)) != 0)
                    fs.Write(buffer, 0, length);
                return true;
            }
            catch { return false; }
            finally
            {
                httpClient?.Dispose();
                fs?.Close();
                response?.Dispose();
            }
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static long GetFileSize(string file)
        {
            return new FileInfo(file).Length;
        }

        ///// <summary>
        ///// 下载文件
        ///// </summary>
        ///// <param name="url"></param>
        ///// <param name="savePath"></param>
        //public static bool DownloadFile(string url, string savePath)
        //{
        //    if (string.IsNullOrEmpty(url))
        //        return false;
        //    HttpWebRequest request;
        //    HttpWebResponse response = null; // 发送请求并获取响应
        //    Stream stream = null;
        //    FileStream fs = null;
        //    try
        //    {
        //        request = WebRequest.Create(url) as HttpWebRequest;
        //        response = request.GetResponse() as HttpWebResponse; // 发送请求并获取响应
        //        stream = response.GetResponseStream(); // 获取响应的数据流
        //        fs = new FileStream(savePath, FileMode.Create);  // 将数据流写入到文件
        //        byte[] bArr = new byte[4096];
        //        int len = default;
        //        while ((len = stream.Read(bArr, 0, bArr.Length)) > 0)
        //            fs.Write(bArr, 0, len);
        //        fs.Close();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        stream?.Dispose();
        //        response?.Dispose();
        //        fs?.Dispose();
        //    }
        //}
    }
}
