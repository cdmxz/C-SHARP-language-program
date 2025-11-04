using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace 鹰眼OCR_WPF.Helper
{
    internal class ClipboardHelper
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool EmptyClipboard();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool CloseClipboard();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GlobalAlloc(uint uFlags, UIntPtr dwBytes);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GlobalLock(IntPtr hMem);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GlobalUnlock(IntPtr hMem);

        private const uint CF_TEXT = 1;
        private const uint GMEM_MOVEABLE = 0x0002;
        private const uint CF_UNICODETEXT = 13; // Unicode文本格式

      
        /// <summary>
        /// 设置剪切板文本
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool SetClipboardText(string text)
        {
            int len = (text.Length + 1) * 2; // 每个字符占两个字节，加上一个空终止符

            if (OpenClipboard(IntPtr.Zero))
            {
                EmptyClipboard();
                // 分配全局内存, 长度为字符串长度*2+2
                IntPtr hMem = GlobalAlloc(GMEM_MOVEABLE, (UIntPtr)len);
                if (hMem != IntPtr.Zero)
                {
                    // 将内存区域锁定
                    IntPtr pMem = GlobalLock(hMem);
                    if (pMem != IntPtr.Zero)
                    {
                        // 将字符串拷贝到内存区域
                        Marshal.Copy(text.ToCharArray(), 0, pMem, text.Length);
                        Marshal.WriteInt16(pMem, text.Length * 2, 0); // Null-terminate the string
                        // 解锁内存区域
                        GlobalUnlock(hMem);
                        if (SetClipboardData(CF_UNICODETEXT, hMem) != IntPtr.Zero)
                        {
                            CloseClipboard();
                            return true;
                        }
                    }
                }
                CloseClipboard();
            }
            return false;
        }

        /// <summary>
        /// 清除剪切板中的文本格式
        /// </summary>
        public static void ClearTextFormat()
        {
            IDataObject data = Clipboard.GetDataObject();
            if (data.GetDataPresent(DataFormats.UnicodeText))
            {
                var str = (string)data.GetData(DataFormats.UnicodeText);
                SetClipboardText(str);
            }
        }
    }
}
