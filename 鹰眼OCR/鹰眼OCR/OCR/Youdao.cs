using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using 鹰眼OCR.OCR;
using Newtonsoft.Json;
using System.Web;

namespace 鹰眼OCR
{
    class Youdao
    {
        public static string Translate(string text, string from, string to, out string speakUrl)
        {
            speakUrl = null;
            string url = "https://openapi.youdao.com/api";
            string salt = DateTime.Now.Millisecond.ToString();
            string curTime = WebHelper.GetTimeSpan();
            string sign = ComputeHash(YoudaoKey.AppKey + Truncate(text) + salt + curTime + YoudaoKey.AppSecret);
            string str = string.Format($"q={HttpUtility.UrlEncode(text)}" +
                $"&from={GetTranLang(from)}&to={GetTranLang(to)}&appKey={YoudaoKey.AppKey}" +
                $"&salt={salt}&sign={sign}&signType=v3&curtime={curTime}");
            string result = WebHelper.Request(url, null, str);
            // 解析json数据
            YoudaoTranslate list = JsonConvert.DeserializeObject<YoudaoTranslate>(result);
            if (list.errorCode != "0")
            {
                ErrorTip(list.errorCode,
                    "https://ai.youdao.com/DOCSIRMA/html/%E8%87%AA%E7%84%B6%E8%AF%AD%E8%A8%80%E7%BF%BB%E8%AF%91/API%E6%96%87%E6%A1%A3/%E6%96%87%E6%9C%AC%E7%BF%BB%E8%AF%91%E6%9C%8D%E5%8A%A1/%E6%96%87%E6%9C%AC%E7%BF%BB%E8%AF%91%E6%9C%8D%E5%8A%A1-API%E6%96%87%E6%A1%A3.html");
                return "";
            }
            speakUrl = list.tSpeakUrl;
            // 接收序列化后的数据
            return string.Join("", list.translation);
        }

        private static string GetTranLang(string lang)
        {
            switch (lang)
            {
                case "自动检测":
                    return "auto";
                case "中文":
                    return "zh-CHS";
                case "英语":
                    return "en";
                case "俄语":
                    return "ru";
                case "粤语":
                    return "yue";
                case "日语":
                    return "ja";
                case "韩语":
                    return "ko";
                default:
                    return "en";
            }
        }

        /// <summary>
        /// 通用文字识别
        /// </summary>
        /// <param name="path"></param>
        /// <param name="img"></param>
        /// <returns>解析后的数据</returns>
        public static string GeneralBasic(Image img, string appKey = null, string appSecret = null)
        {
            string ak = appKey ?? YoudaoKey.AppKey;
            string secret = appSecret ?? YoudaoKey.AppSecret;
            string url = "https://openapi.youdao.com/ocrapi";
            string base64 = WebHelper.ImageToBase64(img);
            string salt = DateTime.Now.Millisecond.ToString();
            string curTime = WebHelper.GetTimeSpan();
            string sign = ComputeHash(ak + Truncate(base64) + salt + curTime + secret);
            string str = string.Format($"img={HttpUtility.UrlEncode(base64)}" +
                $"&langType=auto&detectType=10012&imageType=1&appKey={ak}" +
                $"&salt={salt}&sign={sign}&docType=json&signType=v3&curtime={curTime}");
            string result = WebHelper.Request(url, null, str);
            // 解析json数据
            
            string json_result = GetDictResult(result);
            JsonOCR list = JsonConvert.DeserializeObject<JsonOCR>(json_result);
            StringBuilder sb = new StringBuilder();
            foreach (var region in list.regions)
            {
                foreach (var line in region.lines)
                    sb.Append(line.text + Environment.NewLine);
            }
            // 删除最后一个换行符
            return sb.ToString().TrimEnd('\r', '\n');
        }

