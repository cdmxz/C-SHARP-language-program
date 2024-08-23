using System.Drawing;

namespace Screenshot_WPF.Api
{
    /// <summary>
    /// 窗口信息
    /// </summary>
    public class WindowInfo
    {
        public WindowInfo(nint handle, Rectangle rect)
        {
            Handle = handle;
            Rect = rect;
        }
        public string Title { get; set; }
        public IntPtr Handle { get; set; }
        public Rectangle Rect { get; set; }
    }
}
