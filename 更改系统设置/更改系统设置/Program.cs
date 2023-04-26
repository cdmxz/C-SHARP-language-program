using Microsoft.Win32;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace 更改系统设置
{
    internal class Program
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr LoadLibrary(string lpLibFileName);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool FreeLibrary(IntPtr hLibModule);
        static void Main(string[] args)
        {
            string file = LoadResource();
            IntPtr NSudoDevilModeModuleHandle = LoadLibrary(file);
            // 隐藏任务视图按钮 
            SetRegValue(Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", true), "ShowTaskViewButton", 0, RegistryValueKind.DWord);
            // 关闭UAC
            SetRegValue(Registry.LocalMachine.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", true), "EnableLUA", 0, RegistryValueKind.DWord);
            // 显示文件扩展名
            SetRegValue(Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", true), "HideFileExt", 0, RegistryValueKind.DWord);
            // 关闭WD
            //SetRegValue(Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Services\\SecurityHealthService", true), "Start", 4, RegistryValueKind.DWord);
            // 被占满时合并任务栏
            SetRegValue(Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", true), "TaskbarGlomLevel", 1, RegistryValueKind.DWord);
            // 隐藏人脉
            SetRegValue(Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\\People", true), "PeopleBand", 0, RegistryValueKind.DWord);
            // 搜索框只显示图标
            SetRegValue(Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Search", true), "SearchboxTaskbarMode", 1, RegistryValueKind.DWord);
            // 隐藏任务视图
            SetRegValue(Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", true), "ShowTaskViewButton", 0, RegistryValueKind.DWord);
            // 桌面显示此电脑
            SetRegValue(Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel", true), "{20D04FE0-3AEA-1069-A2D8-08002B30309D}", 0, RegistryValueKind.DWord);
            // 桌面显示控制面板
            SetRegValue(Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\HideDesktopIcons\\NewStartPanel", true), "{5399E694-6CE5-4D6C-8FCE-1D8870FDCBA0}", 0, RegistryValueKind.DWord);
            // 任务栏上显示所有通知区域图标
            SetRegValue(Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer", true), "EnableAutoTray", 0, RegistryValueKind.DWord);
            // 时间精确到秒
            SetRegValue(Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", true), "ShowSecondsInSystemClock", 1, RegistryValueKind.DWord);
            // 关闭打开程序的安全警告
            SetRegValue(Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\Associations", true), "ModRiskFileTypes", ".bat;.exe;.reg;.vbs;.chm;.msi;.js;.cmd", RegistryValueKind.String);
            // 不在开始菜单显示建议
            SetRegValue(Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\ContentDeliveryManager", true), "SubscribedContent-338388Enabled", 0, RegistryValueKind.DWord);
            SetRegValue(Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\ContentDeliveryManager", true), "SubscribedContent-338389Enabled", 1, RegistryValueKind.DWord);
            // 关闭锁屏时的Windows聚焦推广
            SetRegValue(Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\ContentDeliveryManager", true), "RotatingLockScreenOverlayEnabled", 0, RegistryValueKind.DWord);
            // 关闭“使用Windows时获取技巧和建议”
            SetRegValue(Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\ContentDeliveryManager", true), "SoftLandingEnabled", 0, RegistryValueKind.DWord);
            // 关闭“突出显示新安装的程序”
            SetRegValue(Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced", true), "Start_NotifyNewApps", 0, RegistryValueKind.DWord);
            // 登录界面默认打开小键盘
            SetRegValue(Registry.Users.CreateSubKey(".DEFAULT\\Control Panel\\Keyboard", true), "InitialKeyboardIndicators", "2", RegistryValueKind.String);
            // shift右击时显示在比处打开命令窗口
            if (NSudoDevilModeModuleHandle != IntPtr.Zero)
            {// 防止权限不足造成错误
                SetRegValue(Registry.ClassesRoot.CreateSubKey("Directory\\Background\\shell\\cmd", true), "ShowBasedOnVelocityId", 0x00639bc8, RegistryValueKind.DWord);
                DelRegKey(Registry.ClassesRoot.CreateSubKey("Directory\\Background\\shell\\cmd", true), "HideBasedOnVelocityId");
            }
            else
                Console.WriteLine("加载DLL失败");
            // 禁止自动播放
            SetRegValue(Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\AutoplayHandlers", true), "DisableAutoplay", 1, RegistryValueKind.DWord);
            // 关闭多个选项卡时不发出警告
            SetRegValue(Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\TabbedBrowsing", true), "WarnOnClose", 0, RegistryValueKind.DWord);
            // 关闭建议的网站  
            SetRegValue(Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Suggested Sites", true), "Enabled", 0, RegistryValueKind.DWord);
            // 跳过IE首次运行自定义设置
            SetRegValue(Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Main", true), "RunOnceHasShown", 1, RegistryValueKind.DWord);
            SetRegValue(Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Main", true), "RunOnceComplete", 1, RegistryValueKind.DWord);
            SetRegValue(Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Main", true), "DisableFirstRunCustomize", 1, RegistryValueKind.DWord);
            // 关闭IE自动更新
            SetRegValue(Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Main", true), "NoUpdateCheck", 1, RegistryValueKind.DWord);
            // 启用自动换行
            SetRegValue(Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Notepad", true), "fWrap", 1, RegistryValueKind.DWord);
            // 始终显示状态栏
            SetRegValue(Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Notepad", true), "StatusBar", 1, RegistryValueKind.DWord);
            // 禁用客户体验改善计划
            SetRegValue(Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\SQMClient\\Windows", true), "CEIPEnable", 0, RegistryValueKind.DWord);
            // Windows Media Player 不显示首次使用对话框
            SetRegValue(Registry.LocalMachine.CreateSubKey("SOFTWARE\\Policies\\Microsoft\\WindowsMediaPlayer", true), "GroupPrivacyAcceptance", 1, RegistryValueKind.DWord);
            // 删除WD自启动
            DelRegKey(Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true), "SecurityHealth");
            DelRegKey(Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true), "WindowsDefender");
            // 隐藏任务栏资讯和兴趣按钮
            SetRegValue(Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Feeds", true), "ShellFeedsTaskbarViewMode", 2, RegistryValueKind.DWord);
            // 创建快捷方式时不显示“快捷方式”
            byte[] arr = new byte[] { 0, 0, 0, 0 };
            SetRegValue(Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer", true), "link", arr, RegistryValueKind.Binary);
            SetRegValue(Registry.Users.CreateSubKey(".DEFAULT\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer", true), "link", arr, RegistryValueKind.Binary);
            FreeLibrary(NSudoDevilModeModuleHandle);
            File.Delete(file);
            OutPut("\r\n还原系统设置", ConsoleColor.Green, true);
        }

        static string LoadResource()
        {
            string file = "NSudoDM.dll";
            Assembly asm = Assembly.GetExecutingAssembly();
            using (Stream sm = asm.GetManifestResourceStream("更改系统设置.NSudoDM.dll"))
            {
                byte[] bs = new byte[sm.Length];
                sm.Read(bs, 0, (int)sm.Length);
                File.WriteAllBytes(file, bs);
            }
            return file;
        }

        static void SetRegValue(RegistryKey key, string name, object value, RegistryValueKind valueKind)
        {
            if (key == null)
                return;
            key.SetValue(name, value, valueKind);
            key.Close();
        }

        static void DelRegKey(RegistryKey key, string name)
        {
            key.DeleteValue(name, false);
            key.Close();
        }

        /// <summary>
        /// 用指定颜色输出文本
        /// </summary>
        /// <param name="text">要输出的文本</param>
        /// <param name="color">输出文本时使用的颜色</param>
        /// <param name="addLinefeed">是否在末尾添加换行符</param>
        static void OutPut(string text, ConsoleColor color, bool addLinefeed)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            if (addLinefeed)
                Console.WriteLine();
            Console.ForegroundColor = currentColor;
        }
    }
}
