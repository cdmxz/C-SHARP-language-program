namespace 鹰眼OCR_WPFCore.Messages.TextBox.MessageParam
{
    internal class ReplaceParam : FindParam
    {
        /// <summary>
        /// 要替换的值
        /// </summary>
        public required string ReplaceText { get; set; }


        /// <summary>
        /// 是否全部替换
        /// </summary>
        public bool IsReplaceAll { get; set; }
    }
}
