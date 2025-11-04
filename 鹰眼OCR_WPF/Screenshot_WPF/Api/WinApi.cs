using System.Runtime.InteropServices;
using System.Text;
using System.Windows;



namespace Screenshot_WPF.Api
{
    /// <summary>
    /// 封装的WindowsApi
    /// </summary>
    public static class WinApi
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct TagRECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
        [DllImport("user32.dll", EntryPoint = "GetWindowRect")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect([In()] nint hWnd, [Out()] out TagRECT lpRect);

        [DllImport("dwmapi.dll", EntryPoint = "DwmGetWindowAttribute")]
        private static extern int DwmGetWindowAttribute(nint hwnd, DWMWINDOWATTRIBUTE dwAttribute, out TagRECT pvAttribute, int cbAttribute);

        private enum DWMWINDOWATTRIBUTE
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

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        /// <summary>
        /// 获取指定子窗口的父窗口句柄
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns>如果窗口是一个子窗口返回父窗口句柄，如果是父窗口返回NULL</returns>
        [DllImport("user32.dll", EntryPoint = "GetParent")]
        private static extern nint GetParent([In()] nint hWnd);


        // 用于 EnumWindows 回调
        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsIconic(IntPtr hWnd);


        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        // 正确的 EnumChildWindows P/Invoke，使用专门的回调签名
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumChildWindows(IntPtr hWndParent, EnumChildProc lpEnumFunc, IntPtr lParam);

        private delegate bool EnumChildProc(IntPtr hWnd, IntPtr lParam);


        /// <summary>
        /// 窗口是否为父窗口
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        public static bool IsParentWindow(nint hWnd)
        {
            return GetParent(hWnd) == nint.Zero;
        }


        /// <summary>
        /// 通过窗体句柄获取窗体矩形
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        private static Rect GetWindowRectByHandle(nint hWnd)
        {
            GetWindowRect(hWnd, out TagRECT tagRect);
            return tagRect.ToRect();
        }

        /// <summary>
        /// 通过窗体句柄获取窗体大小（不包括阴影部分）
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        private static Rect GetWindowSizeByHandle(nint hWnd)
        {
            if (hWnd == nint.Zero)
            {
                return new Rect();
            }

            _ = DwmGetWindowAttribute(hWnd, DWMWINDOWATTRIBUTE.DWMWA_EXTENDED_FRAME_BOUNDS, out TagRECT rect, Marshal.SizeOf(typeof(TagRECT)));
            return rect.ToRect();
        }


        /// <summary>
        /// 获取窗口大小
        /// 如果窗口是父窗口则返回窗口实际大小（不包括阴影部分）
        /// </summary>
        /// <param name="handle">窗口句柄</param>
        /// <returns></returns>
        public static Rect GetWindowRect(nint handle)
        {
            Rect rect;
            if (IsParentWindow(handle))
            {
                rect = GetWindowSizeByHandle(handle);
            }
            else
            {
                rect = GetWindowRectByHandle(handle);
            }
            return rect;
        }



        /// <summary>
        /// 获取窗口标题
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        private static string GetWindowTitle(IntPtr hWnd)
        {
            int length = GetWindowTextLength(hWnd);
            if (length == 0)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder(length + 1);
            GetWindowText(hWnd, sb, sb.Capacity);
            return sb.ToString();
        }

        /// <summary>
        /// 获取桌面上所有的可见窗口及其可见控件
        /// </summary>
        /// <returns></returns>
        public static WindowInfo[] GetVisibleWindows(IntPtr exclude)
        {
            List<WindowInfo> windows = new List<WindowInfo>();

            // 枚举顶层窗口的回调函数
            EnumWindowsProc enumWindowsCallback = (hWnd, lParam) =>
            {
                if (hWnd != exclude && IsWindowVisible(hWnd) && !IsIconic(hWnd))
                {
                    string title = GetWindowTitle(hWnd);
                    Rect rect = GetWindowRect(hWnd);
                    if (rect.Width > 0 && rect.Height > 0)
                    {
                        var windowInfo = new WindowInfo(title, hWnd, rect);

                        // 枚举该窗口下的所有可见子控件，并添加到 windowInfo.Children
                        var children = new List<WindowInfo>();
                        EnumChildProc enumChildCallback = (childHwnd, childParam) =>
                        {
                            if (IsWindowVisible(childHwnd) && !IsIconic(childHwnd))
                            {
                                Rect childRect = GetWindowRect(childHwnd);
                                if (childRect.Width > 0 && childRect.Height > 0)
                                {
                                    var childInfo = new WindowInfo(GetWindowTitle(childHwnd), childHwnd, childRect);
                                    children.Add(childInfo);
                                }
                            }
                            return true; // 继续枚举
                        };

                        EnumChildWindows(hWnd, enumChildCallback, IntPtr.Zero);

                        windowInfo.Childrens = RemoveDuplicateByRect(children);
                        windows.Add(windowInfo);
                    }
                }
                return true; // 继续枚举
            };

            // 枚举所有顶层窗口
            EnumWindows(enumWindowsCallback, IntPtr.Zero);

            // 去重：按 x,y,width,height 完全相等保留第一个
            var unique = RemoveDuplicateByRect(windows);
            return unique.ToArray();
        }

        private static List<WindowInfo> RemoveDuplicateByRect(List<WindowInfo> list)
        {
            var seen = new HashSet<(int X, int Y, int W, int H)>();
            var result = new List<WindowInfo>(list.Count);
            foreach (var w in list)
            {
                var r = w.Rect;
                // 避免精度问题
                var key = ((int)r.X, (int)r.Y, (int)r.Width, (int)r.Height);
                if (seen.Add(key))
                {
                    result.Add(w);
                }
            }
            return result;
        }


        /// <summary>
        /// 扩展方法
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static Rect ToRect(this TagRECT rect)
        {
            return new(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);
        }
    }

}
