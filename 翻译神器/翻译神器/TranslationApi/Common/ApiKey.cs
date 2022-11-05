namespace 翻译神器.TranslationApi.Common
{
    #region 文字识别Key
    /// <summary>
    /// 百度翻译key
    /// </summary>
    public struct BaiduKey
    {
        public static string AppId { get; set; } = string.Empty;
        public static string Password { get; set; } = string.Empty;

        public static bool IsEmpty
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
    public struct YoudaoKey
    {
        public static string AppKey { get; set; } = string.Empty;
        public static string AppSecret { get; set; } = string.Empty;
        public static bool IsEmpty
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
    public struct TengxunKey
    {
        public static string SecretId { get; set; } = string.Empty;
        public static string SecretKey { get; set; } = string.Empty;
        public static bool IsEmpty
        {
            get
            {
                return string.IsNullOrEmpty(SecretId) && string.IsNullOrEmpty(SecretKey);
            }
        }
    }
    #endregion

}
