using Microsoft.Win32;

namespace 自动进入钉钉直播.Helper
{
    class Reg
    {
        /// <summary>
        /// 注册表启动子键
        /// </summary>
        private const string REG_START_SUBKEY = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

        /// <summary>
        /// 通过注册表、桌面、手动选择路径的方式
        /// 获取钉钉安装路径
        /// </summary>
        /// <returns>返回钉钉安装路径</returns>
        public static string? GetDingPath()
        {
            string? path = GetDingPathFromRegist(); // 从注册表获取钉钉路径
            path ??= GetDingPathFromDesktop();// 从桌面获取钉钉路径
            path ??= GetDingPathFromDialog();// 手动选择钉钉路径
            return path;
        }

        // 手动选择钉钉路径
        private static string? GetDingPathFromDialog()
        {
            DialogResult result = MessageBox.Show("无法从注册表和桌面获取钉钉路径，\n请手动选择“DingtalkLauncher.exe”路径（在钉钉安装目录里）。", Application.ProductName, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Cancel)
                return null;
            using OpenFileDialog dialog = new();
            dialog.Filter = "DingTalk.exe (*.exe)| *.exe";
            dialog.Title = "请选择“DingTalk.exe”路径（在钉钉安装目录里）";
            dialog.DefaultExt = "exe";
            return dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : null;
        }

        // 从注册表获取钉钉路径
        private static string? GetDingPathFromRegist()
        {
            // 64位系统注册表路径
            string subKey = "SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\钉钉";
            if (!Environment.Is64BitOperatingSystem)// 如果是32位系统
                subKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\钉钉";// 32位系统钉钉在注册表的路径
            // 从注册表获取钉钉路径
            RegistryKey? key = Registry.LocalMachine.OpenSubKey(subKey, false);// 打开注册表
            if (key != null)// 如果打开成功
            {
                string? path = key.GetValue("UninstallString")?.ToString();
                if (path != null)
                {
                    string dingDingPath = Path.Combine(Path.GetDirectoryName(path) + "\\", "DingtalkLauncher.exe");
                    return File.Exists(dingDingPath) ? dingDingPath : null;
                }
            }
            return null;
        }

        // 从桌面获取钉钉快捷方式
        private static string? GetDingPathFromDesktop()
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory);
            string dingDingPath = $"{desktop}\\钉钉.lnk";
            return File.Exists(dingDingPath) ? dingDingPath : null;
        }

        /// <summary>
        /// 修改注册表添加自启动
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <param name="KeyName">要添加的键值</param>
        /// <returns></returns>
        public static void AddStart(string Path, string KeyName)
        {
            // 打开注册表
            using RegistryKey key = Registry.LocalMachine.CreateSubKey(REG_START_SUBKEY);
            key.SetValue(KeyName, Path, RegistryValueKind.String);  // 设置值
        }

        /// <summary>
        /// 修改注册表删除自启动
        /// </summary>
        /// <param name="KeyName">要删除的键值</param>
        /// <returns></returns>
        public static void DelStart(string KeyName)
        {
            // 打开注册表
            using RegistryKey key = Registry.LocalMachine.CreateSubKey(REG_START_SUBKEY);
            key.DeleteValue(KeyName);   // 删除值
        }
    }
}
