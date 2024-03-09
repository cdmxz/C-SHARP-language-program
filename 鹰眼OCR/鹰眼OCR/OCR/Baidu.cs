using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace 鹰眼OCR.OCR
{
    class Baidu
    {
        /// <summary>
        /// 百度翻译
        /// </summary>
        /// <param name="srcText">源文本</param>
        /// <param name="from">源语言</param>
        /// <param name="to">目标语言</param>
        public static string Translate(string srcText, string from, string to, string appid = null, string password = null)
        {
            string salt = DateTime.Now.Millisecond.ToString();
            string id = appid ?? BaiduKey.AppId;
            string psd = password ?? BaiduKey.Password;
            // 签名
            string sign = GetTranslateMD5(id + srcText + salt + psd);
            string url = "https://fanyi-api.baidu.com/api/trans/vip/translate";
            string str = string.Format($"q={HttpUtility.UrlEncode(srcText)}&from={GetTranLang(from)}&to={GetTranLang(to)}" +
                 $"&appid={id}&salt={salt}&sign={sign}");
            string result = WebHelper.Request(url, null, str);
            BaiduTranslate list = JsonConvert.DeserializeObject<BaiduTranslate>(result);  // 将json数据转化为对象类型并赋值给list
            if (list.Error_code != null) // 如果调用Api出现错误
                throw new Exception("调用百度翻译失败" + "\n原因：" + list.Error_msg);

            // 接收序列化后的数据
            StringBuilder dst = new StringBuilder();
            foreach (var item in list.Trans_result)
                dst.Append(item.Dst + "\r\n");
            return dst.ToString().TrimEnd('\r', '\n');
        }

        private static string GetTranslateMD5(string str)
        {
            MD5 md5 = MD5.Create();
            //将输入字符串转换为字节数组并计算哈希数据  
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder builder = new StringBuilder();
            //循环遍历哈希数据的每一个字节并格式化为十六进制字符串  
            for (int i = 0; i < data.Length; i++)
                builder.Append(data[i].ToString("x2"));
            return builder.ToString();
        }

        private static string GetTranLang(string lang)
        {
            switch (lang)
            {
                case "自动检测":
                    return "auto";
                case "中文":
                    return "zh";
                case "英语":
                    return "en";
                case "俄语":
                    return "ru";
                case "粤语":
                    return "yue";
                case "文言文":
                    return "wyw";
                case "繁体中文":
                    return "cht";
                case "日语":
                    return "jp";
                case "韩语":
                    return "kor";
                default:
                    return "en";
            }
        }


        public static void ErrorCorrection(string text, out string srcText, out string correctQuery)
        {
            string host = "https://aip.baidubce.com/rpc/2.0/nlp/v1/ecnet?charset=UTF-8&&access_token=" + GetAccessToken(BaiduKey.CorrectionAK, BaiduKey.CorrectionSK);
            string str = "{\"text\":\"" + text + "\"}";
            string result = WebHelper.Request(host, null, str, "application/json");
            CorrectionParseJSON(result, out srcText, out correctQuery);
        }

        private static void CorrectionParseJSON(string json, out string srcText, out string correctQuery)
        {
            Dictionary<string, object> obj_dict = JsonConvert.DeserializeObject(json) as Dictionary<string, object>;
            if (obj_dict.ContainsKey("error_msg"))
                throw new Exception("文本纠错失败！原因：" + obj_dict["error_msg"].ToString());
            srcText = obj_dict["text"].ToString();
            Dictionary<string, object> item = obj_dict["item"] as Dictionary<string, object>;
            correctQuery = item["correct_query"].ToString();
        }


        /// <summary>
        /// 百度语音合成
        /// </summary>
        /// <param name="text">文本</param>
        /// <param name="spd">语速</param>
        /// <param name="pit">语调</param>
        /// <param name="vol">音量</param>
        /// <param name="per">发音人</param>
        /// <param name="fileName">保存到本地的文件名</param>
        public static void SpeechSynthesis(string text, string spd, string pit, string vol, string per, string fileName)
        {
            string host = "https://tsn.baidu.com/text2audio";
            string t = HttpUtility.UrlEncode(text);
            string str = string.Format($"tok={GetAccessToken(BaiduKey.TTS_ApiKey, BaiduKey.TTS_SecretKey)}&tex={HttpUtility.UrlEncode(t)}&cuid={DateTime.Now.Millisecond}&ctp=0&lan=zh&spd={spd}&pit={pit}&vol={vol}&per={per}&aue=3");

            var httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromMilliseconds(30000)
            };

            var content = new StringContent(str, Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = httpClient.PostAsync(host, content).Result;
            if (response.Content.Headers.ContentType.MediaType.Equals("application/json"))
            {
                var json = response.Content.ReadAsStringAsync().Result;
                SpeechSynthesisJson list = JsonConvert.DeserializeObject<SpeechSynthesisJson>(json);
                throw new Exception($"语音合成错误！\r\n错误码：{list.err_no}\r\n错误信息：{list.err_msg}");
            }
            // 把 Stream 转换成 byte[] 后写入文件
            Stream stream = response.Content.ReadAsStream();
            try
            {
                byte[] bytes = new byte[4096];
                using FileStream fs = new(fileName, FileMode.Create);
                int len;
                while ((len = stream.Read(bytes, 0, bytes.Length)) > 0)
                    fs.Write(bytes, 0, len);
                fs.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                stream.Close();
            }
        }

        class SpeechSynthesisJson
        {
            public string err_no { get; set; }
            public string err_msg { get; set; }
        }

        /// <summary>
        /// 语音识别
        /// </summary>
        /// <param name="audioPath"></param>
        /// <param name="rate"></param>
        /// <param name="devPid"></param>
        /// <returns></returns>
        public static string Asr(string audioPath, string rate, string devPid)
        {
            string host = "https://vop.baidu.com/pro_api?";
            string par = $"format=wav&rate=16000&channel=1&cuid=sdzfsad&token={GetAccessToken(BaiduKey.TTS_ApiKey, BaiduKey.TTS_SecretKey)}&dev_pid=80001" +
                $"&speech={WebHelper.FileToBase64(audioPath)}&len={WebHelper.GetFileSize(audioPath)}";
            string result = WebHelper.Request(host, null, par, "application/json");
            var list = JsonConvert.DeserializeObject<RecognitionResult>(result);
            if (list.err_no != "0")
                throw new Exception($"语音识别失败！\r\n错误代码：{list.err_no}\r\n错误消息：{list.err_msg}");
            return string.Join("", list.result);
        }

        private static string GetDevPid(string devPid)
        {
            if (devPid == "普通话")
                return "1537";
            else
                return "1737";// 英语
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
                case "度小宇":
                    return "1";
                case "度小美":
                    return "0";
                case "度逍遥":
                    return "3";
                case "度丫丫":
                    return "4";
                default:
                    return "1";
            }
        }

        /// <summary>
        /// 获取AccessToken，失败时抛出异常
        /// </summary>
        /// <returns></returns>
        public static string GetAccessToken(string apikey = null, string secretkey = null)
        {
            string host = "https://aip.baidubce.com/oauth/2.0/token";
            string ak = apikey ?? BaiduKey.ApiKey;
            string sk = secretkey ?? BaiduKey.SecretKey;
            string str = string.Format($"grant_type=client_credentials&client_id={ak}&client_secret={sk}");
            string result = WebHelper.Request(host, "", str);
            Token list = JsonConvert.DeserializeObject<Token>(result);// 将json数据转化为对象并赋值给list
            if (list.error != null)
                throw new Exception("获取AccessToken失败！" + "\n原因：" + list.error_description);

            return list.access_token;
        }


        /// <summary>
        /// 百度文字识别（通用、通用（高精度）、手写、数字）
        /// </summary>
        /// <param name="mode">模式（通用、通用（高精度）、手写、数字）</param>
        /// <param name="path">图片路径</param>
        /// <param name="img">image</param>
        /// <param name="langType">识别语言</param>
        /// <returns></returns>
        public static string GeneralBasic(string mode, Image img, string langType = null)
        {
            string general_basic_host = "https://aip.baidubce.com/rest/2.0/ocr/v1/general_basic?access_token=";
            string accurate_basic_host = "https://aip.baidubce.com/rest/2.0/ocr/v1/accurate_basic?access_token=";
            string handwriting_host = "https://aip.baidubce.com/rest/2.0/ocr/v1/handwriting?access_token=";
            string numbers_host = "https://aip.baidubce.com/rest/2.0/ocr/v1/numbers?access_token=";

            // 进行base64编码
            string base64 = WebHelper.ImageToBase64(img);
            string host = null, str = null;
            if (mode.IndexOf("通用") != -1)
            {
                host = general_basic_host;
                str = "image=" + HttpUtility.UrlEncode(base64, Encoding.UTF8) + "&language_type=" + GetLangType(langType) + "&detect_direction=true";
            }
            else if (mode.IndexOf("高精度") != -1)
            {
                host = accurate_basic_host;
                str = "image=" + HttpUtility.UrlEncode(base64, Encoding.UTF8) + "&language_type=" + GetLangType(langType) + "&detect_direction=true";
            }
            else if (mode.IndexOf("手写") != -1)
            {
                host = handwriting_host;
                str = "image=" + HttpUtility.UrlEncode(base64, Encoding.UTF8);
            }
            else if (mode.IndexOf("数字") != -1)
            {
                host = numbers_host;
                str = "image=" + HttpUtility.UrlEncode(base64, Encoding.UTF8) + "&detect_direction=true";
            }
            // 发送请求，接收数据
            string result = WebHelper.Request(host, GetAccessToken(), str);
            return GeneralParseJSON(result);
        }

        public static string GetLangType(string lang)
        {
            switch (lang)
            {
                case "中英混合":
                    return "CHN_ENG";
                case "英文":
                    return "ENG";
                case "葡萄牙语":
                    return "POR";
                case "法语":
                    return "FRE";
                case "德语":
                    return "GER";
                case "意大利语":
                    return "ITA";
                case "西班牙语":
                    return "SPA";
                case "俄语":
                    return "RUS";
                case "日语":
                    return "JAP";
                case "韩语":
                    return "KOR";
                default: return "CHN_ENG";
            }
        }

        /// <summary>
        /// 百度文字识别-解析返回的json数据
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private static string GeneralParseJSON(string json)
        {
            // 解析json数据

            BaiduOcrJson list = JsonConvert.DeserializeObject<BaiduOcrJson>(json);
            if (list.error_code != null) // 如果调用Api出现错误
                throw new Exception("OCR识别错误！" + "\n原因：" + list.error_msg);
            if (list.words_result.Count == 0)
                return "未检测到文字";
            // 接收序列化后的数据
            StringBuilder builder = new StringBuilder();
            foreach (var item in list.words_result)
                builder.Append(item.words + "\r\n");

            // 查找最后一个换行符的位置
            return builder.ToString().TrimEnd(new char[] { '\r', '\n' });
        }

        /// <summary>
        /// 表格文字识别
        /// </summary>
        /// <param name="path">图片路径</param>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string FormOcrRequest(string downloadDir, Image img)
        {
            string host = "https://aip.baidubce.com/rest/2.0/solution/v1/form_ocr/request?access_token=";
            string base64 = WebHelper.ImageToBase64(img);
            string str = "image=" + HttpUtility.UrlEncode(base64) + "&is_sync=true&request_type=excel";
            string json = WebHelper.Request(host, GetAccessToken(), str);
            // 接收序列化后的数据
            string result = FormOcrParseJson(json, out string url);

            if (downloadDir == null)
                return result;
            string downloadPath = downloadDir + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xls";
            if (!Directory.Exists(Path.GetDirectoryName(downloadPath)))
                Directory.CreateDirectory(Path.GetDirectoryName(downloadPath));
            try
            {
                WebHelper.DownloadFile(url, downloadPath);
                result += "\r\n是否下载：下载表格成功。\r\n保存路径：" + downloadPath;
            }
            catch (Exception ex)
            {
                result += "\r\n是否下载：下载表格失败！\r\n" + "原因：" + ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 表格文字识别-解析返回的json数据
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private static string FormOcrParseJson(string json, out string url)
        {
            FormOcrJson list = JsonConvert.DeserializeObject<FormOcrJson>(json);
            if (list.error_msg != null)
                throw new Exception("表格识别出错！原因：" + list.error_msg);
            url = list.result.result_data;
            return string.Format($"表格识别结果：\r\n识别进度：{list.result.percent}%\r\n识别状态：{list.result.ret_msg}\r\n下载地址：{url}");
        }

        /// <summary>
        /// 身份证识别
        /// </summary>
        /// <param name="cardSide">"front"含照片面，"back"含国徽面</param>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string Idcard(string cardSide, Image img)
        {
            string host = "https://aip.baidubce.com/rest/2.0/ocr/v1/idcard?access_token=";
            string base64 = WebHelper.ImageToBase64(img);
            string str = "AESEncry=false&&id_card_side=" + cardSide + "&detect_direction=true&detect_risk=true&detect_photo=true&image=" + HttpUtility.UrlEncode(base64);
            string json = WebHelper.Request(host, GetAccessToken(), str);
            string result;
            if (cardSide.ToLower() == "back") // 含国徽面 
                result = IdcardBackParseJson(json);
            else                              // 含照片面
                result = IdcardFrontParseJson(json);
            return result;
        }

        /// <summary>
        /// 身份证识别（含国徽面）-解析返回的json数据
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private static string IdcardBackParseJson(string json)
        {
            IdcardBackJson list = JsonConvert.DeserializeObject<IdcardBackJson>(json);
            if (list == null)
                throw new Exception("无法解析返回结果！");
            string img_status = ImageStatuToStr(list.image_status);
            string risk_type = RiskTypeToStr(list.risk_type);
            if (img_status.IndexOf("身份证正反面颠倒") != -1)
                return string.Format(
                     $"识别状态：{img_status}\r\n" +
                     $"身份证类型：{risk_type}\r\n");
            else
                return string.Format(
                    $"识别状态：{img_status}\r\n" +
                    $"身份证类型：{risk_type}\r\n" +
                    $"签发日期：{FormatDate(list.words_result.签发日期.words)}\r\n" +
                    $"失效日期：{FormatDate(list.words_result.失效日期.words)}\r\n" +
                    $"签发机关：{list.words_result.签发机关.words}");
        }

        /// <summary>
        /// 身份证识别（含照片面）-解析返回的json数据
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private static string IdcardFrontParseJson(string json)
        {
            IdcardFrontJson list = JsonConvert.DeserializeObject<IdcardFrontJson>(json);
            if (list == null)
                throw new Exception("无法解析返回结果！");
            string date = FormatDate(list.words_result.出生.words);
            string result = string.Format(
                    $"识别状态：{ImageStatuToStr(list.image_status)}\r\n" +
                    $"身份证类型：{RiskTypeToStr(list.risk_type)}\r\n" +
                    $"身份证号码、性别、出生是否一致：{NumberTypeToStr(list.idcard_number_type)}\r\n" +
                    $"姓名：{list.words_result.姓名.words}\r\n" +
                    $"性别：{list.words_result.性别.words}\r\n" +
                    $"民族：{list.words_result.民族.words}\r\n" +
                    $"出生：{date}\r\n" +
                    $"住址：{list.words_result.住址.words}\r\n" +
                    $"身份证号码：{list.words_result.公民身份号码.words}");
            if (list.edit_tool != null)
                result += "\r\n编辑软件名称：" + list.edit_tool;
            return result;
        }

        private static string FormatDate(string dateStr)
        {
            // 输入：19890601
            // 输出：1989年06月01日
            StringBuilder sb = new StringBuilder(dateStr);
            sb.Insert(4, "年");
            sb.Insert(7, "月");
            sb.Append("日");
            return sb.ToString();
            // return date.ToString("yyyy年MM月dd日");
        }

        private static string NumberTypeToStr(string numberType)
        {
            switch (numberType)
            {
                case "-1":
                    return "身份证正面所有字段全为空";
                case "0":
                    return "身份证证号识别错误";
                case "1":
                    return "身份证证号和性别、出生信息一致";
                case "2":
                    return "身份证证号和性别、出生信息都不一致";
                case "3":
                    return "身份证证号和出生信息不一致";
                case "4":
                    return "身份证证号和性别信息不一致";
                default:
                    return numberType;
            }
        }

        private static string ImageStatuToStr(string image_status)
        {
            switch (image_status)
            {
                case "normal":
                    return "识别正常";
                case "reversed_side":
                    return "身份证正反面颠倒";
                case "non_idcard ":
                    return "上传的图片中不包含身份证 ";
                case "blurred":
                    return " 身份证模糊   ";
                case "other_type_card ":
                    return " 其他类型证照   ";
                case "over_exposure ":
                    return " 身份证关键字段反光或过曝  ";
                case "over_dark ":
                    return " 身份证欠曝（亮度过低） ";
                case "unknown ":
                    return " 未知状态";
                default:
                    return image_status;
            }
        }

        private static string RiskTypeToStr(string risk_type)
        {
            switch (risk_type)
            {
                case "normal":
                    return "正常身份证";
                case "copy":
                    return "复印件";
                case "temporary":
                    return " 临时身份证";
                case "screen":
                    return " 翻拍";
                case "unknown":
                    return "其他未知情况";
                default:
                    return risk_type;
            }
        }


        /// <summary>
        /// 银行卡识别
        /// </summary>
        /// <param name="path"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string BankCard(Image img)
        {
            string host = "https://aip.baidubce.com/rest/2.0/ocr/v1/bankcard?access_token=";
            string base64 = WebHelper.ImageToBase64(img);
            string str = "image=" + HttpUtility.UrlEncode(base64) + "&detect_direction=true";
            string result = WebHelper.Request(host, GetAccessToken(), str);
            result = BankCardParseJson(result);
            return result;
        }

        /// <summary>
        /// 银行卡识别-解析返回的json数据
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private static string BankCardParseJson(string json)
        {
            BankCardJson list = JsonConvert.DeserializeObject<BankCardJson>(json);
            if (list == null)
                throw new Exception("无法解析返回结果！");
            string cardType = GetBankCardType(list.result.bank_card_type);
            return string.Format(
                $"卡号：{list.result.bank_card_number}\r\n" +
                $"有效期：{list.result.valid_date}\r\n" +
                $"银行卡类型：{cardType}\r\n" +
                $"银行名：{list.result.bank_name}\r\n" +
                $"持卡人姓名：{list.result.holder_name}");
        }

        private static string GetBankCardType(int type)
        {
            switch (type)
            {
                case 0:
                    return "不能识别";
                case 1:
                    return "借记卡";
                case 2:
                    return "贷记卡";
                case 3:
                    return "准贷记卡";
                case 4:
                    return "预付费卡";
                default:
                    return "未知类型卡";
            }
        }


        /// <summary>
        /// 公式识别
        /// </summary>
        /// <param name="path"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string Formula(Image img)
        {
            string host = "https://aip.baidubce.com/rest/2.0/ocr/v1/formula?access_token=";
            string base64 = WebHelper.ImageToBase64(img);
            string str = "image=" + HttpUtility.UrlEncode(base64) + "&detect_direction=true";
            string result = WebHelper.Request(host, GetAccessToken(), str);
            return GeneralParseJSON(result);
        }

        /// <summary>
        /// 二维码识别
        /// </summary>
        /// <param name="path"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string QRCode(Image img)
        {
            string host = "https://aip.baidubce.com/rest/2.0/ocr/v1/qrcode?access_token=";
            string base64 = WebHelper.ImageToBase64(img);
            string str = "image=" + HttpUtility.UrlEncode(base64);
            string result = WebHelper.Request(host, GetAccessToken(), str);
            // 解析json数据

            BaiduOcrJson list = JsonConvert.DeserializeObject<BaiduOcrJson>(result);
            if (list.error_code != null) // 如果调用Api出现错误
                throw new Exception("OCR识别错误！" + "\n原因：" + list.error_msg);
            if (list.codes_result.Count == 0)
                return "未检测到二维码";

            // 接收序列化后的数据
            StringBuilder builder = new StringBuilder();
            foreach (var item in list.codes_result)
            {
                builder.Append("二维码类型：" + item.type + "\r\n");
                builder.Append("二维码内容：");
                foreach (var text in (IEnumerable<object>)item.text)
                    builder.Append(text + "\r\n");
            }

            // 查找最后一个换行符的位置
            int len = builder.ToString().LastIndexOf('\r');
            return len < 0 ? "" : builder.ToString().Remove(len);
        }


        /// <summary>
        /// 测试百度key是否有效
        /// </summary>
        /// <returns></returns>
        public static bool BaiduKeyTest()
        {
            try
            {
                GetAccessToken();// 测试文字识别Key只需要测试能否获得AccessToken就可以了
                Translate("Test", "en", "zh");// 测试百度翻译Key
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }


        #region 获取百度accesstoken 接收json数据类 
        class Token
        {
            public string error { get; set; }
            public string error_description { get; set; }
            public string access_token { get; set; }
        }
        #endregion

        #region 百度翻译 接收json数据类 
        public class Translation
        {
            public string Src { get; set; }
            public string Dst { get; set; }
        }

        public class BaiduTranslate
        {
            // 百度翻译
            public string Error_code { get; set; }
            public string Error_msg { get; set; }
            public List<Translation> Trans_result { get; set; }
        }
        #endregion

        #region 百度文字识别 接收json数据类 
        public class Words_resultItem
        {
            public string words { get; set; }
        }

        public class CodesResult
        {
            public object type { get; set; }
            public object text { get; set; }
        }

        public class BaiduOcrJson
        {
            // 百度文字识别
            public string error_code { get; set; }
            public string error_msg { get; set; }
            public List<Words_resultItem> words_result { get; set; }

            public List<CodesResult> codes_result { get; set; }
        }
        #endregion


        class RecognitionResult
        {
            public string err_no { get; set; }
            public string err_msg { get; set; }

            public List<string> result { get; set; }
        }

        class FormOcrJson
        {
            public string log_id { get; set; }
            public string error_msg { get; set; }
            public Result result { get; set; }
            public class Result
            {
                public string result_data { get; set; }
                public string ret_msg { get; set; }
                public string percent { get; set; }
                public string ret_code { get; set; }
            }
        }


        /// <summary>
        /// 身份证国徽面 json实体类
        /// </summary>
        class IdcardBackJson
        {
            public class ExpirationDate
            {
                public string words { get; set; }
            }

            public class Department
            {
                public string words { get; set; }
            }

            public class DateOfIssuance
            {
                public string words { get; set; }
            }

            public class Words_result
            {
                public ExpirationDate 失效日期 { get; set; }
                public Department 签发机关 { get; set; }
                public DateOfIssuance 签发日期 { get; set; }
            }
            public Words_result words_result { get; set; }
            public int error_code { get; set; }
            public string image_status { get; set; }
            public string risk_type { get; set; }
        }

        /// <summary>
        /// 身份证照片面 json实体类
        /// </summary>
        class IdcardFrontJson
        {
            public class Name
            {
                public string words { get; set; }
            }
            public class Nation
            {
                public string words { get; set; }
            }
            public class Address
            {
                public string words { get; set; }

            }
            public class IdNumber
            {
                public string words { get; set; }
            }
            public class Birthday
            {
                public string words { get; set; }
            }
            public class Sex
            {
                public string words { get; set; }
            }
            public class Words_result
            {
                public Name 姓名 { get; set; }
                public Nation 民族 { get; set; }
                public Address 住址 { get; set; }
                public IdNumber 公民身份号码 { get; set; }
                public Birthday 出生 { get; set; }
                public Sex 性别 { get; set; }
            }
            public Words_result words_result { get; set; }
            public string risk_type { get; set; }
            public string image_status { get; set; }
            public string idcard_number_type { get; set; }
            public string edit_tool { get; set; }
            public int direction { get; set; }

        }


        /// <summary>
        /// 银行卡识别 实体类
        /// </summary>
        class BankCardJson
        {
            public int direction { set; get; }
            public Result result { set; get; }
            public class Result
            {
                public string bank_card_number { get; set; }
                public string valid_date { get; set; }
                public int bank_card_type { get; set; }
                public string bank_name { get; set; }
                public string holder_name { get; set; }
            }
        }
    }
}
