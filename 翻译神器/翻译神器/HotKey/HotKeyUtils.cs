using System;
using 翻译神器.WinApi;

namespace 翻译神器.HotKey
{
    /// <summary>
    /// 热键ID
    /// </summary>
    public enum HotKeyIds
    {
        /// <summary>
        /// 截图翻译
        /// </summary>
        SCREEN_TRAN = 1000,
        /// <summary>
        /// 显示翻译窗口
        /// </summary>
        SHOW_TRANFORM,
        /// <summary>
        /// 固定区域翻译
        /// </summary>
        FIXED_TRAN
    }


    public class HotKeyUtils
    {
        // 注册所有的热键
        public static void RegAllHotKeys(IntPtr handle, HotKeyInfo[] keyInfos)
        {
            foreach (HotKeyInfo keyInfo in keyInfos)
            {
                // 注册热键
                if (!Api.RegisterHotKey(handle, keyInfo.Id, keyInfo.Modifiers, keyInfo.Key))
                {   // 如果注册失败
                    throw new Exception($"注册热键：{keyInfo}失败！");
                }
            }
        }

        /// <summary>
        /// 卸载所有的热键
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="keyInfos"></param>
        public static void UnAllRegHotKeys(IntPtr handle, HotKeyInfo[]? keyInfos)
        {
            if (keyInfos == null)
                return;
            foreach (HotKeyInfo keyInfo in keyInfos)
                Api.UnregisterHotKey(handle, keyInfo.Id);

        }

        /// <summary>
        /// 获取修饰键键值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static uint GetKeyVal(string key)
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
