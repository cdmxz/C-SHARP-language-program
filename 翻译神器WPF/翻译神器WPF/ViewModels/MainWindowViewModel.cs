using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Screenshot_WPF;
using Screenshot_WPF.Helper;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using 翻译神器WPF.Common;
using 翻译神器WPF.Extensions;
using 翻译神器WPF.HotKey;
using 翻译神器WPF.Models;
using 翻译神器WPF.Service;
using 翻译神器WPF.Update;
using 翻译神器WPF.Util;
using 翻译神器WPF.Views;
using 鹰眼OCR_Common.Constants;
using 鹰眼OCR_Common.Messages;

namespace 翻译神器WPF.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject, IDisposable
    {
        public MainWindowViewModel()
        {
            InitializeNavigationItems();
        }

        public MainWindowViewModel(IServiceProvider service, IWindowService windowService, ITranslateApiService apiService)
        {
            _windowService = windowService;
            _services = service;
            var configData = _services.GetRequiredService<ConfigData>();
            _settingsData = configData.SettingsData;
            _translateApiService = apiService;

            InitializeNavigationItems();
            // 默认选中首页
            Navigation(NavigationItems[0]);

            if(_settingsData.IsReadingAloud)
            {
                speech = new SpeechSynthesizer();
                speech.SetOutputToDefaultAudioDevice();
            }

            // 预先创建 TextWindow 实例，确保其 ViewModel 注册消息接收器
            _windowService.Instance<TextWindow>();

            // 注册截图窗体消息
            WeakReferenceMessenger.Default.Register<ScreenshotMessage, string>(this, MessageTokens.ScreenWindow, async (obj, param) =>
            {
                // 接受到截图窗体传过来的截图，开始翻译
                var val = param.Value;
                TranslationAsync(val.CaptureImage);
            });
        }

        private readonly IWindowService _windowService;
        private readonly IServiceProvider _services;
        private readonly ITranslateApiService _translateApiService;
        private readonly SettingsData _settingsData;
        private readonly SpeechSynthesizer speech;

        private string _lastTargetPage = "";
        private IntPtr _handle;
        private HwndSource _hwndSource;
        private DateTime _lastTime;// 上次处理热键时间


        [ObservableProperty]
        private string _title = "翻译神器";

        [ObservableProperty]
        private Page _currentPage;

        public ObservableCollection<NavigationItem> NavigationItems { get; } = [];

        private void InitializeNavigationItems()
        {
            NavigationItems.Add(new NavigationItem("首页", "HomePage"));
            NavigationItems.Add(new NavigationItem("翻译Key", "KeyPage"));
            NavigationItems.Add(new NavigationItem("设置", "SettingsPage"));
            NavigationItems.Add(new NavigationItem("更新", "UpdatePage"));
            NavigationItems.Add(new NavigationItem("关于", "AboutPage"));
        }

        [RelayCommand]
        private void SelectNavigationItem(NavigationItem item)
        {
            if (item == null)
                return;
            Navigation(item);
        }

        private void Navigation(NavigationItem item)
        {
            // 更新所有项的选中状态
            foreach (var navItem in NavigationItems)
            {
                navItem.IsSelected = (navItem == item);
            }

            try
            {
                if (item.TargetPage.Equals("SettingsPage", StringComparison.OrdinalIgnoreCase))
                {
                    // 导航到设置页面，卸载热键
                    UnInstallAllHotkeys();
                }
                else if (_lastTargetPage.Equals("SettingsPage", StringComparison.OrdinalIgnoreCase))
                {// 导航到其他页面，如果上一个页面是热键设置页面，重新安装热键
                    InstallAllHotkeys();
                }

            }
            catch (Exception e)
            {
                NotifierHelper.ShowError(e);
            }
            _lastTargetPage = item.TargetPage;
            // 导航
            CurrentPage = (Page)_services.GetRequiredKeyedServiceEx(item.TargetPage);
        }

        /// <summary>
        /// 任务栏托盘菜单
        /// </summary>
        /// <param name="para"></param>
        [RelayCommand]
        private void TaskbarIcon(string para)
        {
            switch (para)
            {
                case "Screenshot":
                    Screenshot();
                    break;
                case "Show":
                    MainWindowShow();
                    break;
                case "Hide":
                    MainWindowHide();
                    break;
                case "Exit":
                    _windowService.Close<MainWindow>();
                    break;
            }
        }

        private void MainWindowShow()
        {
            var win = _windowService.Instance<MainWindow>();
            // 任务栏可见
            win.ShowInTaskbar = true;
            if (win.Visibility != Visibility.Visible)
            {
                win.Show();
            }

            // 若处于最小化，先还原
            if (win.WindowState == WindowState.Minimized)
            {
                win.WindowState = WindowState.Normal;
            }

            // 激活
            win.Activate();
            win.Focus();
        }

        private void MainWindowHide()
        {
            var win = _windowService.Instance<MainWindow>();
            if (win.WindowState != WindowState.Minimized)
            {
                win.Hide();
            }
        }

        /// <summary>
        /// 窗口状态变更
        /// </summary>
        /// <param name="state"></param>
        [RelayCommand]
        public void WindowStateChanged(string state)
        {
            switch (state)
            {
                case "ContentRendered":
                    // 窗口渲染完成，监听窗口消息，安装热键
                    try
                    {
                        _handle = _windowService.Instance<MainWindow>().Handle;
                        _hwndSource = HwndSource.FromHwnd(_handle);
                        _hwndSource.AddHook(WndProc);
                        InstallAllHotkeys();
                    }
                    catch (Exception e)
                    {
                        NotifierHelper.ShowError(e);
                    }
                    Task.Run(async () =>
                    {
                        // 检查更新
                        try
                        {
                            // 检查更新
                            var updateService = _services.GetRequiredService<UpdateService>();
                            var result = await updateService.CheckAsync();
                            if (result)
                            {
                                App.Current.Dispatcher.Invoke(() =>
                                {
                                    MainWindowShow();
                                    NotifierHelper.ShowInformation("检测到新版本，请在更新页面查看");
                                });
                            }
                        }
                        catch
                        {
                            // 忽略更新检查异常
                        }
                    });
                    break;

                case "StateChanged":
                    // 窗口状态变更
                    var win = _windowService.Instance<MainWindow>();
                    win.ShowInTaskbar = win.WindowState != WindowState.Minimized;
                    if(win.WindowState == WindowState.Minimized)
                    {
                        // 最小化时隐藏窗口
                        win.Hide();
                    }
                    break;

                case "Closed":
                    UnInstallAllHotkeys();
                    Dispose();
                    break;
            }
        }

        [RelayCommand]
        private void KeyDown(System.Windows.Input.KeyEventArgs e)
        {
            if (CurrentPage is SettingsPage)
            {
                string key = e.ToKeyString();
                if (key.Equals(_settingsData.ScreenshotHotKey) || key.Equals(_settingsData.FixedHotKey))
                {
                    // 按下任意键时，如果当前页面是设置页面，并且按下的键是热键，则提示当前页面按下热键无效
                    NotifierHelper.ShowWarning("在设置页面按下热键不生效，请切换到首页");
                }
            }
        }

        // 安装所有热键
        private void InstallAllHotkeys()
        {
            if (_settingsData.HaveDuplicateKeys)
            {
                throw new Exception("热键设置有重复，请重新设置");
            }
            HotKeyUtils.RegHotKey(_handle, _settingsData.ScreenshotHotKey, HotKeyIds.SCREEN_TRAN);
            HotKeyUtils.RegHotKey(_handle, _settingsData.FixedHotKey, HotKeyIds.FIXED_TRAN);
        }

        // 卸载所有热键

        private void UnInstallAllHotkeys()
        {
            HotKeyUtils.UnRegHotKey(_handle, HotKeyIds.SCREEN_TRAN);
            HotKeyUtils.UnRegHotKey(_handle, HotKeyIds.FIXED_TRAN);
        }

        /// <summary>
        /// 监听Windows消息
        /// 处理热键
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="handled"></param>
        /// <returns></returns>
        public nint WndProc(nint hwnd, int msg, nint wParam, nint lParam, ref bool handled)
        {
            if (msg == 0x0312)
            {
                // 防止热键重复触发
                if (DateTime.Now - _lastTime < TimeSpan.FromMilliseconds(300))
                {
                    return IntPtr.Zero;
                }
                _lastTime = DateTime.Now;
                // 标记处理了
                handled = true;
                HotKeyIds hotKeyId = Enum.Parse<HotKeyIds>(wParam.ToString());
                switch (hotKeyId)
                {
                    case HotKeyIds.SCREEN_TRAN:
                        {
                            // 截图翻译
                            Screenshot();
                            break;
                        }
                    case HotKeyIds.FIXED_TRAN:
                        {
                            // 固定翻译
                            FixedScreenshot();
                            break;
                        }
                }
            }
            else if (msg == WindowsMsg.ShowWindowMsg)
            {
                // 自定义消息，激活主窗口
                // 窗口最大化
                MainWindowShow();
            }
            return IntPtr.Zero;
        }


        /// <summary>
        /// 固定截图翻译
        /// </summary>
        private void FixedScreenshot()
        {
            try
            {
                if (_settingsData.Rect.IsEmpty)
                {
                    throw new Exception("请设置固定截图坐标！");
                }

                var image = ScreenShotHelper.CopyScreen(_settingsData.Rect.X, _settingsData.Rect.Y, _settingsData.Rect.Width, _settingsData.Rect.Height);

                TranslationAsync(image);
            }
            catch (Exception ex)
            {
                NotifierHelper.ShowError(ex);
            }
        }

        /// <summary>
        /// 截图识别
        /// </summary>
        private async Task Screenshot()
        {
            MainWindowHide();
            _windowService.Hide<TextWindow>();

            await Task.Delay(300); // 等待窗口隐藏完成（仍在UI线程）
            var screen = _windowService.Instance<ScreenWindow>();
            if (screen.IsVisible)
            {
                screen.Activate();
                return;
            }
            try
            {
                screen.Show(_handle);
            }
            catch (Exception ex)
            {
                MainWindowShow();
                NotifierHelper.ShowError("截图出错：" + ex.Message);
            }
        }

        /// <summary>
        /// 截图翻译
        /// </summary>
        private async Task TranslationAsync(Bitmap bitmap)
        {
            try
            {
                var api = _translateApiService.Current ?? throw new Exception("请先在设置页面选择翻译API并填写Key！");
                string src = "", dst = "";
                await Task.Run(() =>
                {
                    try
                    {
                        string destLangCode = api.LangDict.Values.ToArray()[_settingsData.SelectedLanguageIndex + 1];
                        api.PictureTranslate(bitmap, api.GetDefaultLangCode(), destLangCode, out src, out dst);
                    }
                    catch (Exception ex)
                    {
                        dst = $"错误：{ex.Message}";
                    }
                });
                // 复制原文到剪切板
                if (_settingsData.IsCopyOriginalText)
                    Clipboard.SetText(src);
                // 复制译文到剪切板
                if (_settingsData.IsCopyTranText)
                    Clipboard.SetText(dst);
                // 文字转语音
                if (_settingsData.IsReadingAloud)
                    Speech(dst);

                ShowText(dst);
            }
            catch (Exception ex)
            {
                ShowText("错误：" + ex.Message);
            }
        }

        // 文字转语音
        private void Speech(string text)
        {
            if (string.IsNullOrEmpty(text))
                return;
            speech.SpeakAsyncCancelAll();
            speech.SpeakAsync(text);
        }

        // 显示文本
        private void ShowText(string text)
        {
            WeakReferenceMessenger.Default.Send(new TextMessage(text), MsgTokens.TextWindow);
        }

        // 在显示文本窗口关闭时关闭发音
        //public void CloseSpeak()
        //{
        //    speech?.SpeakAsyncCancelAll();
        //}

        public void Dispose()
        {
            speech?.SpeakAsyncCancelAll();
            speech?.Dispose();
            _hwndSource?.RemoveHook(WndProc);
        }
    }
}