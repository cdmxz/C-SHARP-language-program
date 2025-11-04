using Microsoft.Extensions.DependencyInjection;
using ∑≠“Î…Ò∆˜WPF.Models;
using ∑≠“Î…Ò∆˜WPF.TranslationApi;
using ∑≠“Î…Ò∆˜WPF.TranslationApis;

namespace ∑≠“Î…Ò∆˜WPF.Service
{
    public class TranslateApiService : ITranslateApiService
    {
        private ITranslateApi _current;
        private readonly ConfigData _configData;

        public ITranslateApi Current
        {
            get
            {
                if (_current == null)
                {
                    _current = ReLoadApi();
                }
                return _current;
            }
            //private set
            //{
            //    ITranslateApi? old;
            //    lock (_lock)
            //    {
            //        old = _current;
            //        _current = value;
            //    }
            //    if (!ReferenceEquals(old, value))
            //    {
            //        CurrentChanged?.Invoke(value);
            //    }
            //}
        }

        public TranslateApiService(IServiceProvider serviceProvider)
        {
            _configData = serviceProvider.GetRequiredService<ConfigData>();
        }

        public ITranslateApi ReLoadApi()
        {
            var apis = ApiNames.GetAllApiChineseNames();
            var keys = _configData.KeyData;
            var currentApi = ApiFactory.Create(
               apiChineseName: apis[_configData.SettingsData.SelectedApiIndex],
                baiduKey: keys.BaiduKey,
                youdaoKey: keys.YoudaoKey,
                tengxunKey: keys.TengxunKey);
            _current = currentApi;
            return currentApi;
        }
    }
}
