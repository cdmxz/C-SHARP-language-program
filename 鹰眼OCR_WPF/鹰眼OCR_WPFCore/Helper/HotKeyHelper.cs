using System.Runtime.InteropServices;
using System.Windows.Forms;



namespace 鹰眼OCR_WPFCore.Helper
{
    partial class HotKeyHelper
    {
        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, Keys vk);// 注册热键


        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool UnregisterHotKey(IntPtr hWnd, int id);// 释放注册的的热键


        /// <summary>
        /// 注册热键
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="hotKey"></param>
        /// <param name="id"></param>
        /// <exception cref="Exception"></exception>
        public static void RegHotKey(IntPtr handle, string hotKey, int id)
        {
            // 如果热键为空
            if (string.IsNullOrEmpty(hotKey))
            {
                return;
            }
            // 分解热键
            SplitHotKey(hotKey, out Keys key, out uint fun1, out uint fun2);
            if (!RegisterHotKey(handle, id, fun1 | fun2, key)) // 注册热键
            {
                throw new Exception($"注册热键{hotKey}失败，可能被其他程序占用了");
            }
        }

        /// <summary>
        /// 分割热键
        /// 将ctrl+alt+a分割为ctrl、alt、a
        /// </summary>
        /// <param name="hotKey"></param>
        /// <param name="key"></param>
        /// <param name="fun1"></param>
        /// <param name="fun2"></param>
        private static void SplitHotKey(string hotKey, out Keys key, out uint fun1, out uint fun2)
        {
            fun1 = fun2 = 0U;
            string[] arr = hotKey.Split([',', '+']).Reverse().ToArray();
            // 前一个键为单键（a-z）
            key = Enum.Parse<Keys>(arr[0], true);
            if (arr.Length == 2)// 两个键（后一个键为功能键（ctrl、alt...））
            {
                fun1 = GetKeyVal(arr[1]);
            }

            if (arr.Length == 3) // 三个键（后两个键为功能键（ctrl、alt...））
            {
                fun2 = GetKeyVal(arr[2]);
            }
        }

        // 获取功能键键值
        private static uint GetKeyVal(string key)
        {
            return key.Trim().ToLower() switch
            {
                "alt" => 0x0001,
                "control" => 0x0002,
                "shift" => 0x0004,
                "win" => 0x0008,
                _ => 0,
            };
        }
    }
}
