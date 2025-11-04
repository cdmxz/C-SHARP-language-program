using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Screenshot_WPF;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Speech.Synthesis;
using System.Windows.Media;
using 翻译神器WPF.Common;
using 翻译神器WPF.Controls;
using 翻译神器WPF.Models;
using 翻译神器WPF.Service;
using 翻译神器WPF.TranslationApi;
using 翻译神器WPF.Util;

namespace 翻译神器WPF.ViewModels
{
    public partial class SettingsPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private SettingsData _settingsData;

        [ObservableProperty]
        private bool _isAddedAutoStart;

        public ObservableCollection<string> TextWindowLocations { get; set; }

        private readonly IWindowService _windowService;
        private readonly ITranslateApiService _apiService;

        public SettingsPageViewModel()
        {

        }

        public SettingsPageViewModel(IWindowService windowService, IServiceProvider service, ITranslateApiService apiService)
        {
            _windowService = windowService;
            _apiService = apiService;
            var configData = service.GetRequiredService<ConfigData>();
            SettingsData = configData.SettingsData;
            AllApiNames = new ObservableCollection<string>(ApiNames.GetAllApiChineseNames());
            AllLanguageNames = [];
            // 初始化时加载默认接口的语言列表
            LoadAllLanguageNames(_apiService.Current);
            TextWindowLocations = ["屏幕顶部", "浮动"];
            if (string.IsNullOrEmpty(SettingsData.SelectedTextWindowLocation))
            {
                SettingsData.SelectedTextWindowLocation = TextWindowLocations[0];
            }
            IsAddedAutoStart = IsAddToAutoStart();
        }

        /// <summary>
        /// 翻译接口集合
        /// </summary>
        public ObservableCollection<string> AllApiNames { get; set; }

        /// <summary>
        /// 翻译语言集合
        /// </summary>
        public ObservableCollection<string> AllLanguageNames { get; set; }

        // 加载对应接口的目标语言到界面
        private void LoadAllLanguageNames(ITranslateApi api)
        {
            // Key语言名称（中文），Value是语言代号（英文）
            AllLanguageNames.Clear();
            var langNames = api.GetSupportedTargetLanguageNames();
            foreach (var name in langNames)
            {
                AllLanguageNames.Add(name);
            }
            SettingsData.SelectedLanguageIndex = 0;
        }

        [RelayCommand]
        private void Execute(string parameter)
        {
            switch (parameter)
            {
                case "ApiSelectionChanged":
                    _apiService.ReLoadApi();
                    LoadAllLanguageNames(_apiService.Current);
                    break;
                case "SetFixedArea":
                    SetFixedArea();
                    break;
                case "AutoStartToggle":
                    AutoStartToggle();
                    break;
                case "ReadingAloudChanged":
                    ReadingAloudChanged();
                    break;

            }
        }


        [RelayCommand]
        private void OpacityChanged(double newValue)
        {
            WeakReferenceMessenger.Default.Send(new TextWindowChangedMessage(), MsgTokens.TextWindow);
        }

        [RelayCommand]
        private void FontSizeChanged(double newValue)
        {
            WeakReferenceMessenger.Default.Send(new TextWindowChangedMessage(), MsgTokens.TextWindow);
        }


        [RelayCommand]
        private void YTextBox_InvalidInput(InvalidInputEventArgs e)
        {
            NotifierHelper.ShowWarning(e.ErrorMessage);
            e.Handled = true;
        }

        [RelayCommand]
        private void YTextBoxLostFocus()
        {
            // 检查两个热键是否重复
            if (SettingsData.ScreenshotHotKey.Equals(SettingsData.FixedHotKey))
            {
                if (!string.IsNullOrEmpty(SettingsData.ScreenshotHotKey))
                    NotifierHelper.ShowWarning("截图热键和翻译热键不能相同！");
            }
        }

        [RelayCommand]
        private void ColorChanged()
        {
            WeakReferenceMessenger.Default.Send(new TextWindowChangedMessage(), MsgTokens.TextWindow);
        }


        private async Task SetFixedArea()
        {
            IntPtr? _hwnd = null;
            _windowService.Hide<MainWindow>();
            await Task.Delay(300); // 延时以确保主窗口隐藏
            IntPtr parent = _windowService.Instance<MainWindow>().Handle;
            var screenWindow = _windowService.Instance<ScreenWindow>();
            screenWindow.ShowWithoutMessage(parent, _hwnd);
            SettingsData.Rect = screenWindow.CaptureRectangle;
            _windowService.Show<MainWindow>();
        }


        private void AutoStartToggle()
        {
            try
            {
                bool result;
                string appName = Assembly.GetExecutingAssembly().GetName().Name!;
                if (IsAddToAutoStart())
                {
                    result = Utils.AutoStartOperation(appName, null, OperationMode.Del);
                }
                else
                {
                    string appPath = Environment.ProcessPath! + " " + AutoStartConstant.StartParam;
                    result = Utils.AutoStartOperation(appName, appPath, OperationMode.Add);
                }
                if (!result)
                {
                    NotifierHelper.ShowError("操作失败，请以管理员权限运行本软件！");
                }
            }
            catch (Exception e)
            {
                NotifierHelper.ShowError($"操作出错：{e.Message} 请以管理员权限运行本软件！");
                IsAddedAutoStart = false;
            }
        }

        // 是否添加了自启动
        private static bool IsAddToAutoStart()
        {
            try
            {
                string appName = Assembly.GetExecutingAssembly().GetName().Name!;
                return Utils.AutoStartOperation(appName, null, OperationMode.Query);
            }
            catch
            {
                return false;
            }
        }

        private void ReadingAloudChanged()
        {
            try
            {
                var voices = new SpeechSynthesizer().GetInstalledVoices();
                if (voices.Count == 0)
                {
                    NotifierHelper.ShowWarning("系统未安装语音合成引擎，无法使用语音朗读功能！");
                    SettingsData.IsReadingAloud = false;
                }
            }
            catch (Exception ex)
            {
                NotifierHelper.ShowError($"语音朗读不可用，错误：{ex.Message}");
                SettingsData.IsReadingAloud = false;
            }
        }

    }
}
