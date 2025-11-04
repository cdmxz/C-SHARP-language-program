using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows;
using System.Windows.Threading;
using 鹰眼OCR_Extensions.Audio;
using 鹰眼OCR_Extensions.OCR;
using 鹰眼OCR_Extensions.PDF;
using 鹰眼OCR_Extensions.WinApi;
using 鹰眼OCR_WPF.Constants;
using 鹰眼OCR_WPF.Extensions;
using 鹰眼OCR_WPF.Helper;
using 鹰眼OCR_WPF.Models;
using 鹰眼OCR_WPF.Service;
using 鹰眼OCR_WPF.ViewModels;
using 鹰眼OCR_WPF.Views;
using 鹰眼OCR_WPF.Views.Windows;


// ****************************************************************************************
// 立项时间：2024-08-07
// ****************************************************************************************

namespace 鹰眼OCR_WPF
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private readonly JsonSerializerOptions options = new()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) // 允许所有Unicode字符
        };

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

            services.AddSingleton<CameraWindow>();
            services.AddSingleton<CameraWindowViewModel>();

            services.AddSingleton<SearchWindow>();
            services.AddSingleton<SearchWindowViewModel>();

            services.AddSingleton<QrCodeWindow>();
            services.AddSingleton<QrCodeWindowViewModel>();

            services.AddSingleton<RecordWindow>();
            services.AddSingleton<RecordWindowViewModel>();

            // view
            services.AddSingletonEx<AboutView>("AboutView");

            services.AddSingletonEx<HomeView>("HomeView");
            services.AddSingleton<HomeViewModel>();

            services.AddSingletonEx<HotKeyView>("HotKeyView");
            services.AddSingleton<HotKeyViewModel>();

            services.AddSingletonEx<OptionView>("OptionView");
            services.AddSingleton<OptionViewModel>();

            services.AddSingletonEx<UpdateView>("UpdateView");
            services.AddSingleton<UpdateViewModel>();

            // service
            services.AddSingleton<IWindowService, WindowService>();
            services.AddSingleton<ILanguageService, LanguageService>();

            // other
            services.AddTransient<PdfToImage>();
            services.AddTransient<PlayAudio>();
            services.AddTransient<Baidu>();

            services.AddSingleton<Screenshot_WPF.ScreenWindow>();
            // 读配置文件
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
                    OptionViewData = new OptionViewData(),
                    HotKeyViewData = new HotKeyViewData(),
                    HomeViewData = new HomeViewData() { IsEmpty = true }
                };
            });
        }


        protected override async void OnStartup(StartupEventArgs e)
        {

            if (ProcessHelper.IsRun(out Process? process) && process != null)
            {
                // 如果当前应用在运行，则发送线程消息到主线程，让主线程显示窗口
                var handle = process.MainWindowHandle;
                _ = WinApi.PostMessage(handle, WindowsMsg.ShowWindowMsg, IntPtr.Zero, IntPtr.Zero);
                base.Shutdown();
            }
            else
            {
                App.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                await _host!.StartAsync();
                App.Current.DispatcherUnhandledException += AppOnDispatcherUnhandledException;
                var window = _host.Services.GetRequiredService<MainWindow>();
                App.Current.MainWindow = window;
                window.Show();
                base.OnStartup(e);
            }
        }

        private void AppOnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("程序异常：" + e.Exception.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            Console.WriteLine(e.Exception.Message);
            e.Handled = true;
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
