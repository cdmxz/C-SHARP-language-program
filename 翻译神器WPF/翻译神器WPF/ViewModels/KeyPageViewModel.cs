using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;
using System.Windows.Resources;
using 翻译神器WPF.Models;
using 翻译神器WPF.Service;
using 翻译神器WPF.TranslationApis;
using 翻译神器WPF.Util;

namespace 翻译神器WPF.ViewModels
{
    public partial class KeyPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private KeyData _keyData;


        public KeyPageViewModel(IServiceProvider service, ITranslateApiService apiService)
        {
            var configData = service.GetRequiredService<ConfigData>();
            KeyData = configData.KeyData;
        }

        [RelayCommand]
        private async Task TestApi(string apiCode)
        {
            // 读取测试图片
            Uri uri = new("pack://application:,,,/Resources/KeyTest.jpg");
            StreamResourceInfo info = Application.GetResourceStream(uri);
            using Stream stream = info.Stream;
            System.Drawing.Bitmap bitmap = new(stream);
            // apiCode就是ApiCodes枚举每一项的名称
            var api = ApiFactory.Create(apiCode: apiCode,
                baiduKey: KeyData.BaiduKey,
                youdaoKey: KeyData.YoudaoKey,
                tengxunKey: KeyData.TengxunKey);

            try
            {
                await Task.Run(() =>
                {
                    api.KeyTest(bitmap);
                });
                NotifierHelper.ShowSuccess("测试成功");
            }
            catch (Exception e)
            {
                NotifierHelper.ShowError(e);
            }
              
        }
    }
}