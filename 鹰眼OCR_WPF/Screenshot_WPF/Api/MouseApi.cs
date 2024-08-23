using System.Drawing;
using System.Runtime.InteropServices;

namespace Screenshot_WPF.Api
{
    internal class MouseApi
    {
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref Point lpPoint);

        /// <summary>
        /// 获取当前鼠标光标所在的屏幕（避免多块屏幕截图出现问题）   
        /// </summary>
        /// <returns></returns>
        public static Rectangle GetCurrentScreenRectangle()
        {
            var cursorPos = GetCursorPos();
            MonitorInfo[] screens = MonitorApi.GetAllScreens();
            foreach (var screen in screens)
            {
                Rectangle area = new Rectangle(screen.Left, screen.Top, screen.Width, screen.Height);
                if (cursorPos.X >= screen.Left && cursorPos.X <= area.X + area.Width)
                {
                    return area;
                }
            }
            var scr = screens[0];
            return new Rectangle(new Point(scr.Left, scr.Top), new Size(new Point(scr.Right, scr.Bottom))); ;
        }

        /// <summary>
        /// 获取当前鼠标光标位置
        /// </summary>
        /// <returns></returns>
        public static Point GetCursorPos()
        {
            Point cursorPos = new();
            GetCursorPos(ref cursorPos);
            return cursorPos;
        }

    }
}
