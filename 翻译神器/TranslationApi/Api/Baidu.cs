using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using 翻译神器.TranslationApi.Common;

namespace 翻译神器.TranslationApi.Api
{
    public class Baidu : ITranslateApi
    {
        // Api中文名称
        public string ApiChineseName { get; } = ApiNames.GetApiChineseNameByApiCode(ApiCode.Baidu);


        /// <summary>
        /// 百度图片翻译
        /// </summary>
        /// <param name="img">要翻译的图片</param>
        /// <param name="from">源语言</param>
        /// <param name="to">目标语言</param>
        /// <param name="src_text">原文</param>
        /// <param name="dst_text">译文</param>
        /// <exception cref="Exception"></exception>
        public void PictureTranslate(Image img, string from, string to, out string src_text, out string dst_text)
        {
            if (BaiduKey.IsEmpty)
                throw new Exception("请设置百度文字识别和翻译Key！");
            string url = "https://fanyi-api.baidu.com/api/trans/sdk/picture";
            string appid = BaiduKey.AppId;
            string salt = new Random().Next(1048576).ToString();
            string mac = "mac";
            string cuid = "APICUI";
            string password = BaiduKey.Password;
            string imgMd5 = TranslateUtil.GetLowerCaseMd5(img);
            string sign = TranslateUtil.GetLowerCaseMd5(appid + imgMd5 + salt + cuid + mac + password);
            // 发送请求
            RestRequest request = new(url, Method.Post);
            request.AddFile("image", TranslateUtil.ImageToBytes(img), "1.png");
            request.AddParameter("from", from);
            request.AddParameter("to", to);
            request.AddParameter("appid", appid);
            request.AddParameter("salt", salt);
            request.AddParameter("mac", mac);
            request.AddParameter("cuid", cuid);
            request.AddParameter("version", "3");
            request.AddParameter("sign", sign);
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddParameter("mediaType", "image/png");
            PictureTranslateJson? list = SendRequest<PictureTranslateJson>(request);
            if (list is null)
                throw new Exception("解析Json数据失败");
            if (list.error_code != 0) // 如果调用Api出现错误
                throw new Exception("调用百度图片翻译失败" + "\n原因：" + list.error_msg);
            if (list.data is null || list.data.content is null)
                throw new Exception("识别结果为空");
            // 接收序列化后的数据
            StringBuilder dst = new();
            StringBuilder src = new();
            foreach (var item in list.data.content)
            {
                src.Append(item.src + "\r\n");
                dst.Append(item.dst + "\r\n");
            }
            int sLen = src.ToString().LastIndexOf('\r');
            int dLen = dst.ToString().LastIndexOf('\r');
            src_text = src.ToString().Remove(sLen);
            dst_text = dst.ToString().Remove(dLen);
        }

        /// <summary>
        /// 百度文字翻译
        /// </summary>
        /// <param name="srcText">要翻译的文本</param>
        /// <param name="from">源语言</param>
        /// <param name="to">目标语言</param>
        /// <param name="src_text">原文</param>
        /// <param name="dst_text">译文</param>
        /// <exception cref="Exception"></exception>
        public void TextTranslate(string srcText, string from, string to, out string src_text, out string dst_text)
        {
            if (BaiduKey.IsEmpty)
                throw new Exception("请设置百度文字识别和翻译Key！");
            string url = "https://fanyi-api.baidu.com/api/trans/vip/translate";
            string appid = BaiduKey.AppId;
            string salt = new Random().Next(1048576).ToString();
            string password = BaiduKey.Password;
            string sign = TranslateUtil.GetLowerCaseMd5(appid + srcText + salt + password);
            string str = string.Format($"q={HttpUtility.UrlEncode(srcText)}&from={from}&to={to}" +
                 $"&appid={BaiduKey.AppId}&salt={salt}&sign={sign}");
            // 发送请求   （不知道为什么用RestSharp库发送请求会返回签名无效）
            var content = new StringContent(str, Encoding.UTF8, "application/x-www-form-urlencoded");
            string result = SendRequest(url, content);
            var list = JsonConvert.DeserializeObject<TranslateJson>(result);  // 将json数据转化为对象类型并赋值给list
            if (list is null)
                throw new Exception("解析Json数据失败");
            if (list.Error_code != null) // 如果调用Api出现错误
                throw new Exception("调用百度翻译失败" + "\n原因：" + list.Error_msg);
            if (list.Trans_result is null)
                throw new Exception("识别结果为空");
            // 接收序列化后的数据
            StringBuilder dst = new();
            StringBuilder src = new();
            foreach (var item in list.Trans_result)
            {
                src.Append(item.Src + "\r\n");
                dst.Append(item.Dst + "\r\n");
            }
            int sLen = src.ToString().LastIndexOf('\r');
            int dLen = dst.ToString().LastIndexOf('\r');
            src_text = src.ToString().Remove(sLen);
            dst_text = dst.ToString().Remove(dLen);
        }

