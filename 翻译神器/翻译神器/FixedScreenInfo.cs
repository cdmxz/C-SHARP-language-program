using System.Drawing;

namespace 翻译神器
{
    /// <summary>
    /// 固定截图信息
    /// </summary>
    public struct FixedScreenInfo
    {
        public FixedScreenInfo()
        {
            WindowName = string.Empty;
            WindowClass = string.Empty;
            FixedRect = new Rectangle();
        }
        public FixedScreenInfo(string windowName, string windowClass)
        {
            WindowName = windowName;
            WindowClass = windowClass;
            FixedRect = new Rectangle();
        }
        public FixedScreenInfo(string windowName, string windowClass, Rectangle fixedRect)
        {
            WindowName = windowName;
            WindowClass = windowClass;
            FixedRect = fixedRect;
        }

        /// <summary>
        /// 窗口标题
        /// </summary>
        public string WindowName { get; set; }
        /// <summary>
        /// 窗口类名
        /// </summary>
        public string WindowClass { get; set; }
        /// <summary>
        /// 固定截图-截图矩形
        /// </summary>
        public Rectangle FixedRect { get; set; }

        /// <summary>
        /// 截图大小
        /// </summary>
        public Size RectSize
        {
            get
            {
                return FixedRect.Size;
            }
            set
            {
                FixedRect = new Rectangle(FixedRect.Location, value);
            }
        }

        /// <summary>
        /// 截图坐标
        /// </summary>
        public Point RectLocation
        {
            get
            {
                return FixedRect.Location;
            }
            set
            {
                FixedRect = new Rectangle(value, FixedRect.Size);
            }
        }
    }
}
