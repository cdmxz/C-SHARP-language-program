using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace 翻译神器WPF.Controls
{
    /// <summary>
    /// YNumBox.xaml 的交互逻辑
    /// </summary>
    public partial class YNumBox : UserControl
    {
        public YNumBox()
        {
            InitializeComponent();
        }

        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(int), typeof(YNumBox), new PropertyMetadata(999));// 默认最大值为999

        public int Minimum
        {
            get { return (int)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(int), typeof(YNumBox), new PropertyMetadata(0));// 默认最小值为0

        public int Interval
        {
            get { return (int)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }

        public static readonly DependencyProperty IntervalProperty =
            DependencyProperty.Register("Interval", typeof(int), typeof(YNumBox), new PropertyMetadata(1));// 默认间隔为1

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set
            {
                if (value > Maximum)
                {
                    value = Maximum;
                }
                else if (value < Minimum)
                {
                    value = Minimum;
                }
                SetValue(ValueProperty, value);
            }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(YNumBox), new PropertyMetadata(default(int), OnValueChanged));

        public static readonly RoutedEvent ValueChangedEvent =
    EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(YNumBox));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is YNumBox numeric)
            {
                if (e.NewValue != e.OldValue)
                {
                    // 执行 ValueChangedCommand
                    if (numeric.ValueChangedCommand != null && numeric.ValueChangedCommand.CanExecute(e.NewValue))
                    {
                        numeric.ValueChangedCommand.Execute(e.NewValue);
                    }

                    // 引发 ValueChanged 事件
                    RoutedEventArgs args = new RoutedEventArgs(YNumBox.ValueChangedEvent, numeric);
                    numeric.RaiseEvent(args);
                }
            }
        }

        public ICommand UpCommand
        {
            get { return (ICommand)GetValue(UpCommandProperty); }
            set { SetValue(UpCommandProperty, value); }
        }

        public static readonly DependencyProperty UpCommandProperty =
            DependencyProperty.Register("UpCommand", typeof(ICommand), typeof(YNumBox), new PropertyMetadata(default(ICommand)));

        public ICommand DownCommand
        {
            get { return (ICommand)GetValue(DownCommandProperty); }
            set { SetValue(DownCommandProperty, value); }
        }

        public static readonly DependencyProperty DownCommandProperty =
            DependencyProperty.Register("DownCommand", typeof(ICommand), typeof(YNumBox), new PropertyMetadata(default(ICommand)));
        public ICommand ValueChangedCommand
        {
            get { return (ICommand)GetValue(ValueChangedCommandProperty); }
            set { SetValue(ValueChangedCommandProperty, value); }
        }

        public static readonly DependencyProperty ValueChangedCommandProperty =
            DependencyProperty.Register("ValueChangedCommand", typeof(ICommand), typeof(YNumBox), new PropertyMetadata(default(ICommand)));


        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(YNumBox), new PropertyMetadata(default(object)));

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // 只允许输入数字
            e.Handled = !IsTextAllowed(e.Text);
        }

        [System.Text.RegularExpressions.GeneratedRegex(@"^[0-9]+$")]
        private static partial System.Text.RegularExpressions.Regex MyRegex();

        private static bool IsTextAllowed(string text)
        {
            // 使用正则表达式验证是否为数字
            return MyRegex().IsMatch(text);
        }

        // 处理粘贴操作
        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            Value += Interval;
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            Value -= Interval;
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            // 失去焦点时，验证输入的值
            if (sender is TextBox textBox)
            {
                if (!IsTextAllowed(textBox.Text))
                {
                    // 如果输入的值无效，则恢复到上一个有效值
                    textBox.Text = Value.ToString();
                }
                else
                {
                    Value = int.Parse(textBox.Text);
                }
            }
        }
    }
}
