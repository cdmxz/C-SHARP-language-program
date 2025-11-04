namespace 翻译神器WPF.Util
{
    public class SystemUtil
    {
        // 判断当前版本是否为Windows 11或更高版本
        public static bool IsWindows11OrGreater()
        {
            // Windows 11的版本号为10.0.22000.0
            Version windows11Version = new Version(10, 0, 22000, 0);
            Version currentVersion = Environment.OSVersion.Version;
            return currentVersion >= windows11Version;
        }
    }
}
