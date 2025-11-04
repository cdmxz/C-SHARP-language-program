using System.ComponentModel;
using System.Globalization;
using System.Resources;
using 鹰眼OCR_WPF.Manager;

namespace 鹰眼OCR_WPF.Service
{
    public class LanguageService : ILanguageService, INotifyPropertyChanged
    {
        public LanguageService()
        {
            //获取此命名空间下Resources的Lang的资源
            _resourceManager = new ResourceManager("鹰眼OCR_WPF.Resources.lang", typeof(LanguageManager).Assembly);
        }

        /// <summary>
        /// 资源
        /// </summary>
        private readonly ResourceManager _resourceManager;

        // 使用依赖注入
        //private static readonly Lazy<LanguageService> _instance = new Lazy<LanguageService>(() => new LanguageService());
        //public static LanguageService Instance => _instance.Value;

        public event PropertyChangedEventHandler? PropertyChanged;

        private CultureInfo _currentCulture = new CultureInfo("zh-Hans");
        public CultureInfo CurrentCulture
        {
            get => _currentCulture;
            set
            {
                if (!value.Equals(_currentCulture))
                {
                    _currentCulture = value;
                    Thread.CurrentThread.CurrentUICulture = value;
                    OnPropertyChanged(null);
                }
            }
        }

        public string SelectedLanguage
        {
            get => _currentCulture.Name;
            set => CurrentCulture = new CultureInfo(value);
        }

        public string GetString(string key)
        {
            ArgumentNullException.ThrowIfNull(key);
            return _resourceManager.GetString(key) ?? throw new ArgumentException("找不到资源：" + key);

        }

        protected void OnPropertyChanged(string? propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
