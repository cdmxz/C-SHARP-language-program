using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Screenshot_WPF;
using 鹰眼OCR_Common.Constants;
using 鹰眼OCR_Common.Messages;
using 鹰眼OCR_WPF.Views.Windows;
using 鹰眼OCR_WPF.Models;
using 鹰眼OCR_WPF.Service;

namespace 鹰眼OCR_WPF.ViewModels
{
    public partial class HotKeyViewModel : ObservableObject
    {
        public HotKeyViewModel(IServiceProvider service)
        {
            ConfigData configData = service.GetRequiredService<ConfigData>();
            configData.HotKeyViewData ??= new HotKeyViewData();
            // _hotKeyViewData没放入进容器，手动new下
            _hotKeyViewData = configData.HotKeyViewData;

            _windowService = service.GetRequiredService<IWindowService>();

            // 注册截图窗体消息，接收传来的截图
            WeakReferenceMessenger.Default.Register<ScreenshotMessage, string>(this, MessageTokens.ScreenWindow, (obj, para) =>
            {
                var val = para.Value;
                var rect = val.CaptureRect;
                var _hwnd = val.DestHandle;
                _hotKeyViewData.Rect = rect; // 保存截图坐标高宽
            });
        }

        private readonly IWindowService _windowService;

        [ObservableProperty]
        private HotKeyViewData _hotKeyViewData;

        /// <summary>
        /// 固定截图
        /// </summary>
        [RelayCommand]
        private void Execute()
        {
            IntPtr? _hwnd = null;
            _windowService.Hide<MainWindow>();
            IntPtr parent = _windowService.Instance<MainWindow>().Handle;
            // 截图，返回的截图通过WeakReferenceMessenger发送给HomeView
            _windowService.Instance<ScreenWindow>().Show(parent, _hwnd);
            _windowService.Show<MainWindow>();
        }
    }
}
