using System.Runtime.InteropServices;

namespace 腾讯会议摸鱼助手.TengXunMeetingHelper.Common
{
    internal class WinApi
    {
        // 激活并显示窗口
        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hwnd);

        // 将 虚拟密钥代码转换为扫描代码
        [DllImport("user32.dll")]
        public extern static uint MapVirtualKey(uint uCode, uint uMapType);

        public const int KEYEVENTF_KEYUP = 0x0002; // 释放按键
        public const int MAPVK_VK_TO_VSC = 0;      // 虚拟密钥代码转换为扫描代码

        // 发送键盘事件
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        // 发送鼠标事件
        [DllImport("user32")]
        public static extern int mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);//模拟鼠标消息

        public const int MOUSEEVENTF_LEFTDOWN = 0x0002;   // 模拟鼠标左键按下      
        public const int MOUSEEVENTF_LEFTUP = 0x0004;     // 模拟鼠标左键抬起 
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008;  // 模拟鼠标右键按下 
        public const int MOUSEEVENTF_RIGHTUP = 0x0010;    // 模拟鼠标右键抬起 
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020; // 模拟鼠标中键按下 
        public const int MOUSEEVENTF_MIDDLEUP = 0x0040;   // 模拟鼠标中键抬起 
        public const int MOUSEEVENTF_WHEEL = 0x0800;      // 模拟鼠标滚轮滚动

        // 设置鼠标位置
        // 逻辑坐标
        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll", EntryPoint = "GetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos([Out()] out Point lpPoint);

        [DllImport("User32.dll", CharSet = CharSet.Auto)] // 屏幕坐标转窗口坐标
        public static extern bool ScreenToClient(IntPtr hWnd, ref Point pt);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ClientToScreen(IntPtr hWnd, ref Point pt); // 窗口坐标转屏幕坐标


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate bool WNDENUMPROC(IntPtr hwnd, int lParam);

        [DllImport("user32.dll", EntryPoint = "EnumWindows")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumWindows(WNDENUMPROC lpEnumFunc, [MarshalAs(UnmanagedType.SysInt)] IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "GetWindowTextW")]
        public static extern int GetWindowTextW([In()] IntPtr hWnd, [Out()][MarshalAs(UnmanagedType.LPWStr)] System.Text.StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", EntryPoint = "IsWindowVisible")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible([In()] IntPtr hWnd);

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWINFO
        {

            /// DWORD->unsigned int
            public uint cbSize;

            /// RECT->tagRECT
            public TagRECT rcWindow;

            /// RECT->tagRECT
            public TagRECT rcClient;

            /// DWORD->unsigned int
            public uint dwStyle;

            /// DWORD->unsigned int
            public uint dwExStyle;

            /// DWORD->unsigned int
            public uint dwWindowStatus;

            /// UINT->unsigned int
            public uint cxWindowBorders;

            /// UINT->unsigned int
            public uint cyWindowBorders;

            /// ATOM->WORD->unsigned short
            public ushort atomWindowType;

            /// WORD->unsigned short
            public ushort wCreatorVersion;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TagRECT
        {
            /// LONG->int
            public int left;

            /// LONG->int
            public int top;

            /// LONG->int
            public int right;

            /// LONG->int
            public int bottom;

            public Rectangle ToRectangle() => new(left, top, right - left, bottom - top);
        }

        [DllImport("user32.dll", EntryPoint = "GetWindowInfo")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowInfo([In()] IntPtr hwnd, ref WINDOWINFO pwi);

        [DllImport("dwmapi.dll", EntryPoint = "DwmGetWindowAttribute")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE dwAttribute, out TagRECT pvAttribute, int cbAttribute);

        public enum DWMWINDOWATTRIBUTE
        {
            DWMWA_NCRENDERING_ENABLED = 1,      // [get] Is non-client rendering enabled/disabled
            DWMWA_NCRENDERING_POLICY,           // [set] DWMNCRENDERINGPOLICY - Non-client rendering policy
            DWMWA_TRANSITIONS_FORCEDISABLED,    // [set] Potentially enable/forcibly disable transitions
            DWMWA_ALLOW_NCPAINT,                // [set] Allow contents rendered in the non-client area to be visible on the DWM-drawn frame.
            DWMWA_CAPTION_BUTTON_BOUNDS,        // [get] Bounds of the caption button area in window-relative space.
            DWMWA_NONCLIENT_RTL_LAYOUT,         // [set] Is non-client content RTL mirrored
            DWMWA_FORCE_ICONIC_REPRESENTATION,  // [set] Force this window to display iconic thumbnails.
            DWMWA_FLIP3D_POLICY,                // [set] Designates how Flip3D will treat the window.
            DWMWA_EXTENDED_FRAME_BOUNDS,        // [get] Gets the extended frame bounds rectangle in screen space
            DWMWA_HAS_ICONIC_BITMAP,            // [set] Indicates an available bitmap when there is no better thumbnail representation.
            DWMWA_DISALLOW_PEEK,                // [set] Don't invoke Peek on the window.
            DWMWA_EXCLUDED_FROM_PEEK,           // [set] LivePreview exclusion information
            DWMWA_CLOAK,                        // [set] Cloak or uncloak the window
            DWMWA_CLOAKED,                      // [get] Gets the cloaked state of the window
            DWMWA_FREEZE_REPRESENTATION,        // [set] BOOL, Force this window to freeze the thumbnail without live update
            DWMWA_PASSIVE_UPDATE_MODE,          // [set] BOOL, Updates the window only when desktop composition runs for other reasons
            DWMWA_LAST
        };


        [DllImport("user32.dll", EntryPoint = "GetClassNameW")]
        public static extern int GetClassNameW([In()] IntPtr hWnd, [Out()][MarshalAs(UnmanagedType.LPWStr)] System.Text.StringBuilder lpClassName, int nMaxCount);

    }

}
