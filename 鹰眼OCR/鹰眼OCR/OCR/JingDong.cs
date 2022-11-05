using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace 鹰眼OCR.OCR
{
    class JingDong
    {
        static JingDong()
        {
            // 避免错误：基础连接已经关闭: 未能为 SSL/TLS 安全通道建立信任关系。
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(RemoteCertificateValidate);
        }
        private static string Post(string url, byte[] buffer, string contentType = null, WebHeaderCollection headers = null, string appkey = null, string secretkey = null)
        {
            string ak = appkey ?? JingDongKey.AppKey;
            string sk = secretkey ?? JingDongKey.SecretKey;
            string timeSpan = TimeSpan();
            string sign = GetSign(sk, timeSpan);
            url += "?appkey=" + ak + "&timestamp=" + timeSpan + "&sign=" + sign;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "post";
            request.Timeout = 30_000;// 30秒超时
            request.ContentType = (contentType != null) ? contentType : "image/jpg";
            if (headers != null)
                request.Headers = headers;
            request.KeepAlive = true;
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    return reader.ReadToEnd();
            }
        }

        public static string BankCard(Image img)
        {
            string url = "https://aiapi.jd.com/jdai/ocr_bankcard";
            byte[] bArr = WebExt.ImageToBytes(img);
            string result = Post(url, bArr);
            return BankCardParseJson(result);
        }

        private static string BankCardParseJson(string json)
        {
            Dictionary<string, object> data = GetKeyValue(json, "code", "10000", "msg", "result");
            Dictionary<string, object> r = GetKeyValue(JsonConvert.SerializeObject(data), "code", "0", "message", "result");
            return string.Format($"卡号：{r["bank_number"]}\r\n卡号位数：{r["card_count"]}\r\n" +
                $"卡名称：{r["card_name"]}\r\n银行卡类型：{r["card_type"]}\r\n" +
                $"银行卡日期：{r["bank_date"]}\r\n银行名：{r["bank_name"]}");
        }

        private static Dictionary<string, object> GetKeyValue(string json, string codeName, string successCode, string msgName, string key)
        {
            
            Dictionary<string, object> obj_dict = JsonConvert.DeserializeObject(json) as Dictionary<string, object>;
            string code = string.Empty, msg = string.Empty;
            if (obj_dict.ContainsKey(codeName))
                code = obj_dict[codeName].ToString();
            if (obj_dict.ContainsKey(msgName))
                msg = obj_dict[msgName].ToString();
            if (code != successCode || !obj_dict.ContainsKey(key))
                throw new Exception("错误！" + "\n原因：" + msg);

            return (Dictionary<string, object>)obj_dict[key];
        }

        public static string Idcard(Image img)
        {
            string url = "https://aiapi.jd.com/jdai/ocr_idcard";
            byte[] bArr = WebExt.ImageToBytes(img);
            string result = Post(url, bArr);
            return IdcardParseJSON(result);
        }

        private static string IdcardParseJSON(string json)
        {
            Dictionary<string, object> data = GetKeyValue(json, "code", "10000", "msg", "result");
            
            // 解析json数据
            JsonId list = JsonConvert.DeserializeObject<JsonId>(JsonConvert.SerializeObject(data));
            if (list.code != "0") // 如果调用Api出现错误
                throw new Exception("OCR识别错误！" + "\n原因：" + list.message);
            string result = "";
            foreach (var item in list.resultData)
            {
                if (item.side == "front")
                    result = IdcardBackParseJson(list.resultData);
                if (item.side == "back")
                    result += IdcardFrontParseJson(list.resultData);
            }
            return result.Trim('\r', '\n');
        }

        private static string IdcardBackParseJson(List<Id_resultItem> list)
        {
            string str = string.Empty;
            foreach (var item in list)
            {
                str = string.Format($"照片面\r\n姓名：{item.name}\r\n性别：{item.sex}\r\n" +
    $"民族：{item.nation}\r\n出生：{item.birthday}\r\n" +
    $"住址：{item.address}\r\n身份证号码：{item.number}\r\n");
            }
            return str;
        }
        private static string IdcardFrontParseJson(List<Id_resultItem> list)
        {
            string str = string.Empty;
            foreach (var item in list)
            {
                str = string.Format($"\r\n国徽面\r\n签发机关：{item.authority}\r\n有效期：{item.timelimit}");
            }
            return str;
        }

        public static string GeneralBasic(Image img, string appkey = null, string secretkey = null)
        {
            string url = "https://aiapi.jd.com/jdai/ocr_universal";
            byte[] bArr = WebExt.ImageToBytes(img);
            string result = Post(url: url, buffer: bArr, contentType: null, headers: null, appkey: appkey, secretkey: secretkey);
            return GeneralParseJSON(result);
        }

        private static string GeneralParseJSON(string json)
        {
            
            Dictionary<string, object> data = GetKeyValue(json, "code", "10000", "msg", "result");
            // 解析json数据
            JsonOCR list = JsonConvert.DeserializeObject<JsonOCR>(JsonConvert.SerializeObject(data));
            if (list.code != "0") // 如果调用Api出现错误
                throw new Exception("OCR识别错误！" + "\n原因：" + list.message);
            if (list.resultData.Count == 0)
                return "未检测到文字";
            // 接收序列化后的数据
            StringBuilder builder = new StringBuilder();
            foreach (var item in list.resultData)
                builder.Append(item.text + "\r\n");
            // 查找最后一个换行符的位置
            return builder.ToString().TrimEnd(new char[] { '\r', '\n' });
        }


        public static void SpeechSynthesis(string text, string spd, string vol, string per, string fileName)
        {
            string url = "https://aiapi.jd.com/jdai/tts";
            byte[] buffer = Encoding.UTF8.GetBytes(text.Replace("\r", "").Replace("\n", ""));
            spd = (float.Parse(spd) / 10).ToString();  // 语速 取值范围：[0.1, 2.0]
            vol = (float.Parse(vol) / 10).ToString();  // 音量 取值范围：[0.1, 10.0]
            string tim = GetPeopleCode(per);           // 发音人代号

            WebHeaderCollection header = new WebHeaderCollection();
            string property = "{\"platform\": \"Windows\", \"version\": \"1.1.0.0\", " +
                "\"parameters\": {\"aue\": \"3\", \"vol\": \"" + vol + "\", \"sr\": \"16000\", \"sp\": \"" + spd + "\", \"tim\": \"" + tim + "\", \"tt\": \"0\"}}";
            header.Add("Service-Type", "synthesis");
            header.Add("Request-Id", new Random().Next(0, 100000000).ToString());
            header.Add("Sequence-Id", "-1");
            header.Add("Protocol", "1");
            header.Add("Net-State", "1");
            header.Add("Applicator", "1");
            header.Add("Property", property);
            string result = Post(url, buffer, "application/x-www-form-urlencoded", header);
            TTSParseJSON(result, fileName);
        }

        private static void TTSParseJSON(string json, string fileName)
        {
            Dictionary<string, object> result = GetKeyValue(json, "code", "10000", "msg", "result");
            
            JsonTTS list = JsonConvert.DeserializeObject<JsonTTS>(JsonConvert.SerializeObject(result));
            if (list.status != "0")
                throw new Exception("语音合成错误！" + "\n原因：" + list.message);
            byte[] buffer = Convert.FromBase64String(list.audio);
            File.WriteAllBytes(fileName, buffer);
        }

        /// <summary>
        /// 获取发音人对应的代号
        /// </summary>
        /// <param name="name">发音人名称</param>
        /// <returns></returns>
        public static string GetPeopleCode(string name)
        {
            switch (name)
            {
                case "桃桃":
                    return "0";
                case "斌斌":
                    return "1";
                case "婷婷":
                    return "3";
                default:
                    return "0";
            }
        }

        public static string ASR(string fileName, string rate)
        {
            string url = "https://aiapi.jd.com/jdai/asr";
            byte[] buffer = File.ReadAllBytes(fileName);
            // 请求参数
            WebHeaderCollection header = new WebHeaderCollection();
            string property = "{\"autoend\":false,\"encode\":{\"channel\":1,\"format\":\"wav\",\"sample_rate\":" + rate + "},\"platform\":\"Windows\",\"version\":\"1.1.0.1\"}";
            header.Add("Domain", "general");
            header.Add("Application-Id", new Random().Next(0, 100000000).ToString());
            header.Add("Request-Id", new Random().Next(0, 100000000).ToString());
            header.Add("Sequence-Id", "-1");
            header.Add("Asr-Protocol", "1");
            header.Add("Net-State", "2");
            header.Add("Applicator", "1");
            header.Add("Property", property);
            string result = Post(url, buffer, "application/octet-stream", header);
            return ASRParseJSON(result);
        }

        private static string ASRParseJSON(string json)
        {
            Dictionary<string, object> result = GetKeyValue(json, "code", "10000", "msg", "result");
            
            JsonASR list = JsonConvert.DeserializeObject<JsonASR>(JsonConvert.SerializeObject(result));
            if (list.status != "0")
                throw new Exception("语音合成错误！" + "\n原因：" + list.message);
            StringBuilder sb = new StringBuilder();
            foreach (var item in list.content)
                sb.Append(item.text);
            return sb.ToString();
        }

        private static string GetSign(string sk, string ts) => WebExt.GetMD5(sk + ts);

        private static string TimeSpan()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToString((long)ts.TotalMilliseconds);
        }

        private static bool RemoteCertificateValidate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {   // 总是接受  
            return true;
        }
    }

    #region 身份证识别接收数据类
    public class Id_resultItem
    {
        public string birthday;
        public string number;
        public string side;
        public string address;
        public string nation;
        public string sex;
        public string name;
        public string timelimit;
        public string authority;
    }

    public class JsonId
    {
        public string message;
        public string code;
        public List<Id_resultItem> resultData;
    }
    #endregion

    #region 通用文字识别接收数据类
    public class OCR_resultItem
    {
        public string text { get; set; }
    }

    public class JsonOCR
    {
        public string message;
        public string code;
        public List<OCR_resultItem> resultData;
    }
    #endregion

    #region 语音合成接收数据类
    public class JsonTTS
    {
        public string audio;
        public string message;
        public string status;
    }
    #endregion

    #region 语音识别接收数据类
    public class ContentText
    {
        public string text;
    }

    public class JsonASR
    {
        public List<ContentText> content;
        public string message;
        public string status;
    }
    #endregion
}
