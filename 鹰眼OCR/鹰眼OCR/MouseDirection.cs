namespace 鹰眼OCR
{
    /// <summary>
    /// 鼠标拖动方向
    /// </summary>
    public enum MouseDirection
    {
        /// <summary>
        /// 不拖动
        /// </summary>
        None,
        /// <summary>
        /// 水平方向拖动，只改变窗体的宽度
        /// </summary>
        Herizontal,
        /// <summary>
        /// 垂直方向拖动，只改变窗体的高度
        /// </summary>
        Vertical,
        /// <summary>
        /// 倾斜拖动，同时改变窗体的宽度和高度
        /// </summary>
        Declining,
    }
}
