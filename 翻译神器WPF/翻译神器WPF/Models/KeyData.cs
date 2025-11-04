using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using 翻译神器WPF.TranslationApi.Common;

namespace 翻译神器WPF.Models
{
    public partial class KeyData : ObservableObject
    {
        [ObservableProperty]
        private BaiduKey _baiduKey;

        [ObservableProperty]
        private YoudaoKey _youdaoKey;

        [ObservableProperty]
        private TengxunKey _tengxunKey;

        
    }
}
