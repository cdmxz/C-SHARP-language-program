using System;
using System.Drawing;

using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace ScreenShot
{
    /// <summary>
    /// 封装的WindowsApi
    /// </summary>
    public static class WinApi
    {
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
        [DllImport("user32.dll", EntryPoint = "ClientToScreen")]
        private static extern bool ClientToScreen(IntPtr hWnd, ref POINT pt);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern bool ScreenToClient(IntPtr hWnd, ref POINT pt);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public POINT(Point p)
            {
                X = p.X;
                Y = p.Y;
            }
            public int X;
            public int Y;
        }

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
        private static extern bool GetWindowRect([In()] IntPtr hWnd, [Out()] out TagRECT lpRect);

        [DllImport("user32.dll")]
        private static extern IntPtr GetDesktopWindow();// 获取桌面句柄
        [DllImport("user32.dll")]
        private static extern IntPtr ChildWindowFromPointEx(IntPtr pHwnd, Point pt, uint uFlgs);// 在父窗体中查找子窗体
        private const int CWP_SKIPDISABLED = 0x2;   // 忽略不可用窗体
        private const int CWP_SKIPINVISIBL = 0x1;   // 忽略隐藏的窗体
        private const int CWP_All = 0x0;            // 一个都不忽略


        [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
        private static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
        public const int SW_SHOW = 5;// 在窗口原来的位置以原来的尺寸激活和显示窗口
        public const int SW_SHOWNORMAL = 1;

        [DllImport("dwmapi.dll", EntryPoint = "DwmGetWindowAttribute")]
        private static extern int DwmGetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE dwAttribute, out TagRECT pvAttribute, int cbAttribute);

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

        [DllImport("user32.dll", EntryPoint = "GetWindowLongW")]
        public static extern int GetWindowLongW([In()] IntPtr hWnd, int nIndex);
        const int GWL_STYLE = 16;
        const int WS_VISIBLE = 0x10000000;


        [DllImport("user32", EntryPoint = "SetWindowPos")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndlnsertAfter, int X, int Y, int cx, int cy, int flags);
        public static int SWP_NOSIZE = 1;
        public static int SWP_NOMOVE = 2;

        public static Point ScreenToClient(IntPtr hWnd, Point p)
        {
            POINT pt = new(p);
            ScreenToClient(hWnd, ref pt);// 屏幕坐标转为客户端窗口坐标
            return pt.ToPoint();
        }

        public static Point ClientToScreen(IntPtr hWnd, Point p)
        {
            POINT pt = new(p);
            ClientToScreen(hWnd, ref pt);// 屏幕坐标转为客户端窗口坐标
            return pt.ToPoint();
        }

        /// <summary>
        /// 获取指定子窗口的父窗口句柄
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns>如果窗口是一个子窗口返回父窗口句柄，如果是父窗口返回NULL</returns>
        [DllImport("user32.dll", EntryPoint = "GetParent")]
        private static extern IntPtr GetParent([In()] IntPtr hWnd);

        /// <summary>
        /// 根据鼠标所在的坐标获取窗口句柄
        /// </summary>
        /// <param name="mousePoint">鼠标坐标</param>
        /// <param name="form">当前窗体</param>
        /// <returns></returns>
        public static IntPtr GetWindowHandleByPos(Point mousePoint, Form form)
        {
            form.Enabled = false;// 禁用本窗体，防止阻挡其它窗体
            // 在桌面根据鼠标坐标查找窗口（此时被找到的窗口作为父窗口）
            IntPtr parentHwnd = ChildWindowFromPointEx(GetDesktopWindow(), mousePoint, CWP_SKIPDISABLED | CWP_SKIPINVISIBL);
            POINT lp = new(mousePoint);
            IntPtr childHwnd = parentHwnd;
            // 在父窗口中继续查找子窗口句柄，直到为null
            while (true)
            {
                ScreenToClient(childHwnd, ref lp);  // 把屏幕坐标转换为窗口内部坐标
                // 在父窗口中继续查找子窗口句柄
                childHwnd = ChildWindowFromPointEx(childHwnd, new Point(lp.X, lp.Y), 0x0004);
                if (childHwnd == IntPtr.Zero || childHwnd == parentHwnd)
                    break;
                // 将找到的子窗口作为父窗口，继续查找子窗口中的子窗口
                parentHwnd = childHwnd;
                // 更新鼠标坐标
                lp = new POINT(Control.MousePosition);
            }
            form.Enabled = true;
            return parentHwnd;
        }

        /// <summary>
        /// 窗口是否为父窗口
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        public static bool IsParentWindow(IntPtr hWnd) => GetParent(hWnd) == IntPtr.Zero;

        /// <summary>
        /// 通过窗体句柄获取窗体矩形
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        public static Rectangle GetWindowRectByHandle(IntPtr hWnd)
        {
            GetWindowRect(hWnd, out TagRECT tagRect);
            return tagRect.ToRectangle();
        }
        // 激活并显示窗口
        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hwnd);

        /// <summary>
        /// 通过窗体句柄获取窗体大小（不包括阴影部分）
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        public static Rectangle GetWindowSizeByHandle(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero)
                return new Rectangle();
            _ = DwmGetWindowAttribute(hWnd, DWMWINDOWATTRIBUTE.DWMWA_EXTENDED_FRAME_BOUNDS, out TagRECT rect, Marshal.SizeOf(typeof(TagRECT)));
            return rect.ToRectangle();
        }

        /// <summary>
        /// 获取窗口大小
        /// 如果窗口是父窗口则返回窗口实际大小（不包括阴影部分）
        /// </summary>
        /// <param name="handle">窗口句柄</param>
        /// <returns></returns>
        public static Rectangle GetWindowRect(IntPtr handle)
        {
            Rectangle rect;
            if (IsParentWindow(handle))
                rect = GetWindowSizeByHandle(handle);
            else
                rect = GetWindowRectByHandle(handle);
            return rect;
        }

        /// <summary>
        /// 把窗口显示到最前方并等待millisec毫秒
        /// </summary>
        /// <param name="hwnd">窗口句柄</param>
        /// <param name="millisec">等待毫秒数</param>
        public static void SetForegroundWindowAndWait(IntPtr hwnd, int millisec)
        {
            SetForegroundWindow(hwnd);
            Thread.Sleep(millisec);
        }


        /// <summary>
        /// 顶置窗口
        /// </summary>
        /// <param name="hwnd"></param>
        public static void TopWindow(IntPtr hwnd)
        {
            SetWindowPos(hwnd, new IntPtr(-1), 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE);
        }

        public static string GetClassNamebyHandle(IntPtr hWnd)
        {
            StringBuilder sb = new(260);
            _ = GetClassName(hWnd, sb, sb.Capacity);
            return sb.ToString();
        }

        /// <summary>
        /// 扩展方法
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static Rectangle ToRectangle(this TagRECT rect) => new(rect.left, rect.top, rect.right - rect.left, rect.bottom - rect.top);

        /// <summary>
        /// 扩展方法
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public static Point ToPoint(this POINT pt) => new(pt.X, pt.Y);
    }
}