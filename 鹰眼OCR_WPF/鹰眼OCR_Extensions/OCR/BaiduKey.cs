namespace 鹰眼OCR_Extensions.OCR
{
    // 百度文字识别和百度翻译key
    public class BaiduKey
    {
        public string ApiKey { get; set; }
        public string SecretKey { get; set; }
        public string AppId { get; set; }
        public string Password { get; set; }

        public BaiduKey()
        {

        }


        /// <summary>
        /// OCR key是否为空
        /// </summary>
        public bool IsOcrEmpty
        {
            get
            {
                return (string.IsNullOrWhiteSpace(ApiKey) || string.IsNullOrWhiteSpace(SecretKey));
            }
        }

        /// <summary>
        /// 翻译Key是否为空
        /// </summary>
        public bool IsTranslateEmpty
        {
            get
            {
                return string.IsNullOrWhiteSpace(AppId) || string.IsNullOrWhiteSpace(Password);
            }
        }
    }
}
