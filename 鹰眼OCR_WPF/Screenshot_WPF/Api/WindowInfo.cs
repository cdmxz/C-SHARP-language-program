using System.Windows;

namespace Screenshot_WPF.Api
{
    /// <summary>
    /// 窗口信息
    /// </summary>
    public class WindowInfo
    {
        public WindowInfo(nint handle, Rect rect)
        {
            Handle = handle;
            Rect = rect;
        }

        public WindowInfo(string title, nint handle, Rect rect)
        {
            Title = title;
            Handle = handle;
            Rect = rect;
        }

        public string Title { get; set; }
        public IntPtr Handle { get; set; }
        public Rect Rect { get; set; }
        public List<WindowInfo> Childrens { get; internal set; }
    }
}
