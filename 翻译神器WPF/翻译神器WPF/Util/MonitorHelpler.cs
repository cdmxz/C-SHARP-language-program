using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace 翻译神器WPF.Util
{
    internal class MonitorHelpler
    {
        public static Size GetPrimaryScreenSize()
        {
            var w = (int)SystemParameters.PrimaryScreenWidth;
            var h = (int)SystemParameters.PrimaryScreenHeight;
            return new Size(w, h);
        }

        // 获取当前鼠标所在显示器的大小（像素）
        public static Size GetCurrentMonitorSize()
        {
            if (!GetCursorPos(out POINT pt))
            {
                // 回退：使用主屏（虚拟屏）
                var w = (int)SystemParameters.PrimaryScreenWidth;
                var h = (int)SystemParameters.PrimaryScreenHeight;
                return new Size(w, h);
            }

            IntPtr hMonitor = MonitorFromPoint(pt, MONITOR_DEFAULTTONEAREST);
            MONITORINFO mi = new MONITORINFO();
            mi.cbSize = Marshal.SizeOf(typeof(MONITORINFO));
            if (GetMonitorInfo(hMonitor, ref mi))
            {
                int width = mi.rcMonitor.right - mi.rcMonitor.left;
                int height = mi.rcMonitor.bottom - mi.rcMonitor.top;
                return new Size(width, height);
            }

            //失败时回退主屏尺寸
            var pw = (int)SystemParameters.PrimaryScreenWidth;
            var ph = (int)SystemParameters.PrimaryScreenHeight;
            return new Size(pw, ph);
        }

        private const uint MONITOR_DEFAULTTONEAREST = 0x00000002;

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        private static extern IntPtr MonitorFromPoint(POINT pt, uint dwFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct MONITORINFO
        {
            public int cbSize;
            public RECT rcMonitor;
            public RECT rcWork;
            public uint dwFlags;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
    }
}
