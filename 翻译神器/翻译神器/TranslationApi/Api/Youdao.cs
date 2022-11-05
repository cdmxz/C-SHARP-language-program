using Newtonsoft.Json;
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
    class Youdao : ITranslateApi
    {
        public string ApiChineseName { get; } = ApiNames.GetApiChineseNameByApiCode(ApiCode.YouDao);

        /// <summary>
        /// 有道图片翻译
        /// </summary>
        /// <param name="img">要翻译的图片</param>
        /// <param name="from">源语言</param>
        /// <param name="to">目标语言</param>
        /// <param name="src_text">原文</param>
        /// <param name="dst_text">译文</param>
        /// <exception cref="Exception"></exception>
        public void PictureTranslate(Image img, string from, string to, out string src_text, out string dst_text)
        {
            if (YoudaoKey.IsEmpty)
                throw new Exception("请设置有道图片翻译Key！");
            string url = "https://openapi.youdao.com/ocrtransapi";
            string q = TranslateUtil.ImageToBase64(img);
            string salt = new Random().Next(1048576).ToString();
            string appKey = YoudaoKey.AppKey;
            string appSecret = YoudaoKey.AppSecret;
            string sign = GetSign(appKey + q + salt + appSecret);
            string str = $"from={from}&to={to}&type=1&q={HttpUtility.UrlEncode(q)}&appKey={appKey}&salt={salt}&sign={sign}";
            string result = Post(url, str);
            var list = JsonConvert.DeserializeObject<PictureTranslationJson>(result);
            if (list is null || list.resRegions is null)
                throw new Exception("解析Json数据失败");
            if (list.errorCode != "0")
                throw new Exception("错误代码：" + list.errorCode);
            // 接收序列化后的数据
            StringBuilder dst = new StringBuilder();
            StringBuilder src = new StringBuilder();
            foreach (var item in list.resRegions)
            {
                src.Append(item.context + "\r\n");
                dst.Append(item.tranContent + "\r\n");
            }

            int sLen = src.ToString().LastIndexOf('\r');
            int dLen = dst.ToString().LastIndexOf('\r');
            src_text = src.ToString().Remove(sLen);
            dst_text = dst.ToString().Remove(dLen);
        }

        /// <summary>
        /// 有道图片翻译
        /// </summary>
        /// <param name="path">图片路径</param>
        /// <param name="from">源语言</param>
        /// <param name="to">目标语言</param>
        /// <param name="src_text">原文</param>
        /// <param name="dst_text">译文</param>
        /// <exception cref="ArgumentException"></exception>
        public void PictureTranslate(string path, string from, string to, out string src_text, out string dst_text)
        {
            if (!File.Exists(path))
                throw new ArgumentException("图片不存在！");
            Image img = Image.FromFile(path);
            PictureTranslate(img, from, to, out src_text, out dst_text);
        }


        /// <summary>
        /// 有道文本翻译
        /// </summary>
        /// <param name="srcText">原文</param>
        /// <param name="from">源语言</param>
        /// <param name="to">目标语言</param>
        /// <returns>译文</returns>
        /// <exception cref="Exception"></exception>

        public void TextTranslate(string srcText, string from, string to, out string src_text, out string dst_text)
        {
            src_text = srcText;
            dst_text = TextTranslate(srcText, from, to);
        }
        /// <summary>
        /// 有道文本翻译
        /// </summary>
        /// <param name="srcText">原文</param>
        /// <param name="from">源语言</param>
        /// <param name="to">目标语言</param>
        /// <returns>译文</returns>
        /// <exception cref="Exception"></exception>
        public static string TextTranslate(string srcText, string from, string to)
        {
            if (YoudaoKey.IsEmpty)
                throw new Exception("请设置有道翻译Key！");
            string url = "https://openapi.youdao.com/api";
            string q = srcText;
            string salt = new Random().Next(1048576).ToString();
            string appKey = YoudaoKey.AppKey;
            string appSecret = YoudaoKey.AppSecret;
            long millis = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            string curTime = (millis / 1000).ToString();
            string sign = GetSign(appKey + Truncate(q) + salt + curTime + appSecret);
            string str = $"q={HttpUtility.UrlEncode(q)}&from={from}&to={to}&appKey={appKey}&salt={salt}&sign={sign}&signType=v3&curtime={curTime}";
            string result = Post(url, str);
            TranslationJson? list = JsonConvert.DeserializeObject<TranslationJson>(result);
            if (list is null || list.translation is null)
                throw new Exception("解析Json数据失败");
            if (list.errorCode != "0")
                throw new Exception("错误代码：" + list.errorCode);
            result = "";
            foreach (var item in list.translation)
                result += item;
            return result;
        }

        /// <summary>
        /// 测试Key是否有效
        /// </summary>
        /// <returns></returns>
        public void KeyTest()
        {
            PictureTranslate(Properties.Resources.KeyTest, LangDict["中文"], LangDict["英文"], out string s, out string d);
        }

        private static string Truncate(string q)
        {
            if (string.IsNullOrEmpty(q))
                return string.Empty;
            int len = q.Length;
            return len <= 20 ? q : q.Substring(0, 10) + len + q.Substring(len - 10, 10);
        }

        private static string GetSign(string str)
        {
            return TranslateUtil.GetUpperCaseMd5(str);
        }

        private static string Post(string url, string parameter)
        {
            using HttpClient httpClient = new();
            using StringContent content = new(parameter, Encoding.UTF8, "application/x-www-form-urlencoded");
            using HttpResponseMessage response = httpClient.PostAsync(url, content).Result;
            using StreamReader reader = new(response.Content.ReadAsStream(), Encoding.UTF8);
            return reader.ReadToEnd();
        }

        #region 源语言和目标语言字典
        public Dictionary<string, string> LangDict { get; } = new() {
            {"中文", "zh-CHS"},
            {"英文", "en"},
            {"中文繁体", "zh-CHT"},
            {"日文", "ja"},
            {"韩文", "ko"},
            {"法文", "fr"},
            {"西班牙文", "es"},
            {"葡萄牙文", "pt"},
            {"意大利文", "it"},
            {"俄文", "ru"},
            {"越南文", "vi"},
            {"德文", "de"},
            {"阿拉伯文", "ar"},
            {"印尼文", "id"},
            {"自动识别", "auto"}
        };
        #endregion

        public string GetDefaultLangCode() => LangDict["自动识别"];

        public string[] GetSupportedTargetLanguageNames()
        {
            return LangDict.Keys.Where((key) => !key.Equals("自动识别")).ToArray();
        }

        public string[] GetSupportedLanguageCode()
        {
            return LangDict.Values.ToArray();
        }

        #region 图片翻译Json类
        /// <summary>
        /// 图片翻译Json类
        /// </summary>
        class PictureTranslationJson
        {
            /// <summary>
            /// 错误码
            /// </summary>
            public string? errorCode { get; set; }
            /// <summary>
            /// 译文列表
            /// </summary>
            public List<ResRegions>? resRegions { get; set; }
            public class ResRegions
            {
                /// <summary>
                /// 原文
                /// </summary>
                public string? tranContent { get; set; }
                /// <summary>
                /// 译文
                /// </summary>
                public string? context { get; set; }
            }
        }
        #endregion

        #region 文本翻译Json类
        /// <summary>
        /// 文本翻译Json类
        /// </summary>
        class TranslationJson
        {
            /// <summary>
            /// 错误码
            /// </summary>
            public string? errorCode { get; set; }
            /// <summary>
            /// 译文数组
            /// </summary>
            public string[]? translation { get; set; }
        }
        #endregion
    }
}