        /// <summary>
        /// 表格文字识别
        /// </summary>
        /// <param name="path"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string FormOcrRequest(string downloadDir, Image img)
        {
            string url = "https://openapi.youdao.com/ocr_table";
            string base64 = WebHelper.ImageToBase64(img);
            string salt = DateTime.Now.Millisecond.ToString();
            string curTime = WebHelper.GetTimeSpan();
            string sign = ComputeHash(YoudaoKey.AppKey + Truncate(base64) + salt + curTime + YoudaoKey.AppSecret);
            string str = string.Format($"q={HttpUtility.UrlEncode(base64)}" +
                $"&type=1&appKey={YoudaoKey.AppKey}" +
                $"&salt={salt}&sign={sign}&docType=excel&signType=v3&curtime={curTime}");
            string result = WebHelper.Request(url, null, str);

            // 解析json数据
            
            string json_result = GetDictResult(result);
            JsonForm list = JsonConvert.DeserializeObject<JsonForm>(json_result);

            if (list.tables.Count == 0)
                throw new Exception("识别失败！");

            // 将接收到的base64数据写入到xlsx文件
            if (downloadDir == null)
                return string.Format("识别成功。\r\n返回的Base64编码：\r\n{0}", string.Join("", list.tables.ToArray()));

            string downloadPath = new SavePath().FormPath + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
            if (!Directory.Exists(Path.GetDirectoryName(downloadPath)))
                Directory.CreateDirectory(Path.GetDirectoryName(downloadPath));
            using (FileStream fs = new FileStream(downloadPath, FileMode.Create))
            {
                foreach (var cont in list.tables)
                {
                    byte[] contents = Convert.FromBase64String(cont);
                    fs.Write(contents, 0, contents.Length);
                }
            }
            return string.Format("识别成功，已下载到当前程序目录。\r\n返回的Base64编码：\r\n{0}", string.Join("", list.tables.ToArray()));
        }


        /// <summary>
        /// 身份证识别
        /// </summary>
        /// <param name="path"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string Idcard(Image img)
        {
            string url = "https://openapi.youdao.com/ocr_structure";
            string base64 = WebHelper.ImageToBase64(img);
            string salt = DateTime.Now.Millisecond.ToString();
            string curTime = WebHelper.GetTimeSpan();
            string sign = ComputeHash(YoudaoKey.AppKey + Truncate(base64) + salt + curTime + YoudaoKey.AppSecret);
            string str = string.Format($"q={HttpUtility.UrlEncode(base64)}" +
                $"&structureType=idcard&appKey={YoudaoKey.AppKey}" +
                $"&salt={salt}&sign={sign}&docType=json&signType=v3&curtime={curTime}");
            string result = WebHelper.Request(url, null, str);
            // 解析json数据
            
            string json_result = GetDictResult(result);
            JsonId list = JsonConvert.DeserializeObject<JsonId>(json_result);
            return string.Format($"姓名：{list.Name}\r\n性别：{list.Gender}\r\n" +
    $"民族：{list.Nation}\r\n出生：{list.Birthday}\r\n" +
    $"住址：{list.Address}\r\n身份证号码：{list.IDNumber}");
        }

        /// <summary>
        /// 解析json数据到字典，对字典中"Result"项重新json编码并返回
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private static string GetDictResult(string json)
        {
            
            Dictionary<string, object> dict = JsonConvert.DeserializeObject(json) as Dictionary<string, object>;
            if (dict["errorCode"].ToString() != "0")
            {
                string errMsg = string.Format($"识别失败！\r\n错误代码：{dict["errorCode"]}");
                if (dict.ContainsKey("msg"))
                    errMsg += string.Format($"\r\n错误消息：{dict["msg"]}");
                throw new Exception(errMsg);
            }
            Dictionary<string, object> dict_result = dict["Result"] as Dictionary<string, object>;
            return JsonConvert.SerializeObject(dict_result);
        }

