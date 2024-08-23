using Newtonsoft.Json;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;

namespace 鹰眼OCR_Extensions.OCR
{
    public class Baidu
    {
        private readonly static Dictionary<string, string> TranslateLangDict = new Dictionary<string, string>()
        {
            { "自动检测", "auto" },
            { "中文", "zh" },
            { "英语", "en" },
            { "俄语", "ru" },
            { "粤语", "yue" },
            { "文言文", "wyw" },
            { "繁体中文", "cht" },
            { "日语", "jp" },
            { "韩语", "kor" },
        };

        private readonly static Dictionary<string, string> OCRLangDict = new Dictionary<string, string>()
        {
           { "中英混合" , "CHN_ENG"},
           {"英文" , "ENG"},
           {"葡萄牙语" , "POR"},
           {"法语" , "FRE"},
           {"德语" , "GER"},
           {"意大利语" , "ITA"},
           {"西班牙语" , "SPA"},
           {"俄语" , "RUS"},
           {"日语" , "JAP"},
           {"韩语" , "KOR"},
        };

        private static Dictionary<string, string> IdcardSide = new Dictionary<string, string>()
        {
            { "照片面","front"},
            { "国徽面","back"},
        };


        /// <summary>
        /// 获取支持的翻译语言中文名称
        /// </summary>
        /// <returns></returns>
        public static string[] GetTranslateLangChinese()
        {
            return TranslateLangDict.Keys.ToArray();
        }

        /// <summary>
        /// 获取支持的ocr语言中文名称
        /// </summary>
        /// <returns></returns>
        public static string[] GetOCRLangChinese()
        {
            return OCRLangDict.Keys.ToArray();
        }

        /// <summary>
        /// 获取身份证正反面字符串
        /// </summary>
        /// <returns></returns>
        public static string[] GetIdCardSides()
        {
            return IdcardSide.Keys.ToArray();
        }

        public BaiduKey BaiduKey;
        private static readonly char[] trimChars = new char[] { '\r', '\n' };


        public Baidu()
        {
        }

        public Baidu(BaiduKey baiduKey)
        {
            BaiduKey = baiduKey;
        }


        /// <summary>
        /// 获取AccessToken，失败时抛出异常
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetAccessTokenAsync()
        {
            string host = "https://aip.baidubce.com/oauth/2.0/token";
            string ak = BaiduKey.ApiKey;
            string sk = BaiduKey.SecretKey;

            string str = string.Format($"grant_type=client_credentials&client_id={ak}&client_secret={sk}");
            string result = await WebHelper.RequestAsync(host, "", str);
            Token? list = JsonConvert.DeserializeObject<Token>(result);// 将json数据转化为对象并赋值给list
            if (list == null || list.error != null)
            {
                throw new Exception("获取AccessToken失败！" + "\n原因：" + list?.error_description);
            }

            return list.access_token;
        }



        /// <summary>
        /// 百度翻译
        /// </summary>
        /// <param name="srcText">源文本</param>
        /// <param name="from">源语言</param>
        /// <param name="to">目标语言</param>
        public async Task<string> TranslateAsync(string srcText, string from, string to)
        {
            string salt = DateTime.Now.Millisecond.ToString();
            string id = BaiduKey.AppId;
            string psd = BaiduKey.Password;
            // 签名
            string sign = WebHelper.GetTranslateMD5(id + srcText + salt + psd);
            string url = "https://fanyi-api.baidu.com/api/trans/vip/translate";
            string str = $"q={HttpUtility.UrlEncode(srcText)}&from={GetTranLang(from)}&to={GetTranLang(to)}" +
                 $"&appid={id}&salt={salt}&sign={sign}";
            string result = await WebHelper.RequestAsync(url, "", str);
            BaiduTranslate? list = JsonConvert.DeserializeObject<BaiduTranslate>(result);  // 将json数据转化为对象类型并赋值给list
            if (list == null || list.Error_code != null) // 如果调用Api出现错误
            {
                throw new Exception("调用百度翻译失败" + "\n原因：" + list?.Error_msg);
            }

            // 接收序列化后的数据
            StringBuilder dst = new StringBuilder();
            foreach (var item in list.Trans_result)
            {
                dst.Append(item.Dst + "\r\n");
            }

            return dst.ToString().TrimEnd('\r', '\n');
        }

