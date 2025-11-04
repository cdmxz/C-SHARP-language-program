using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using System.Windows.Input;
using 鹰眼OCR_Common.Constants;
using 鹰眼OCR_WPF.Constants;
using 鹰眼OCR_WPF.Extensions;
using 鹰眼OCR_WPF.Helper;
using 鹰眼OCR_WPF.Manager;
using 鹰眼OCR_WPF.Messages;
using 鹰眼OCR_WPF.Models;
using 鹰眼OCR_WPF.Views;
using 鹰眼OCR_WPF.Views.Windows;

namespace 鹰眼OCR_WPF.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private UserControl _currentView;

        [ObservableProperty]
        private string _title;

        private readonly IServiceProvider _services;
        private readonly HotKeyViewData _hotKeyViewData;



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

            // 根据配置文件设置当前语言
            var langItem = Languages.LanguageList[configData.OptionViewData.SelectedLanguageIndex];
            var cultureInfo = new System.Globalization.CultureInfo(langItem.CultureName);
            LanguageManager.Instance.ChangeLanguage(cultureInfo);

            // 左上角标题
            string ver = System.Windows.Forms.Application.ProductVersion;
            Title = $"{LanguageManager.Instance["AppName"]} V{ver[..^2]}";
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
                UnInstallAllHotkeys();
            }
        }

        /// <summary>
        /// 在窗口中按下快捷键
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        [RelayCommand]
        private void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && (e.Key == Key.H || e.Key == Key.F))
            {
                WeakReferenceMessenger.Default.Send(new DictionaryMessage(new Dictionary<string, object>() { { MessageTokens.MainWindowViewModel, MessageTokens.ShowSearchWindow } }), MessageTokens.HomeViewModel);
                e.Handled = true;
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
