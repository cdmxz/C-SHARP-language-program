using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace 翻译神器WPF.Controls
{
    /// <summary>
    /// YTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class YTextBox : UserControl
    {
        public YTextBox()
        {
            InitializeComponent();
            panel.DataContext = this;
        }

        /// <summary>
        /// textbox的左侧文字
        /// </summary>
        public string Tip
        {
            get => (string)GetValue(TipProperty);
            set => SetValue(TipProperty, value);
        }

        // Using a DependencyProperty as the backing store for Tip.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TipProperty =
            DependencyProperty.Register("Tip", typeof(string), typeof(YTextBox));



        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(YTextBox), new PropertyMetadata(string.Empty));

        public double TextWidth
        {
            get => (double)GetValue(TextWidthProperty);
            set => SetValue(TextWidthProperty, value);
        }

        // Using a DependencyProperty as the backing store for TextWidth. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextWidthProperty =
            DependencyProperty.Register("TextWidth", typeof(double), typeof(YTextBox), new PropertyMetadata(120.0));


        /// <summary>
        /// 是否接收键盘按键
        /// </summary>
        public bool _isReceiveKey;

        public bool IsReceiveKey
        {
            get => _isReceiveKey;
            set
            {
                _isReceiveKey = value;
                TextBox.IsReadOnly = value;
                InputMethod.SetIsInputMethodEnabled(TextBox, !value); // 禁用输入法
            }
        }

        /// <summary>
        /// 当输入只包含修饰键时触发的路由事件（冒泡）
        /// </summary>
        public static readonly RoutedEvent InvalidInputEvent =
            EventManager.RegisterRoutedEvent(
                "InvalidInput",
                RoutingStrategy.Bubble,
                typeof(RoutedEventHandler),
                typeof(YTextBox));

        public event RoutedEventHandler InvalidInput
        {
            add { AddHandler(InvalidInputEvent, value); }
            remove { RemoveHandler(InvalidInputEvent, value); }
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!IsReceiveKey) return;

            e.Handled = true;
            TextBox.Clear();

            // 先写入修饰键
            if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
            {
                TextBox.Text = $"{e.KeyboardDevice.Modifiers}".Replace(",", "+");
            }

            // 追加非修饰主键
            if (e.Key != Key.None
                && e.Key != Key.LeftAlt
                && e.Key != Key.RightAlt
                && e.Key != Key.LeftCtrl
                && e.Key != Key.RightCtrl
                && e.Key != Key.LeftShift
                && e.Key != Key.RightShift
                && e.Key != Key.LWin
                && e.Key != Key.RWin)
            {
                Key actualKey = (e.Key == Key.System) ? e.SystemKey : e.Key;
                if (!string.IsNullOrEmpty(TextBox.Text))
                    TextBox.Text += "+";
                TextBox.Text += actualKey.ToString();
            }
        }

        private void TextBox_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (IsReceiveKey)
            {
                TextBox.Clear();
                e.Handled = true;
            }
        }

        // 仅在焦点丢失时校验：只包含修饰键则提示
        private void TextBox_LostFocus(object? sender, RoutedEventArgs e)
        {
            var input = TextBox.Text?.Trim() ?? string.Empty;

            if (IsOnlyModifiers(input))
            {
                var message = $"不能只包含修饰键，请加上主键（如 {input}+P）";

                // 标记错误
                MarkValidationError(message);

                // 抛事件
                RaiseEvent(new InvalidInputEventArgs(InvalidInputEvent, TextBox, message));
            }
            else
            {
                ClearValidationError();
                TextBox.ClearValue(ToolTipProperty);
            }
        }

        private static bool IsOnlyModifiers(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return false;

            var parts = text.Split('+', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (parts.Length == 0)
                return false;

            foreach (var p in parts)
            {
                if (!Enum.IsDefined(typeof(ModifierKeys), p))
                    return false;
            }
            return true;
        }

        private void MarkValidationError(string message)
        {
            var be = TextBox.GetBindingExpression(TextBox.TextProperty);
            if (be != null)
            {
                Validation.MarkInvalid(be, new ValidationError(new ExceptionValidationRule(), be)
                {
                    ErrorContent = message
                });
            }
        }

        private void ClearValidationError()
        {
            var be = TextBox.GetBindingExpression(TextBox.TextProperty);
            if (be != null)
            {
                Validation.ClearInvalid(be);
            }
        }
    }

    // 自定义事件参数，携带错误消息
    public sealed class InvalidInputEventArgs : RoutedEventArgs
    {
        public string ErrorMessage { get; }

        public InvalidInputEventArgs(RoutedEvent routedEvent, object source, string errorMessage)
            : base(routedEvent, source)
        {
            ErrorMessage = errorMessage;
        }
    }
}
