using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Windows.Media;
using 翻译神器WPF.JsonConverters;
using Color = System.Windows.Media.Color;

namespace 翻译神器WPF.Models
{
    public partial class SettingsData : ObservableObject, INotifyDataErrorInfo
    {
        [JsonIgnore]
        private readonly Dictionary<string, string> _errors = new();

        [JsonIgnore]
        public bool HasErrors => _errors.Count != 0;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable<string> GetErrors(string propertyName)
        {
            yield return _errors.TryGetValue(propertyName, out string? value) ? value : string.Empty;
        }

        private void AddError(string error, [CallerMemberName] string? propertyName = null)
        {
            if (!_errors.TryAdd(propertyName, error))
            {
                _errors[propertyName] = error;
            }
        }

        private void OnErrorsChanged([CallerMemberName] string? propertyName = null)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        IEnumerable INotifyDataErrorInfo.GetErrors(string? propertyName)
        {
            if (_errors.TryGetValue(propertyName, out string? value))
            {
                yield return value;
            }
        }


        // 校验方法
        private bool Validate([CallerMemberName] string? propertyName = null)
        {
            _errors.Clear();
            if (HaveDuplicateKeys)
            {
                AddError("热键不能重复", propertyName);
                OnErrorsChanged(propertyName);
            }
            return HaveDuplicateKeys == false;
        }


        /// <summary>
        /// 判断是否有重复的热键
        /// </summary>
        [JsonIgnore]
        public bool HaveDuplicateKeys
        {
            get
            {
                return (FixedHotKey.Equals(ScreenshotHotKey) && !string.IsNullOrEmpty(FixedHotKey));
            }
        }

        /// <summary>
        /// 截图热键
        /// </summary>
        private string _screenshotHotKey = string.Empty;

        /// <summary>
        /// 固定截图热键
        /// </summary>
        private string _fixedHotKey = string.Empty;

        /// <summary>
        /// 选择的翻译接口索引
        /// </summary>
        [ObservableProperty]
        private int _selectedApiIndex;

        ///// <summary>
        ///// 选择的翻译接口项
        ///// </summary>
        //[ObservableProperty]
        //private string _selectedApiItem;

        /// <summary>
        /// 选择的目标翻译语言索引
        /// </summary>
        [ObservableProperty]
        private int _selectedLanguageIndex;

        /// <summary>
        /// 固定截图矩形
        /// </summary>
        [ObservableProperty]
        private Rectangle _rect;

        [ObservableProperty]
        private bool _isReadingAloud;

        [ObservableProperty]
        private bool _isCopyTranText;

        [ObservableProperty]
        private bool _isCopyOriginalText;

        [ObservableProperty]
        private int _delayTime = 5;

        [ObservableProperty]
        private string _selectedTextWindowLocation;

        [ObservableProperty]
        private int _selectedTextWindowLocationIndex;

        [ObservableProperty]
        private double _textWindowOpacity = 1.0;

        [ObservableProperty]
        private double _textWindowFontSize = 18;

        [ObservableProperty]
        private bool _isTextWindowTopmost;

        [ObservableProperty]
        private double _textWindowWidth = 300;

        [ObservableProperty]
        private double _textWindowHeight = 200;

        [ObservableProperty]
        private double _textWindowLeft = 0;

        [ObservableProperty]
        private double _textWindowTop = 0;

        public string ScreenshotHotKey
        {
            get => _screenshotHotKey;
            set
            {
                _screenshotHotKey = value;
                Validate();
                OnPropertyChanged();
            }
        }

        public string FixedHotKey
        {
            get => _fixedHotKey;
            set
            {
                _fixedHotKey = value;
                Validate();
                OnPropertyChanged();
            }
        }


        private Color _textBgColor = Colors.DarkGray;

        [JsonConverter(typeof(ColorJsonConverter))]
        public Color TextBgColor
        {
            get => _textBgColor;
            set
            {
                _textBgColor = value;
                OnPropertyChanged();
            }
        }

    }
}
