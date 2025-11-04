using System.ComponentModel;
using System.Globalization;
using System.Resources;

namespace 鹰眼OCR_WPF.Manager
{
    public class LanguageManager : INotifyPropertyChanged
    {
        /// <summary>
        /// 资源
        /// </summary>
        private readonly ResourceManager _resourceManager;

        /// <summary>
        /// 懒加载
        /// </summary>
        private static readonly Lazy<LanguageManager> _lazy = new Lazy<LanguageManager>(() => new LanguageManager());
        public static LanguageManager Instance => _lazy.Value;
        public event PropertyChangedEventHandler? PropertyChanged;


        public LanguageManager()
        {
            //获取此命名空间下Resources的Lang的资源
            _resourceManager = new ResourceManager("鹰眼OCR_WPF.Resources.lang", typeof(LanguageManager).Assembly);
        }

        /// <summary>
        /// 索引器的写法，传入字符串的下标
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public string this[string name]
        {
            get
            {
                ArgumentNullException.ThrowIfNull(name);
                return _resourceManager.GetString(name) ?? throw new ArgumentException("找不到资源：" + name);
            }
        }

        public void ChangeLanguage(CultureInfo cultureInfo)
        {
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("item[]")); 
        }


    }

}
