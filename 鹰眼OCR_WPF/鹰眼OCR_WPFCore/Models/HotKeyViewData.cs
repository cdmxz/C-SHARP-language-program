using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace 鹰眼OCR_WPFCore.Models
{

    /// <summary>
    /// 热键配置数据类
    /// </summary>
    public partial class HotKeyViewData : ObservableObject, INotifyDataErrorInfo
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
                var values = new[] { FixedHotKey, RecordingHotKey, PhotoHotKey, ScreenshotHotKey };
                var duplicates = values.GroupBy(x => x)
                                       .Where(g => g.Count() > 1 && !string.IsNullOrEmpty(g.Key))
                                       .Select(g => g.Key)
                                       .ToList();
                return duplicates.Count != 0;
            }
        }

        /// <summary>
        /// 截图热键
        /// </summary>
        private string _screenshotHotKey = string.Empty;

        /// <summary>
        /// 拍照热键
        /// </summary>
        private string _photoHotKey = string.Empty;

        /// <summary>
        /// 录音热键
        /// </summary>
        private string _recordingHotKey = string.Empty;

        /// <summary>
        /// 固定截图热键
        /// </summary>
        private string _fixedHotKey = string.Empty;

        /// <summary>
        /// 固定截图矩形
        /// </summary>
        [ObservableProperty]
        private Rectangle _rect;


        public string ScreenshotHotKey
        {
            get => _screenshotHotKey;
            set
            {
                var oldValue = _screenshotHotKey;
                _screenshotHotKey = value;
                Validate();
                OnPropertyChanged();
            }
        }
        public string PhotoHotKey
        {
            get => _photoHotKey;
            set
            {
                _photoHotKey = value;
                Validate();
                OnPropertyChanged();
            }
        }
        public string RecordingHotKey
        {
            get => _recordingHotKey;
            set
            {
                var oldValue = _recordingHotKey;
                _recordingHotKey = value;
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



    }
}