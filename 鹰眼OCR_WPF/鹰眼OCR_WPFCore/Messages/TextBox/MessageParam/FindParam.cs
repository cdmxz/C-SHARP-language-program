namespace 鹰眼OCR_WPFCore.Messages.TextBox.MessageParam
{
    public class FindParam
    {
        /// <summary>
        /// 要查找的文本
        /// </summary>
        public required string FindText { get; set; }


        /// <summary>
        /// 大小写敏感
        /// </summary>
        public bool IgnoreCase { get; set; }


        /// <summary>
        /// 全字符匹配
        /// </summary>
        public bool IsWholeWord { get; set; }


        /// <summary>
        /// 是否为向上查找
        /// true: 向上查找
        /// false: 向下查找
        /// </summary>
        public bool IsPrevious { get; set; }


        /// <summary>
        /// 是否为第一次查找
        /// </summary>
        public bool IsFirst { get; set; }
    }
}
