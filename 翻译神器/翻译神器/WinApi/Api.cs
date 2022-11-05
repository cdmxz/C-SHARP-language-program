using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace 翻译神器.WinApi
{
    class Api
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct TagRECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        // 判断按键是否按下
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(int vKey);

        public const int VK_MENU = 0x12;
        public const int VK_CONTROL = 0x17;

        // 将 虚拟密钥代码转换为扫描代码

        [DllImport("user32.dll")]
        public extern static uint MapVirtualKey(uint uCode, uint uMapType);

        public const int KEYEVENTF_KEYUP = 0x0002; // 释放按键
        public const int MAPVK_VK_TO_VSC = 0;      // 虚拟密钥代码转换为扫描代码


        // 发送键盘消息
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        // 激活并显示窗口
        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hwnd);

        // 查找窗口句柄
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(string? lpClassName, string? lpWindowName);

        // 屏幕坐标转窗口坐标
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ClientToScreen(IntPtr hWnd, ref POINT pt);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public POINT(int x, int y)
            {
                X = x;
                Y = y;
            }
            public int X;
            public int Y;
        }

        // 指定窗口的显示状态
        [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
        public const int SW_SHOWNORMAL = 1;// 激活并显示窗口。如果窗口最小化或最大化，系统将窗口恢复到其原始大小和位置。

        // 注册热键
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, Keys vk);

        // 释放注册的的热键
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        // 发送线程消息
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PostThreadMessage(int threadId, uint msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 获取窗口句柄，并判断是否有效，无效则抛出异常
        /// </summary>
        /// <param name="windowTitle">窗口标题</param>
        /// <param name="windowClass">窗口类名</param>
        /// <returns></returns>
        public static IntPtr FindWindowHandle(string windowTitle, string windowClass)
        {
            string? title = string.IsNullOrEmpty(windowTitle) ? null : windowTitle;
            string? className = string.IsNullOrEmpty(windowClass) ? null : windowClass;
            if (title is null && className is null)
                throw new ArgumentException("请设置 窗口标题或窗口类名！");
            IntPtr hwnd = FindWindow(title, className);
            if (IntPtr.Zero == hwnd)
                throw new Exception("找不到对应的窗口句柄！");
            return hwnd;
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

        public static int WAIT_MILLISECONDS = 500;


        /// <summary>
        /// 发送按键
        /// </summary>
        /// <param name="key"></param>
        public static void SendKey(Keys key)
        {
            // 模拟按下按键
            Api.keybd_event((byte)key, (byte)Api.MapVirtualKey((uint)key, Api.MAPVK_VK_TO_VSC), 0, 0);
            Thread.Sleep(50);
            Api.keybd_event((byte)key, (byte)Api.MapVirtualKey((uint)key, Api.MAPVK_VK_TO_VSC), Api.KEYEVENTF_KEYUP, 0);
        }
    }
}
