using 翻译神器WPF.TranslationApi;

namespace 翻译神器WPF.Service
{
    public interface ITranslateApiService
    {
        ITranslateApi Current { get; }

        ITranslateApi ReLoadApi();
    }
}
