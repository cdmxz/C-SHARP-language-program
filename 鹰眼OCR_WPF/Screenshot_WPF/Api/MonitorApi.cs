using System.Runtime.InteropServices;

namespace Screenshot_WPF.Api
{
    class MonitorApi
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MONITORINFOEX
        {
            public int cbSize = Marshal.SizeOf(typeof(MONITORINFOEX));
            public RECT rcMonitor = new RECT();
            public RECT rcWork = new RECT();
            public int dwFlags = 0;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public char[] szDevice = new char[32];
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetMonitorInfo(IntPtr hMonitor, [In, Out] MONITORINFOEX lpmi);

        [DllImport("user32.dll")]
        public static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumDelegate lpfnEnum, IntPtr dwData);

        public delegate bool MonitorEnumDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData);


        public static bool MonitorEnumProc(IntPtr hMonitor, IntPtr hdcMonitor, ref RECT lprcMonitor, IntPtr dwData)
        {
            MONITORINFOEX mi = new MONITORINFOEX();
            GetMonitorInfo(hMonitor, mi);
            monitors?.Add(new MonitorInfo
            {
                Left = mi.rcMonitor.left,
                Top = mi.rcMonitor.top,
                Right = mi.rcMonitor.right,
                Bottom = mi.rcMonitor.bottom,
                Width = Math.Abs(mi.rcMonitor.right - mi.rcMonitor.left),
                Height = Math.Abs(mi.rcMonitor.bottom - mi.rcMonitor.top)
            });
            return true;
        }



        private static List<MonitorInfo>? monitors;

        /// <summary>
        /// 获取所有屏幕信息
        /// </summary>
        public static MonitorInfo[] GetAllScreens()
        {
            monitors = new();
            EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, MonitorEnumProc, IntPtr.Zero);
            return monitors.ToArray();
        }
    }
}