        /// <summary>
        /// HttpClient发送请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        private static string SendRequest(string url, StringContent content)
        {
            using HttpClient httpClient = new();
            using HttpRequestMessage requset = new(HttpMethod.Post, url);
            requset.Content = content;
            using Stream stream = httpClient.Send(requset).Content.ReadAsStream();
            using StreamReader sr = new(stream);
            return sr.ReadToEnd();
        }

        /// <summary>
        /// RestClient 发送请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        private static T? SendRequest<T>(RestRequest request)
        {
            using RestClient client = new();
            return client.Post<T>(request);
        }

        /// <summary>
        /// 测试key 失败抛出异常
        /// </summary>
        public void KeyTest()
        {
            PictureTranslate(Properties.Resources.KeyTest, LangDict["中文"], LangDict["英语"], out _, out _);
        }


        #region 源语言和目标语言字典
        public Dictionary<string, string> LangDict { get; } = new()
        {
            {"自动检测","auto"},
            {"中文","zh"},
            {"英语","en"},
            {"日语","jp"},
            {"韩语","kor"},
            {"法语","fra"},
            { "西班牙语","spa"},
            {"俄语","ru"},
            {"葡萄牙语","pt"},
            {"德语","de"},
            {"意大利语","it"},
            {"丹麦语","dan"},
            {"荷兰语","nl"},
            {"马来语","may"},
            {"瑞典语","swe"},
            { "印尼语","id"},
            {"波兰语","pl"},
            {"罗马尼亚语","rom"},
            {"土耳其语","tr"},
            {"希腊语","el"},
            {"匈牙利语","hu"},
        };

        public string GetDefaultLangCode() => LangDict["自动检测"];

        public string[] GetSupportedTargetLanguageNames()
        {
            return LangDict.Keys.Where((key) => !key.Equals("自动检测")).ToArray();
        }

        public string[] GetSupportedLanguageCode()
        {
            return LangDict.Values.ToArray();
        }

        #endregion

        #region 图片翻译Json
        class PictureTranslateJson
        {
            public int error_code { get; set; }
            public string? error_msg { get; set; }

            public Data? data { get; set; }

            public class Data
            {
                public class Content
                {
                    public string? src { get; set; }   // 分段翻译的原文
                    public string? dst { get; set; }   // 分段翻译译文
                }
                public List<Content>? content { get; set; }
                public string? sumSrc { get; set; } // 未分段翻译原文
                public string? sumDst { get; set; } // 未分段翻译译文
            }
        }
        #endregion

        #region 文字翻译Json
        class TranslationResult
        {
            public string? Src { get; set; }
            public string? Dst { get; set; }
        }

        class TranslateJson
        {
            public string? Error_code { get; set; }
            public string? Error_msg { get; set; }
            public TranslationResult[]? Trans_result { get; set; }
        }
        #endregion
    }
}
