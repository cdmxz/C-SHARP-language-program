using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace 鹰眼OCR.Util
{
    /// <summary>
    /// 封装WindowsApi
    /// </summary>
    class WinApi
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

        [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
        public static extern int GetWindowLong(HandleRef hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowLong(HandleRef hWnd, int nIndex, int dwNewLong);
        public const int WS_SYSMENU = 0x00080000; // 系统菜单
        public const int WS_MINIMIZEBOX = 0x00020000;  // 最大最小化按钮

        // 判断按键是否按下
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(int vKey);

        public const int VK_MENU = 0x12;
        public const int VK_CONTROL = 0x17;
        
        // 发送键盘消息
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        // 将 虚拟密钥代码转换为扫描代码

        [DllImport("user32.dll")]
        public extern static uint MapVirtualKey(uint uCode, uint uMapType);

        public const int KEYEVENTF_KEYUP = 0x0002; // 释放按键
        public const int MAPVK_VK_TO_VSC = 0;      // 虚拟密钥代码转换为扫描代码

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

        // 判断键盘任一按键是否按下
        public static bool IsKeyDown(Keys key) => GetAsyncKeyState((int)key) != 0;

        /// <summary>
        /// 发送按键
        /// </summary>
        /// <param name="key"></param>
        public static void SendKey(Keys key)
        {
            // 模拟按下按键
            keybd_event((byte)key, (byte)MapVirtualKey((uint)key, MAPVK_VK_TO_VSC), 0, 0);
            Thread.Sleep(50);
            keybd_event((byte)key, (byte)MapVirtualKey((uint)key, MAPVK_VK_TO_VSC), KEYEVENTF_KEYUP, 0);
        }
    }
}
