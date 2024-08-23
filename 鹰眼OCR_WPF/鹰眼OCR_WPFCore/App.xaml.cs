using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;
using 鹰眼OCR_Extensions.Audio;
using 鹰眼OCR_Extensions.OCR;
using 鹰眼OCR_Extensions.PDF;
using 鹰眼OCR_Extensions.WinApi;
using 鹰眼OCR_WPF.Views.Windows;
using 鹰眼OCR_WPFCore.Constants;
using 鹰眼OCR_WPFCore.Extensions;
using 鹰眼OCR_WPFCore.Helper;
using 鹰眼OCR_WPFCore.Models;
using 鹰眼OCR_WPFCore.Service;
using 鹰眼OCR_WPFCore.ViewModels;
using 鹰眼OCR_WPFCore.Views;
using 鹰眼OCR_WPFCore.Views.Windows;


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

            services.AddSingleton<FindWindow>();
            services.AddSingleton<FindWindowViewModel>();

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

            // other
            services.AddTransient<PdfToImage>();
            services.AddTransient<PlayAudio>();
            services.AddTransient<Baidu>();

            services.AddSingleton<Screenshot_WPF.ScreenWindow>();
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
                    HotKeyViewData = new HotKeyViewData()
                };
            });
        }



        protected override async void OnStartup(StartupEventArgs e)
        {
            if (ProcessHelper.IsRun(out Process? process) && process != null)
            {
                // 如果当前应用在运行，则发送线程消息到主线程，让主线程显示窗口
                var handle = process.MainWindowHandle;
                WinApi.PostMessage(handle, WindowsMsg.ShowWindowMsg, IntPtr.Zero, IntPtr.Zero);
                base.Shutdown();
            }
            else
            {
                App.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                await _host!.StartAsync();

                var window = _host.Services.GetRequiredService<MainWindow>();
                window.Show();
                base.OnStartup(e);
            }
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host!.StopAsync();
            base.OnExit(e);
        }

    }
}
