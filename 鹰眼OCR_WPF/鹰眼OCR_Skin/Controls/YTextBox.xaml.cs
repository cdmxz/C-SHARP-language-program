using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace 鹰眼OCR_Skin.Controls
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



        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (IsReceiveKey)
            {
                e.Handled = true;
                TextBox.Clear();
                if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
                {
                    TextBox.Text = $"{e.KeyboardDevice.Modifiers},";
                }
                if (e.Key != Key.None
                    && e.Key != Key.LeftAlt
                     && e.Key != Key.RightAlt
                     && e.Key != Key.LeftCtrl
                     && e.Key != Key.RightCtrl
                    && e.Key != Key.LeftShift
                    && e.Key != Key.RightShift
             )
                {
                    // 使用 KeyEventArgs 的 SystemKey 属性来捕获修饰键
                    Key actualKey = (e.Key == Key.System) ? e.SystemKey : e.Key;
                    TextBox.Text += $"{actualKey}";
                }

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
    }
}