        // 获取翻译语言代码
        private static string GetTranLang(string lang)
        {
            return TranslateLangDict.GetValueOrDefault(lang, "en");
        }




        /// <summary>
        /// 文本纠错
        /// </summary>
        /// <param name="text"></param>
        /// <param name="srcText"></param>
        /// <param name="correctQuery"></param>
        public async Task<string> TextCorrectAsync(string text)
        {
            string token = await GetAccessTokenAsync();
            string host = "https://aip.baidubce.com/rpc/2.0/nlp/v1/ecnet?charset=UTF-8&&access_token=" + token;
            string str = "{\"text\":\"" + text + "\"}";
            string result = WebHelper.Request(host, "", str, "application/json");
            return CorrectionParseJSON(result);
        }

        private static string CorrectionParseJSON(string json)
        {
            Dictionary<string, object>? obj_dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            if (obj_dict == null || obj_dict.ContainsKey("error_msg"))
            {
                throw new Exception("文本纠错失败！原因：" + obj_dict?["error_msg"].ToString());
            }
            // string srcText = obj_dict["text"].ToString();
            Dictionary<string, object>? item = obj_dict["item"] as Dictionary<string, object>;
            string correctQuery = item?["correct_query"].ToString() ?? throw new Exception("无法解析返回数据！");
            return correctQuery;
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
        public async void SpeechSynthesisAsync(string text, string spd, string pit, string vol, string per, string fileName)
        {
            string host = "https://tsn.baidu.com/text2audio";
            string t = HttpUtility.UrlEncode(text);
            string token = await GetAccessTokenAsync();
            string str = string.Format($"tok={token}&tex={HttpUtility.UrlEncode(t)}&cuid={DateTime.Now.Millisecond}&ctp=0&lan=zh&spd={spd}&pit={pit}&vol={vol}&per={per}&aue=3");

            var response = await WebHelper.RequestToResponseAsync(host, "", str, "");
            if (response.Content.Headers.ContentType.Equals("application/json"))
            {
                var json = response.Content.ReadAsStringAsync().Result;
                SpeechSynthesisJson? list = JsonConvert.DeserializeObject<SpeechSynthesisJson>(json);
                throw new Exception($"语音合成错误！\r\n错误码：{list?.err_no}\r\n错误信息：{list?.err_msg}");
            }
            // 把 Stream 写入文件
            using Stream stream = response.Content.ReadAsStream();
            WebHelper.StreamToFile(stream, fileName);
            response.Dispose();
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
        public async Task<string> AsrAsync(string audioPath, string rate, string devPid)
        {
            string host = "https://vop.baidu.com/pro_api?";
            string token = await GetAccessTokenAsync();
            string par = $"format=wav&rate=16000&channel=1&cuid=sdzfsad&token={token}&dev_pid=80001" +
                $"&speech={WebHelper.FileToBase64(audioPath)}&len={WebHelper.GetFileSize(audioPath)}";
            string result = await WebHelper.RequestAsync(host, "", par, "application/json");
            var list = JsonConvert.DeserializeObject<RecognitionResult>(result);
            if (list?.err_no != "0")
            {
                throw new Exception($"语音识别失败！\r\n错误代码：{list.err_no}\r\n错误消息：{list.err_msg}");
            }

            return string.Join("", list.result);
        }

        private static string GetDevPid(string devPid)
        {
            if (devPid == "普通话")
            {
                return "1537";
            }
            else
            {
                return "1737";// 英语
            }
        }

        /// <summary>
        /// 获取发音人对应的代号
        /// </summary>
        /// <param name="name">发音人名称</param>
        /// <returns></returns>
        private static string GetPeopleCode(string name)
        {
            return name switch
            {
                "度小宇" => "1",
                "度小美" => "0",
                "度逍遥" => "3",
                "度丫丫" => "4",
                _ => "1",
            };
        }




        /// <summary>
        /// 百度文字识别（通用、通用（高精度）、手写、数字）
        /// </summary>
        /// <param name="mode">模式（通用、通用（高精度）、手写、数字）</param>
        /// <param name="img">image</param>
        /// <param name="langType">识别语言</param>
        /// <returns></returns>
        public async Task<string> GeneralBasicAsync(string mode, Image img, string langType = "")
        {
            string general_basic_host = "https://aip.baidubce.com/rest/2.0/ocr/v1/general_basic?access_token=";
            string accurate_basic_host = "https://aip.baidubce.com/rest/2.0/ocr/v1/accurate_basic?access_token=";
            string handwriting_host = "https://aip.baidubce.com/rest/2.0/ocr/v1/handwriting?access_token=";
            string numbers_host = "https://aip.baidubce.com/rest/2.0/ocr/v1/numbers?access_token=";

            // 进行base64编码
            string base64 = WebHelper.ImageToBase64(img);
            string host = "";
            string str = "image=" + HttpUtility.UrlEncode(base64, Encoding.UTF8);
            string token = await GetAccessTokenAsync();
            if (mode.Contains("通用"))
            {
                host = general_basic_host;
                str += "&language_type=" + GetLangType(langType) + "&detect_direction=true";
            }
            else if (mode.Contains("高精度"))
            {
                host = accurate_basic_host;
                str += "&language_type=" + GetLangType(langType) + "&detect_direction=true";
            }
            else if (mode.Contains("手写"))
            {
                host = handwriting_host;
            }
            else if (mode.Contains("数字"))
            {
                host = numbers_host;
                str += "&detect_direction=true";
            }
            // 发送请求，接收数据
            string result = await WebHelper.RequestAsync(host, token, str);
            return GeneralParseJSON(result);
        }

        // 获取ocr语言代码
        private static string GetLangType(string lang)
        {
            return OCRLangDict.GetValueOrDefault(lang, "CHN_ENG");
        }

        /// <summary>
        /// 百度文字识别-解析返回的json数据
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private static string GeneralParseJSON(string json)
        {
            // 解析json数据

            BaiduOcrJson? list = JsonConvert.DeserializeObject<BaiduOcrJson>(json);
            if (list == null || list.error_code != null) // 如果调用Api出现错误
            {
                throw new Exception("OCR识别错误！" + "\n原因：" + list?.error_msg);
            }

            if (list.words_result.Count == 0)
            {
                return "未检测到文字";
            }
            // 接收序列化后的数据
            StringBuilder builder = new StringBuilder();
            foreach (var item in list.words_result)
            {
                builder.Append(item.words + "\r\n");
            }

            // 查找最后一个换行符的位置
            return builder.ToString().TrimEnd(trimChars);
        }




        /// <summary>
        /// 表格文字识别
        /// </summary>
        /// <param name="downloadDir">下载文件夹</param>
        /// <param name="img"></param>
        /// <returns></returns>
        public async Task<string> FormOcrRequestAsync(string downloadDir, Image img)
        {
            string host = "https://aip.baidubce.com/rest/2.0/solution/v1/form_ocr/request?access_token=";
            string base64 = WebHelper.ImageToBase64(img);
            string str = "image=" + HttpUtility.UrlEncode(base64) + "&is_sync=true&request_type=excel";
            string token = await GetAccessTokenAsync();
            string json = await WebHelper.RequestAsync(host, token, str);
            // 接收序列化后的数据
            string result = FormOcrParseJson(json, out string url);

            if (string.IsNullOrWhiteSpace(downloadDir))
            {
                return result;
            }

            string downloadPath = downloadDir + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xls";
            if (!Directory.Exists(Path.GetDirectoryName(downloadPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(downloadPath));
            }

            try
            {
                await WebHelper.DownloadFileAsync(url, downloadPath);
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
            FormOcrJson? list = JsonConvert.DeserializeObject<FormOcrJson>(json);
            if (list == null || list.error_msg != null)
            {
                throw new Exception("表格识别出错！原因：" + list?.error_msg);
            }

            url = list.result.result_data;
            return string.Format($"表格识别结果：\r\n识别进度：{list.result.percent}%\r\n识别状态：{list.result.ret_msg}\r\n下载地址：{url}");
        }





        /// <summary>
        /// 身份证识别
        /// </summary>
        /// <param name="cardSide">"front"含照片面，"back"含国徽面</param>
        /// <param name="img"></param>
        /// <returns></returns>
        public async Task<string> IdcardAsync(string cardSide, Image img)
        {
            string host = "https://aip.baidubce.com/rest/2.0/ocr/v1/idcard?access_token=";
            string base64 = WebHelper.ImageToBase64(img);
            string token = await GetAccessTokenAsync();
            cardSide = GetIdcardSideCode(cardSide);
            string str = "AESEncry=false&&id_card_side=" + cardSide + "&detect_direction=true&detect_risk=true&detect_photo=true&image=" + HttpUtility.UrlEncode(base64);
            string json = await WebHelper.RequestAsync(host, token, str);
            string result;
            if (cardSide.Equals("back", StringComparison.CurrentCultureIgnoreCase)) // 含国徽面 
            {
                result = IdcardBackParseJson(json);
            }
            else                              // 含照片面
            {
                result = IdcardFrontParseJson(json);
            }

            return result;
        }


        private static string GetIdcardSideCode(string cardSide)
        {
            return IdcardSide.GetValueOrDefault(cardSide, "front");
        }

        /// <summary>
        /// 身份证识别（含国徽面）-解析返回的json数据
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private static string IdcardBackParseJson(string json)
        {
            IdcardBackJson? list = JsonConvert.DeserializeObject<IdcardBackJson>(json);
            if (list == null)
            {
                throw new Exception("无法解析返回结果！");
            }

            string img_status = ImageStatuToStr(list.image_status);
            string risk_type = RiskTypeToStr(list.risk_type);
            if (img_status.Contains("身份证正反面颠倒", StringComparison.CurrentCulture))
            {
                return string.Format(
                     $"识别状态：{img_status}\r\n" +
                     $"身份证类型：{risk_type}\r\n");
            }
            else
            {
                return string.Format(
                    $"识别状态：{img_status}\r\n" +
                    $"身份证类型：{risk_type}\r\n" +
                    $"签发日期：{FormatDate(list.words_result.签发日期.words)}\r\n" +
                    $"失效日期：{FormatDate(list.words_result.失效日期.words)}\r\n" +
                    $"签发机关：{list.words_result.签发机关.words}");
            }
        }

        /// <summary>
        /// 身份证识别（含照片面）-解析返回的json数据
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private static string IdcardFrontParseJson(string json)
        {
            IdcardFrontJson? list = JsonConvert.DeserializeObject<IdcardFrontJson>(json);
            if (list == null || list.idcard_number_type == "-1")
            {
                throw new Exception("无法解析返回结果！");
            }

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
            {
                result += "\r\n编辑软件名称：" + list.edit_tool;
            }

            return result;
        }

        private static string FormatDate(string dateStr)
        {
            // 输入：19890601
            // 输出：1989年06月01日
            StringBuilder sb = new StringBuilder(dateStr);
            sb.Insert(4, "年");
            sb.Insert(7, "月");
            sb.Append('日');
            return sb.ToString();
            // return date.ToString("yyyy年MM月dd日");
        }

        private static string NumberTypeToStr(string numberType)
        {
            return numberType switch
            {
                "-1" => "身份证正面所有字段全为空",
                "0" => "身份证证号识别错误",
                "1" => "身份证证号和性别、出生信息一致",
                "2" => "身份证证号和性别、出生信息都不一致",
                "3" => "身份证证号和出生信息不一致",
                "4" => "身份证证号和性别信息不一致",
                _ => numberType,
            };
        }

        private static string ImageStatuToStr(string image_status)
        {
            return image_status switch
            {
                "normal" => "识别正常",
                "reversed_side" => "身份证正反面颠倒",
                "non_idcard " => "上传的图片中不包含身份证 ",
                "blurred" => " 身份证模糊   ",
                "other_type_card " => " 其他类型证照   ",
                "over_exposure " => " 身份证关键字段反光或过曝  ",
                "over_dark " => " 身份证欠曝（亮度过低） ",
                "unknown " => " 未知状态",
                _ => image_status,
            };
        }

        private static string RiskTypeToStr(string risk_type)
        {
            return risk_type switch
            {
                "normal" => "正常身份证",
                "copy" => "复印件",
                "temporary" => " 临时身份证",
                "screen" => " 翻拍",
                "unknown" => "其他未知情况",
                _ => risk_type,
            };
        }






        /// <summary>
        /// 银行卡识别
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public async Task<string> BankCardAsync(Image img)
        {
            string host = "https://aip.baidubce.com/rest/2.0/ocr/v1/bankcard?access_token=";
            string base64 = WebHelper.ImageToBase64(img);
            string token = await GetAccessTokenAsync();
            string str = "image=" + HttpUtility.UrlEncode(base64) + "&detect_direction=true";
            string result = await WebHelper.RequestAsync(host, token, str);
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
            BankCardJson? list = JsonConvert.DeserializeObject<BankCardJson>(json);
            if (list == null || list.result == null)
            {
                throw new Exception("无法解析返回结果！");
            }

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
            return type switch
            {
                0 => "不能识别",
                1 => "借记卡",
                2 => "贷记卡",
                3 => "准贷记卡",
                4 => "预付费卡",
                _ => "未知类型卡",
            };
        }


        /// <summary>
        /// 公式识别
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public async Task<string> FormulaAsync(Image img)
        {
            string host = "https://aip.baidubce.com/rest/2.0/ocr/v1/formula?access_token=";
            string base64 = WebHelper.ImageToBase64(img);
            string token = await GetAccessTokenAsync();
            string str = "image=" + HttpUtility.UrlEncode(base64) + "&detect_direction=true";
            string result = await WebHelper.RequestAsync(host, token, str);
            return GeneralParseJSON(result);
        }


        /// <summary>
        /// 测试OCRkey是否有效
        /// </summary>
        /// <returns></returns>
        public async static Task OCRKeyTestAsync(string ak, string sk)
        {
            BaiduKey key = new BaiduKey()
            {
                ApiKey = ak,
                SecretKey = sk,
            };
            var baidu = new Baidu(key);
            // 测试文字识别Key只需要测试能否获得AccessToken就可以了
            _ = await baidu.GetAccessTokenAsync();
        }

        public async static Task TranslateKeyTestAsync(string id, string pw)
        {
            BaiduKey key = new BaiduKey()
            {
                AppId = id,
                Password = pw
            };
            var baidu = new Baidu(key);
            // 测试百度翻译Key
            await baidu.TranslateAsync("Test", "en", "zh");
        }

        public async Task<string> OCRActionAsync(BaiduOCRParameter param)
        {
            string text;
            string mode = param.Mode;
            System.Drawing.Image img = param.Image;
            string SelectedLangType = param.LangType;
            string SelectedIdCardSide = param.IdCardSide;
            string downloadPath = param.DownloadedPath;
            try
            {
                // 判断识别类型 通用、高精度、手写、数字
                if (mode.Contains("通用") || mode.Contains("手写") || mode.Contains("数字"))
                {
                    text = await GeneralBasicAsync(mode, img, SelectedLangType);
                }
                else if (mode.Contains("公式"))
                {
                    text = await FormulaAsync(img);
                }
                else if (mode.Contains("身份证"))
                {
                    text = await IdcardAsync(SelectedIdCardSide, img);
                }
                else if (mode.Contains("银行卡"))
                {
                    text = await BankCardAsync(img);
                }
                else if (mode.Contains("表格"))
                {
                    text = await FormOcrRequestAsync(downloadPath, img);
                }
                else
                {
                    text = "不支持的识别类型！";
                }
            }
            catch (Exception ex)
            {
                text = string.Format($"时间：{DateTime.Now}\r\n错误：{ex.Message}\r\n请检查图片是否有效、选择的识别功能是否匹配（如选择身份证识别但导入的图片不是身份证），或检查网络连接是否正确。");
            }
            return text;

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
