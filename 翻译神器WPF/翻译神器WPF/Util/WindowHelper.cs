using System.Diagnostics;
using System.Runtime.InteropServices;

namespace 翻译神器WPF.Util
{
    public static class WindowHelper
    {
        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        public static IntPtr GetMainWindowHandle(Process process)
        {
            IntPtr mainWindowHandle = process.MainWindowHandle;

            // 如果 MainWindowHandle 为0，尝试枚举所有窗口
            if (mainWindowHandle == IntPtr.Zero)
            {
                return FindWindowByProcessId(process.Id);
            }

            return mainWindowHandle;
        }

        private static IntPtr FindWindowByProcessId(int processId)
        {
            IntPtr result = IntPtr.Zero;

            EnumWindows((window, param) =>
            {
                GetWindowThreadProcessId(window, out uint windowProcessId);

                if (windowProcessId == processId && IsMainWindow(window))
                {
                    result = window;
                    return false; // 停止枚举
                }

                return true; // 继续枚举
            }, IntPtr.Zero);

            return result;
        }

        private static bool IsMainWindow(IntPtr handle)
        {
            return GetWindow(handle, GW_OWNER) == IntPtr.Zero && IsWindowVisible(handle);
        }

        // Win32 API 声明
        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        private const uint GW_OWNER = 4;
    }
}
