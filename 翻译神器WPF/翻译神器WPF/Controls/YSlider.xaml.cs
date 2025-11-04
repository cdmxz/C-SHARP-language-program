using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace 翻译神器WPF.Controls
{
    /// <summary>
    /// YSlider.xaml 的交互逻辑
    /// </summary>
    public partial class YSlider : UserControl
    {
        public YSlider()
        {
            InitializeComponent();
            this.panel.DataContext = this;
        }

        // ValueChanged 路由事件
        public static readonly RoutedEvent ValueChangedEvent =
            EventManager.RegisterRoutedEvent(
                "ValueChanged",
                RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<double>),
                typeof(YSlider));

        public event RoutedPropertyChangedEventHandler<double> ValueChanged
        {
            add => AddHandler(ValueChangedEvent, value);
            remove => RemoveHandler(ValueChangedEvent, value);
        }


        // 命令：值改变时可执行
        public ICommand? ValueChangedCommand
        {
            get => (ICommand?)GetValue(ValueChangedCommandProperty);
            set => SetValue(ValueChangedCommandProperty, value);
        }

        public static readonly DependencyProperty ValueChangedCommandProperty =
            DependencyProperty.Register(nameof(ValueChangedCommand), typeof(ICommand), typeof(YSlider));

        // 可选：命令参数；未设置时默认传 newValue
        public object? ValueChangedCommandParameter
        {
            get => GetValue(ValueChangedCommandParameterProperty);
            set => SetValue(ValueChangedCommandParameterProperty, value);
        }

        public static readonly DependencyProperty ValueChangedCommandParameterProperty =
            DependencyProperty.Register(nameof(ValueChangedCommandParameter), typeof(object), typeof(YSlider));

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        // 默认为双向绑定
        // Using a DependencyProperty as the backing store for Value. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                "Value",
                typeof(double),
                typeof(YSlider),
                new FrameworkPropertyMetadata(
                    0.0,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnValuePropertyChanged));

        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (YSlider)d;
            double oldValue = e.OldValue is double ov ? ov : 0d;
            double newValue = e.NewValue is double nv ? nv : 0d;
            control.OnValueChanged(oldValue, newValue);
        }

        protected virtual void OnValueChanged(double oldValue, double newValue)
        {
            // 引发路由事件
            var args = new RoutedPropertyChangedEventArgs<double>(oldValue, newValue)
            {
                RoutedEvent = ValueChangedEvent
            };
            RaiseEvent(args);

            // 执行命令
            var cmd = ValueChangedCommand;
            var param = ValueChangedCommandParameter ?? newValue;
            if (cmd != null && cmd.CanExecute(param))
                cmd.Execute(param);
        }

        public double SmallChange
        {
            get => (double)GetValue(SmallChangeProperty);
            set => SetValue(SmallChangeProperty, value);
        }

        // Using a DependencyProperty as the backing store for Interval. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SmallChangeProperty =
            DependencyProperty.Register("SmallChange", typeof(double), typeof(YSlider), new PropertyMetadata(0.1));

        public double MaxValue
        {
            get => (double)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        // Using a DependencyProperty as the backing store for MaxValue. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(YSlider));

        public double MinValue
        {
            get => (double)GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }

        // Using a DependencyProperty as the backing store for MinValue. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(double), typeof(YSlider));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        // Using a DependencyProperty as the backing store for Text. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(YSlider));
    }
}
