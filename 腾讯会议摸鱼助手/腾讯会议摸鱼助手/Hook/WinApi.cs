using System.Runtime.InteropServices;

namespace 腾讯会议摸鱼助手.Hook
{
    class WinApi
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

        [StructLayout(LayoutKind.Sequential)]
        public struct TagMOUSEHOOKSTRUCT
        {
            public Point pt;
            public int mouseData;
            public int flags;
            public int time;
            public int dwExtraInfo;
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
    }
}
