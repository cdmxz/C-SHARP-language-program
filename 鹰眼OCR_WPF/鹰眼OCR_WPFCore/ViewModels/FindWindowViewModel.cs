using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using 鹰眼OCR_Common.Constants;
using 鹰眼OCR_WPFCore.Constants;
using 鹰眼OCR_WPFCore.Messages;
using 鹰眼OCR_WPFCore.Messages.TextBox;
using 鹰眼OCR_WPFCore.Messages.TextBox.MessageParam;

namespace 鹰眼OCR_WPFCore.ViewModels
{
    public partial class FindWindowViewModel : ObservableObject
    {

        private FindTypes _findType;
        public FindTypes FindType
        {
            get => _findType; set
            {
                _findType = value;
                OnPropertyChanged();
                if (value == FindTypes.Up)
                {
                    FindBtnContent = "上一个";
                }
                else
                {
                    FindBtnContent = "下一个";
                }
            }
        }


        /// <summary>
        /// 要查找的文本。
        /// </summary>
        private string _findText;
        public string FindText
        {
            get => _findText;
            set
            {
                _findText = value;
                OnPropertyChanged();
                _isFirst = true;
            }
        }





        /// <summary>
        /// 要替换的文本。
        /// </summary>
        [ObservableProperty]
        private string _replaceText;

        /// <summary>
        /// 是否大小写敏感。
        /// true：不忽略大小写。
        /// false：忽略大小写。
        /// </summary>
        [ObservableProperty]
        private bool _isCaseSensitive;

        /// <summary>
        /// 是否全字符匹配。
        /// </summary>
        [ObservableProperty]
        private bool _isWholeWord;

        /// <summary>
        /// 查找按钮的内容。
        /// </summary>
        [ObservableProperty]
        private string _findBtnContent;

        /// <summary>
        /// 显示的信息。
        /// </summary>
        [ObservableProperty]
        private string _info;

        // 这个字符串是否为第一次查找
        private bool _isFirst = true;

        // 上一次点击的按钮是查找按钮
        private bool _isFindMode = true;

        public FindWindowViewModel()
        {
            FindType = FindTypes.Down;
            // 接收查找文本
            WeakReferenceMessenger.Default.Register<DictionaryMessage, string>(this, MessageTokens.FindWindow, (r, m) =>
            {
                // key表示从哪个窗口发来的消息
                if (m.Value.TryGetValue(MessageTokens.HomeViewModel, out object? text))
                {
                    FindText = (string)text;
                }

                if (m.Value.TryGetValue(MessageTokens.HomeView, out object? info))
                {
                    Info = (string)info;
                }
            });

        }


        /// <summary>
        /// 查找
        /// </summary>
        [RelayCommand]
        private void Find()
        {
            _isFindMode = true;
            if (string.IsNullOrEmpty(FindText))
            {
                Info = "查找内容不能为空";
                return;
            }
            var param = new FindParam()
            {
                FindText = FindText,
                IgnoreCase = !IsCaseSensitive,
                IsWholeWord = IsWholeWord,
                IsPrevious = FindType == FindTypes.Up,
                IsFirst = _isFirst
            };
            var message = new FindMessage(param);
            WeakReferenceMessenger.Default.Send(message, MessageTokens.HomeView);
            _isFirst = false;
        }

        /// <summary>
        /// 替换
        /// </summary>
        /// <param name="p">字符串bool值，true全部替换，false非全部替换</param>
        [RelayCommand]
        private void Replace(string p)
        {
            _isFindMode = false;
            if (string.IsNullOrEmpty(FindText))
            {
                Info = "查找内容不能为空";
                return;
            }
            var param = new ReplaceParam()
            {
                FindText = FindText,
                ReplaceText = ReplaceText,
                IgnoreCase = !IsCaseSensitive,
                IsWholeWord = IsWholeWord,
                IsPrevious = FindType == FindTypes.Up,
                IsReplaceAll = bool.Parse(p),
                IsFirst = _isFirst
            };
            var message = new ReplaceMessage(param);
            WeakReferenceMessenger.Default.Send(message, MessageTokens.HomeView);
            _isFirst = false;
        }


        [RelayCommand]
        private void KeyUp(System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                if (_isFindMode)
                {
                    Find();
                }
                else
                {
                    Replace("false");
                }
            }

        }
    }
}
