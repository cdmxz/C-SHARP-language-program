using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace 动作录制
{
    class Api
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int HOOKPROC(int nCode, int wParam, IntPtr lParam);
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
        public const int WM_SYSKEYDOWN = 0x104;
        public const int WM_SYSKEYUP = 0x105;
        public const int WH_KEYBOARD_LL = 13;
        public const int WH_MOUSE_LL = 14;

        [StructLayout(LayoutKind.Sequential)]
        public struct TagKBDLLHOOKSTRUCT
        {
            public uint vkCode;
            public uint scanCode;
            public uint flags;
            public uint time;
            public uint dwExtraInfo;
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowsHookExW")]
        public static extern IntPtr SetWindowsHookExW(int idHook, HOOKPROC lpfn, [In()] IntPtr hmod, uint dwThreadId);
        [DllImport("user32.dll", EntryPoint = "UnhookWindowsHookEx")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx([In()] IntPtr hhk);

        [DllImport("user32.dll", EntryPoint = "CallNextHookEx")]
        public static extern int CallNextHookEx([In()] IntPtr hhk, int nCode, int wParam, IntPtr lParam);

        [DllImport("kernel32.dll", EntryPoint = "GetModuleHandleW")]
        public static extern IntPtr GetModuleHandleW([In()][MarshalAs(UnmanagedType.LPWStr)] string lpModuleName);


        [DllImport("user32.dll")]
        public extern static uint MapVirtualKey(uint uCode, uint uMapType);

        public const int KEYEVENTF_KEYUP = 0x0002; // 释放按键
        public const int MAPVK_VK_TO_VSC = 0;      // 虚拟密钥代码转换为扫描代码

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);


        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, Keys vk);// 注册热键

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);                         // 释放注册的的热键


        [StructLayout(LayoutKind.Sequential)]
        public struct TagMOUSEHOOKSTRUCT
        {
            public POINT pt;
            public int mouseData;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }
        public const int WM_MOUSEWHEEL = 0x020A;
        public const int WM_MOUSEMOVE = 0x200;
        public const int WM_LBUTTONDOWN = 0x201;
        public const int WM_RBUTTONDOWN = 0x204;
        public const int WM_MBUTTONDOWN = 0x207;
        public const int WM_LBUTTONUP = 0x202;
        public const int WM_RBUTTONUP = 0x205;
        public const int WM_MBUTTONUP = 0x208;
        public const int WM_LBUTTONDBLCLK = 0x203;
        public const int WM_RBUTTONDBLCLK = 0x206;
        public const int WM_MBUTTONDBLCLK = 0x209;

        [DllImport("user32")]
        public static extern int mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);//模拟鼠标消息

        public const int MOUSEEVENTF_LEFTDOWN = 0x0002;   //模拟鼠标左键按下      
        public const int MOUSEEVENTF_LEFTUP = 0x0004;     //模拟鼠标左键抬起 
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008;  //模拟鼠标右键按下 
        public const int MOUSEEVENTF_RIGHTUP = 0x0010;    //模拟鼠标右键抬起 
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020; //模拟鼠标中键按下 
        public const int MOUSEEVENTF_MIDDLEUP = 0x0040;   //模拟鼠标中键抬起 
        public const int MOUSEEVENTF_WHEEL = 0x0800;      //模拟鼠标滚轮滚动

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        public static extern bool SetCursorPos(int X, int Y);
    }
}
