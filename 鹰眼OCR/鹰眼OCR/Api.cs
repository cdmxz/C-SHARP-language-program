using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace 鹰眼OCR
{
    class Api
    {
        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hwnd);// 将窗口显示到前方


        [DllImport("user32.dll", EntryPoint = "FindWindow")]// 查找窗口句柄
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);


        [DllImport("User32.dll", CharSet = CharSet.Auto)] // 屏幕坐标转窗口坐标
        public static extern bool ScreenToClient(IntPtr hWnd, ref POINT pt);


        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ClientToScreen(IntPtr hWnd, ref POINT pt); // 窗口坐标转屏幕坐标


        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
            public int X;
            public int Y;
        }


        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, Keys vk);// 注册热键


        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);// 释放注册的的热键

        /// <summary>
        /// 获取窗口句柄，并判断是否有效，无效则抛出异常
        /// </summary>
        /// <param name="windowName"></param>
        /// <param name="windowClass"></param>
        /// <returns></returns>
        public static IntPtr FindWindowHandle(string windowName, string windowClass)
        {
            IntPtr hwnd;
            if (!string.IsNullOrEmpty(windowName))
                hwnd = FindWindow(null, windowName);
            else if (!string.IsNullOrEmpty(windowClass))
                hwnd = FindWindow(windowClass, null);
            else
                throw new Exception("请设置窗口标题或窗口类名！");
            if (IntPtr.Zero == hwnd)
                throw new Exception("找不到对应的窗口句柄！");
            return hwnd;
        }


        [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
        public static extern int GetWindowLong(HandleRef hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowLong(HandleRef hWnd, int nIndex, int dwNewLong);
        public const int WS_SYSMENU = 0x00080000; // 系统菜单
        public const int WS_MINIMIZEBOX = 0x00020000;  // 最大最小化按钮
    }
}
