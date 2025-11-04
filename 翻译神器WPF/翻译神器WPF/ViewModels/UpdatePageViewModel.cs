using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using 翻译神器WPF.Update;
using 翻译神器WPF.Util;

namespace 翻译神器WPF.ViewModels
{
    public partial class UpdatePageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _updateInfo;

        [ObservableProperty]
        private bool _buttonIsEnabled;

        private readonly UpdateService _updateService;

        public UpdatePageViewModel() { }

        public UpdatePageViewModel(UpdateService service)
        {
            _updateService = service;
            _updateInfo = $"当前版本：{_updateService.CurrentVersion}\r\n\r\n";
            var info = service.Info;
            if (service.IsNewVersionAvailable)
            {
                _updateInfo += $"发现新版本: {info?.Version}\n\n更新内容:\n{info?.Info}\n\n下载链接：{info?.Url}\n\n密码：{info?.Password}";
            }
            else
            {
                _updateInfo += $"更新内容:\n{info?.Info}";
            }
            _buttonIsEnabled = service.IsNewVersionAvailable;
        }

        [RelayCommand]
        private void CopyUrl()
        {
            try
            {
                var info = _updateService.Info;
                var text = $"下载链接：{info?.Url}\n密码：{info?.Password}";
                // 复制下载链接到剪贴板
                Clipboard.SetText(text);
                NotifierHelper.ShowInformation("下载链接已复制到剪贴板");
            }
            catch
            {
            }
        }

        [RelayCommand]
        private void OpenUrl()
        {
            try
            {
                // 复制密码到剪贴板
                Clipboard.SetText(_updateService.Info?.Password ?? string.Empty);
                NotifierHelper.ShowInformation("密码已复制到剪贴板");
            }
            catch
            {

            }
            try
            {
                _updateService.OpenUpdateUrl();
            }
            catch (Exception ex)
            {
                NotifierHelper.ShowError("无法打开更新链接: " + ex.Message);
            }
        }
    }
}