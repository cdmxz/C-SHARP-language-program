using Microsoft.Win32;

namespace 腾讯会议摸鱼助手.Utils
{
    internal class Reg
    {
        // 注册表路径
        static readonly string SubKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

        /// <summary>
        /// 添加自启动
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static void AddAutoStart(string name, string? value)
        {
            using RegistryKey? registryKey = Registry.CurrentUser.OpenSubKey(SubKey, true);
            if (registryKey == null)
                throw new Exception("无法打开注册表");
            if (value == null)
                throw new ArgumentNullException(nameof(value) + "应不为空");
            registryKey.SetValue(name, value);
        }

        /// <summary>
        /// 删除自启动
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static void DelAutoStart(string name)
        {
            using RegistryKey? registryKey = Registry.CurrentUser.OpenSubKey(SubKey, true);
            if (registryKey == null)
                throw new Exception("无法打开注册表");
            registryKey.DeleteValue(name);
        }

        /// <summary>
        /// 查询是否添加了自启动
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool IsAddAutoStart(string name)
        {
            using RegistryKey? registryKey = Registry.CurrentUser.OpenSubKey(SubKey, true);
            if (registryKey == null)
                throw new Exception("无法打开注册表");
            return registryKey.GetValue(name) != null;
        }
    }
}
