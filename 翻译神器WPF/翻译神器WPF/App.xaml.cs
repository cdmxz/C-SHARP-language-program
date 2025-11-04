using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Screenshot_WPF;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows;
using System.Windows.Threading;
using 翻译神器WPF.Common;
using 翻译神器WPF.Extensions;
using 翻译神器WPF.Models;
using 翻译神器WPF.Service;
using 翻译神器WPF.Update;
using 翻译神器WPF.Util;
using 翻译神器WPF.ViewModels;
using 翻译神器WPF.Views;

namespace 翻译神器WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly JsonSerializerOptions options = new()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) //允许所有Unicode字符
        };


        /// <summary>
        /// 设置 Windows 11 主题支持
        /// </summary>
        [ModuleInitializer]
        internal static void Init()
        {
            if (SystemUtil.IsWindows11OrGreater())
            {
                AppContext.SetSwitch("Switch.System.Windows.Appearance.EnableFluentThemeWindowBackdrop", true);
            }
        }

        private readonly static IHost _host = Host.CreateDefaultBuilder()
        .ConfigureServices((context, services) =>
        {
            ConfigureServices(services);
        })
        .Build();

        private static void ConfigureServices(IServiceCollection services)
        {
            // window
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainWindowViewModel>();

            services.AddSingleton<TextWindow>();
            services.AddSingleton<TextWindowViewModel>();

            services.AddSingleton<ScreenWindow>();

            // view
            services.AddSingletonEx<SettingsPage>("SettingsPage");
            services.AddSingleton<SettingsPageViewModel>();

            services.AddSingletonEx<HomePage>("HomePage");

            services.AddSingletonEx<KeyPage>("KeyPage");
            services.AddSingleton<KeyPageViewModel>();

            services.AddSingletonEx<AboutPage>("AboutPage");
            services.AddSingleton<AboutPageViewModel>();

            services.AddSingletonEx<UpdatePage>("UpdatePage");
            services.AddSingleton<UpdatePageViewModel>();

            // service
            services.AddSingleton<IWindowService, WindowService>();
            services.AddSingleton<ITranslateApiService, TranslateApiService>();

            // other
            services.AddSingleton<UpdateService>();

            //读配置文件
            services.AddSingleton<ConfigData>(sp =>
            {
                if (File.Exists(ConfigData.ConfigFile))
                {
                    var text = File.ReadAllText(ConfigData.ConfigFile);
                    var cd = JsonSerializer.Deserialize<ConfigData>(text);
                    if (cd != null)
                    {
                        return cd;
                    }
                }
                return new ConfigData()
                {
                    SettingsData = new(),
                    KeyData = new()
                    {
                        BaiduKey = new TranslationApi.Common.BaiduKey(),
                        YoudaoKey = new TranslationApi.Common.YoudaoKey(),
                        TengxunKey = new TranslationApi.Common.TengxunKey()
                    },
                };
            });
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            // 检查是否已有实例
            if (ProcessHelper.IsRun(out Process? process) && process != null)
            {
                string title = _host.Services.GetRequiredService<MainWindowViewModel>().Title;
                var windowHandle = WinApi.FindWindow(null, title);

                if (windowHandle != IntPtr.Zero)
                {
                    // 已存在实例，发送消息
                    _ = WinApi.PostMessage(windowHandle, WindowsMsg.ShowWindowMsg, IntPtr.Zero, IntPtr.Zero);
                    App.Current.Shutdown();
                }
                //else
                //{
                //    MessageBox.Show("windowHandle为null");
                //}
            }
            else
            {
                await _host!.StartAsync();
                App.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                App.Current.DispatcherUnhandledException += AppOnDispatcherUnhandledException;
                //var window = _host.Services.GetRequiredService<TextWindow>();
                var window = _host.Services.GetRequiredService<MainWindow>();

                //让 OnMainWindowClose 生效
                App.Current.MainWindow = window;
                // 根据系统版本设置背景色
                if (!SystemUtil.IsWindows11OrGreater())
                {
                    window.Background = System.Windows.Media.Brushes.White;
                }
                window.Show();
                base.OnStartup(e);

                //处理启动参数：如果包含 "auto"，启动最小化（并会根据窗口逻辑隐藏到托盘）
                //if (e?.Args != null && e.Args.Any(a => string.Equals(a, AutoStartConstant.StartParam, StringComparison.OrdinalIgnoreCase)))
                //{
                //    window.Hide();
                //}
                if (!File.Exists(ConfigData.ConfigFile))
                {
                    //首次运行，显示欢迎
                    NotifierHelper.Show(Notification.Wpf.NotificationType.Information, "首次使用请先设置翻译Apikey和热键");
                }
                else
                    window.Hide();
            }
        }

        private void AppOnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            //MessageBox.Show("程序异常：" + e.Exception.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            //Console.WriteLine(e.Exception.Message);
            File.AppendAllText("翻译神器异常日志.log", $"{DateTime.Now:G}  {e.Exception}\r\n");
            App.Current.Shutdown();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            // 窗口关闭，保存配置文件
            var configData = _host.Services.GetRequiredService<ConfigData>();
            var cd = JsonSerializer.Serialize(configData, options);
            try
            {
                File.WriteAllText(ConfigData.ConfigFile, cd);
            }
            catch (Exception)
            {
            }
            await _host!.StopAsync();
            base.OnExit(e);
        }


    }

}
