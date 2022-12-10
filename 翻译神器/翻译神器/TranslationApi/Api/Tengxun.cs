using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TencentCloud.Common;
using TencentCloud.Common.Profile;
using TencentCloud.Tmt.V20180321;
using TencentCloud.Tmt.V20180321.Models;
using 翻译神器.TranslationApi.Common;

namespace 翻译神器.TranslationApi.Api
{
    public class Tengxun : ITranslateApi
    {

        public string ApiChineseName { get; } = ApiNames.GetApiChineseNameByApiCode(ApiCode.TexngXun);

        private readonly TmtClient client;

        public Tengxun()
        {
            Credential cred = new();
            HttpProfile httpProfile = new()
            {
                Timeout = 6,
                Endpoint = "tmt.tencentcloudapi.com"
            };
            ClientProfile clientProfile = new()
            {
                HttpProfile = httpProfile
            };
            client = new(cred, "ap-guangzhou", clientProfile);
        }

        /// <summary>
        /// 测试Key是否有效
        /// </summary>
        /// <returns></returns>
        public void KeyTest()
        {
            PictureTranslate(Properties.Resources.KeyTest, LangDict["简体中文"], LangDict["英语"], out _, out _);
        }

        /// <summary>
        /// 腾讯图片翻译
        /// </summary>
        /// <param name="img">图片</param>
        /// <param name="from">源语言</param>
        /// <param name="to">目标语言</param>
        public void PictureTranslate(Image img, string from, string to, out string src_text, out string dst_text)
        {
            if (TengxunKey.IsEmpty)
                throw new Exception("请设置腾讯图片翻译Key！");
            // 发送请求
            ImageTranslateRequest req = new()
            {
                SessionUuid = DateTime.Now.ToString("G"),
                Scene = DateTime.Now.ToString("G"),
                Data = TranslateUtil.ImageToBase64(img),
                Source = from,
                Target = to,
                ProjectId = 0
            };
            // 在每次使用之前初始化密钥，
            // 而不是在构造函数初始化
            // 避免在调用构造函数时密钥还未被初始化，导致错误
            var cred = new Credential()
            {
                SecretId = TengxunKey.SecretId,
                SecretKey = TengxunKey.SecretKey
            };
            client.Credential = cred;
            ImageTranslateResponse resp = client.ImageTranslateSync(req);
            string result = AbstractModel.ToJsonString(resp);
            var list = JsonConvert.DeserializeObject<PictureTranslationJson>(result);
            if (list is null)
                throw new Exception("解析Json数据失败");
            if(list.ImageRecord is null || list.ImageRecord.Value is null)
                    throw new Exception("识别结果为空");
            if (list.ImageRecord.Value.Count == 0)
                throw new Exception("未识别到文字！");
            // 接收序列化后的数据
            StringBuilder dst = new();
            StringBuilder src = new();
            foreach (var item in list.ImageRecord.Value)
            {
                src.Append(item.SourceText + "\r\n");
                if (string.IsNullOrEmpty(item.TargetText))
                    continue;
                dst.Append(item.TargetText + "\r\n");
            }
            src_text = src.ToString();
            dst_text = dst.ToString();
            int sLen = src_text.LastIndexOf('\r');
            int dLen = dst_text.LastIndexOf('\r');
            src_text = sLen != -1 ? src_text.Remove(sLen) : src_text;
            dst_text = dLen != -1 ? dst_text.Remove(dLen) : dst_text;
        }

        /// <summary>
        /// 腾讯文本翻译
        /// </summary>
        /// <param name="srcText">原文</param>
        /// <param name="from">源语言</param>
        /// <param name="to">目标语言</param>
        /// <returns></returns>
        public void TextTranslate(string srcText, string from, string to, out string src_text, out string dst_text)
        {
            TextTranslateRequest request = new()
            {
                SourceText = srcText,
                Source = from,
                Target = to,
                ProjectId = 0
            };
            var cred = new Credential()
            {
                SecretId = TengxunKey.SecretId,
                SecretKey = TengxunKey.SecretKey
            };
            client.Credential = cred;
            TextTranslateResponse response = client.TextTranslateSync(request);
            string result = AbstractModel.ToJsonString(response);
            var list = JsonConvert.DeserializeObject<TranslationJson>(result);
            if (list is null)
                throw new Exception("解析Json数据失败");
            if(list.TargetText is null)
                throw new Exception("识别结果为空");
            src_text = srcText;
            dst_text = list.TargetText;
        }


        #region 源语言和目标语言字典
        public Dictionary<string, string> LangDict { get; } = new()
        {
            {"自动识别", "auto"},
            {"简体中文", "zh"},
            {"英语", "en"},
            {"繁体中文", "zh-TW"},
            {"日语", "ja"},
            {"韩语", "ko"},
            {"俄语", "ru"},
            {"法语", "fr"},
            {"德语", "de"},
            {"意大利语", "it"},
            {"西班牙语", "es"},
            {"葡萄牙语", "pt"},
            {"马来西亚语", "ms"},
            {"泰语", "th"},
            {"越南语", "vi"},

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


        #region 图片翻译Json
        public class PictureTranslationJson
        {
            public Image_Record? ImageRecord { get; set; }

            public class Image_Record
            {
                public List<ValueItem>? Value { get; set; }
                public class ValueItem
                {
                    /// <summary>
                    /// 源文本
                    /// </summary>
                    public string? SourceText { get; set; }
                    /// <summary>
                    /// 翻译后的文本
                    /// </summary>
                    public string? TargetText { get; set; }
                }
            }
        }
        #endregion

        #region 翻译Json
        public class TranslationJson
        {
            /// <summary>
            ///  翻译后的文本
            /// </summary>
            public string? TargetText { get; set; }
        }
        #endregion
    }
}