        /// <summary>
        /// 短语音识别
        /// </summary>
        /// <param name="audioPath">音频文件路径</param>
        /// <param name="rate">采样率</param>
        /// <param name="lang">要识别的语言</param>
        /// <returns></returns>
        public static string Asr(string audioPath, string rate, string lang)
        {
            string url = "https://openapi.youdao.com/asrapi";
            string base64 = WebHelper.FileToBase64(audioPath);
            string salt = DateTime.Now.Millisecond.ToString();
            string curTime = WebHelper.GetTimeSpan();
            string sign = ComputeHash(YoudaoKey.AppKey + Truncate(base64) + salt + curTime + YoudaoKey.AppSecret);
            string str = string.Format($"q={HttpUtility.UrlEncode(base64)}" +
                $"&langType={GetRecLang(lang)}&appKey={YoudaoKey.AppKey}" +
                $"&salt={salt}&sign={sign}&signType=v3&curtime={curTime}" +
                $"&format=wav&rate={rate}&channel=1&type=1");
            string result = WebHelper.Request(url, null, str);
            
            JsonRecognition lst = JsonConvert.DeserializeObject<JsonRecognition>(result);
            if (lst.errorCode != "0")
            {
                ErrorTip(lst.errorCode, "https://ai.youdao.com/DOCSIRMA/html/%E8%AF%AD%E9%9F%B3%E8%AF%86%E5%88%ABASR/API%E6%96%87%E6%A1%A3/%E7%9F%AD%E8%AF%AD%E9%9F%B3%E8%AF%86%E5%88%AB%E6%9C%8D%E5%8A%A1/%E7%9F%AD%E8%AF%AD%E9%9F%B3%E8%AF%86%E5%88%AB%E6%9C%8D%E5%8A%A1-API%E6%96%87%E6%A1%A3.html");
                return "";
            }
            return string.Join("", lst.result);
        }

        private static void ErrorTip(string errCode, string url)
        {
            DialogResult retVal = MessageBox.Show("错误码：" + errCode + "\r\n是否查看对应的错误消息？", "识别错误", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (retVal == DialogResult.Yes)
                Process.Start(url);
        }

        private static string GetRecLang(string lang)
        {
            switch (lang)
            {
                case "中文":
                    return "zh-CHS";
                case "英文":
                    return "en";
                case "日文":
                    return "ja";
                case "韩文":
                    return "ko";
                default:
                    return "zh-CHS";
            }

        }

        #region 通用文字识别接收json数据类
        class Lines
        {
            public string text { get; set; }
        }
        class Regions
        {
            public List<Lines> lines { get; set; }
        }
        class JsonOCR
        {
            public List<Regions> regions { get; set; }
        }
        #endregion

        #region 表格文字识别接收json数据类
        class JsonForm
        {
            public List<string> tables { get; set; }
        }
        #endregion

        #region 身份证识别接收json数据类
        class JsonId
        {
            public string Name { get; set; }
            public string Nation { get; set; }
            public string Gender { get; set; }
            public string Birthday { get; set; }
            public string Address { get; set; }
            public string IDNumber { get; set; }
        }
        #endregion

        #region 语音识别接收接送数据类
        class JsonRecognition
        {
            public string errorCode { get; set; }
            public string[] result { get; set; }
        }
        #endregion

        #region 有道翻译
        public class YoudaoTranslate
        {
            public string errorCode { get; set; }
            public string tSpeakUrl { get; set; }//翻译后的发音地址
            public List<string> translation { get; set; }
        }
        #endregion

        /// <summary>
        /// （当img长度大于20时）  返回q前10个字符 + q长度 + q后十个字符
        /// （当img长度小于等于20）返回字符串q。
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        private static string Truncate(string q)
        {
            if (q == null)
                return null;
            return q.Length <= 20 ? q : (q.Substring(0, 10) + q.Length.ToString() + q.Substring(q.Length - 10, 10));
        }

        /// <summary>
        /// 计算哈希值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static string ComputeHash(string input)
        {
            HashAlgorithm algorithm = SHA256.Create();
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "");
        }
    }
}
