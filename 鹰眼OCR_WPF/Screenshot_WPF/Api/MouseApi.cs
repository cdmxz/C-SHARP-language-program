
using System.Runtime.InteropServices;
using System.Windows;
using Point = System.Windows.Point;

namespace Screenshot_WPF.Api
{
    internal class MouseApi
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public POINT(System.Windows.Point p)
            {
                X = (int)p.X;
                Y = (int)p.Y;
            }
            public int X;
            public int Y;
        }

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(ref POINT lpPoint);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);


        /// <summary>
        /// 获取当前鼠标光标所在的屏幕（避免多块屏幕截图出现问题）   
        /// </summary>
        /// <returns></returns>
        public static Rect GetCurrentScreenRect()
        {
            var cursorPos = GetCursorPos();
            MonitorInfo[] screens = MonitorApi.GetAllScreens();
            foreach (var screen in screens)
            {
                Rect area = new(screen.Left, screen.Top, screen.Width, screen.Height);
                if (area.Contains(cursorPos))
                {
                    // 找到鼠标所在屏幕
                    return area;
                }
            }
            // 未找到则返回主屏幕
            var scr = screens[0];
            return new Rect(scr.Left, scr.Top, scr.Width, scr.Height);
        }

        /// <summary>
        /// 获取当前鼠标光标位置
        /// </summary>
        /// <returns></returns>
        public static Point GetCursorPos()
        {
            POINT p = new();
            GetCursorPos(ref p);
            return new Point(p.X, p.Y);
        }

        public static void SetCursor(int x, int y)
        {
            SetCursorPos(x, y);
        }

    }
}
