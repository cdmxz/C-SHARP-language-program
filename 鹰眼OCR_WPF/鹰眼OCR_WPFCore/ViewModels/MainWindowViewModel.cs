using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Text.Json;
using System.Windows.Controls;
using System.Windows.Interop;
using 鹰眼OCR_WPF;
using 鹰眼OCR_WPF.Views.Windows;
using 鹰眼OCR_WPFCore.Constants;
using 鹰眼OCR_WPFCore.Extensions;
using 鹰眼OCR_WPFCore.Helper;
using 鹰眼OCR_WPFCore.Models;
using 鹰眼OCR_WPFCore.Views;

namespace 鹰眼OCR_WPFCore.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private UserControl _currentView;

        [ObservableProperty]
        private string _title;

        private readonly IServiceProvider _services;
        private readonly HotKeyViewData _hotKeyViewData;
        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true
        };


        // 上一个页面名称
        private string _lastViewName = "";

        // mainwindow句柄
        private IntPtr _handle;


        public MainWindowViewModel(IServiceProvider serviceProvider)
        {
            _services = serviceProvider;
            CurrentView = _services.GetRequiredService<HomeView>();
            var configData = _services.GetRequiredService<ConfigData>();
            _hotKeyViewData = configData.HotKeyViewData;

            string ver = System.Windows.Forms.Application.ProductVersion;
            Title = "鹰眼OCR V" + ver.Remove(ver.Length - 2);

        }

        /// <summary>
        /// 导航命令
        /// </summary>
        /// <param name="viewName"></param>
        [RelayCommand]
        public void Navigate(string viewName)
        {
            try
            {
                if (viewName.Equals("HotKeyView", StringComparison.OrdinalIgnoreCase))
                {
                    // 导航到热键设置页面，卸载热键
                    UnInstallAllHotkeys();
                }
                else if (_lastViewName.Equals("HotKeyView", StringComparison.OrdinalIgnoreCase))
                {// 导航到其他页面，如果上一个页面是热键设置页面，重新安装热键
                    InstallAllHotkeys();
                }

            }
            catch (Exception e)
            {
                NotifierHelper.ShowError(e);
            }
            _lastViewName = viewName;
            CurrentView = (UserControl)_services.GetRequiredKeyedServiceEx(viewName);
        }


        /// <summary>
        /// 窗口状态变更
        /// </summary>
        /// <param name="state"></param>
        [RelayCommand]
        public void WindowStateChanged(string state)
        {
            if (state == "ContentRendered")
            {
                // 窗口渲染完成，安装热键
                try
                {
                    _handle = _services.GetRequiredService<MainWindow>().Handle;
                    InstallAllHotkeys();
                }
                catch (Exception e)
                {
                    NotifierHelper.ShowError(e);
                }
            }
            else if (state == "Closing")
            {
                // 窗口关闭，保存配置文件
                var configData = _services.GetRequiredService<ConfigData>();
                var cd = JsonSerializer.Serialize(configData, options);
                try
                {
                    File.WriteAllText(ConfigData.ConfigFile, cd);
                }
                catch (Exception)
                {
                }
                UnInstallAllHotkeys();
            }
        }

        // 安装所有热键
        private void InstallAllHotkeys()
        {
            if (_hotKeyViewData == null)
            {
                return;
            }
            if (_hotKeyViewData.HaveDuplicateKeys)
            {
                throw new Exception("热键设置有重复，请重新设置");
            }
            HotKeyHelper.RegHotKey(_handle, _hotKeyViewData.ScreenshotHotKey, (int)HotKeyId.Screenshot);
            HotKeyHelper.RegHotKey(_handle, _hotKeyViewData.PhotoHotKey, (int)HotKeyId.Photograph);
            HotKeyHelper.RegHotKey(_handle, _hotKeyViewData.RecordingHotKey, (int)HotKeyId.Record);
            HotKeyHelper.RegHotKey(_handle, _hotKeyViewData.FixedHotKey, (int)HotKeyId.FixedScreen);
        }

        // 卸载所有热键

        private void UnInstallAllHotkeys()
        {
            HotKeyHelper.UnregisterHotKey(_handle, (int)HotKeyId.Screenshot);
            HotKeyHelper.UnregisterHotKey(_handle, (int)HotKeyId.Photograph);
            HotKeyHelper.UnregisterHotKey(_handle, (int)HotKeyId.Record);
            HotKeyHelper.UnregisterHotKey(_handle, (int)HotKeyId.FixedScreen);
        }


    }
}
