using System.Text.Json.Serialization;

namespace 翻译神器WPF.TranslationApi.Common
{
    #region 文字识别Key
    /// <summary>
    /// 百度翻译key
    /// </summary>
    public class BaiduKey
    {
        public BaiduKey()
        {
        }

        public string AppId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        [JsonIgnore]
        public bool IsEmpty
        {
            get
            {
                return string.IsNullOrEmpty(AppId) && string.IsNullOrEmpty(Password);
            }
        }
    }

    /// <summary>
    /// 有道文字识别key
    /// </summary>
    public class YoudaoKey
    {
        public YoudaoKey()
        {
        }

        public string AppKey { get; set; } = string.Empty;
        public string AppSecret { get; set; } = string.Empty;
        [JsonIgnore]
        public bool IsEmpty
        {
            get
            {
                return (string.IsNullOrEmpty(AppKey) && string.IsNullOrEmpty(AppSecret));
            }
        }
    }

    /// <summary>
    /// 腾讯key
    /// </summary>
    public class TengxunKey
    {
        public TengxunKey()
        {
        }

        public string SecretId { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        [JsonIgnore]
        public bool IsEmpty
        {
            get
            {
                return string.IsNullOrEmpty(SecretId) && string.IsNullOrEmpty(SecretKey);
            }
        }
    }
    #endregion

}
