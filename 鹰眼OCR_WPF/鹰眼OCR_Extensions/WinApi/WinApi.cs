using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace 鹰眼OCR_Extensions.WinApi
{
    /// <summary>
    /// 封装WindowsApi
    /// </summary>
    public class WinApi
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow")]// 查找窗口句柄
        public extern static nint FindWindow(string? lpClassName, string? lpWindowName);


        [DllImport("user32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(IntPtr hwnd, int Msg, IntPtr wParam, IntPtr lParam);


        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(nint hWnd, int id, uint fsModifiers, Keys vk);// 注册热键

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(nint hWnd, int id);// 释放注册的的热键

      
    }
}
