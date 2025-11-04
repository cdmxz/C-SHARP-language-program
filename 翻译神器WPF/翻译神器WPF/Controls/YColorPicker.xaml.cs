using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace 翻译神器WPF.Controls
{
    /// <summary>
    /// YColorPicker.xaml 的交互逻辑
    /// </summary>
    public partial class YColorPicker : UserControl
    {
        public YColorPicker()
        {
            InitializeComponent();
        }

        // 路由事件：颜色改变
        public static readonly RoutedEvent ColorChangedEvent =
            EventManager.RegisterRoutedEvent(
                        nameof(ColorChanged),
                        RoutingStrategy.Bubble,
                        typeof(RoutedPropertyChangedEventHandler<Color>),
                        typeof(YColorPicker));

        // 公开事件
        public event RoutedPropertyChangedEventHandler<Color> ColorChanged
        {
            add => AddHandler(ColorChangedEvent, value);
            remove => RemoveHandler(ColorChangedEvent, value);
        }

        // 依赖属性：SelectedColor
        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register(
                nameof(SelectedColor),
                typeof(Color),
                typeof(YColorPicker),
                new FrameworkPropertyMetadata(
                    Colors.White,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnSelectedColorChanged));

        public Color SelectedColor
        {
            get => (Color)GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        // 依赖属性：ColorChangedCommand
        public static readonly DependencyProperty ColorChangedCommandProperty =
            DependencyProperty.Register(
                nameof(ColorChangedCommand),
                typeof(ICommand),
                typeof(YColorPicker),
                new PropertyMetadata(null));

        public ICommand? ColorChangedCommand
        {
            get => (ICommand?)GetValue(ColorChangedCommandProperty);
            set => SetValue(ColorChangedCommandProperty, value);
        }

        // 依赖属性：ColorChangedCommandParameter（可选）
        public static readonly DependencyProperty ColorChangedCommandParameterProperty =
            DependencyProperty.Register(
                nameof(ColorChangedCommandParameter),
                typeof(object),
                typeof(YColorPicker),
                new PropertyMetadata(null));

        public object? ColorChangedCommandParameter
        {
            get => GetValue(ColorChangedCommandParameterProperty);
            set => SetValue(ColorChangedCommandParameterProperty, value);
        }

        private static void OnSelectedColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var picker = (YColorPicker)d;
            var oldColor = (Color)e.OldValue;
            var newColor = (Color)e.NewValue;

            // 触发路由事件
            var args = new RoutedPropertyChangedEventArgs<Color>(oldColor, newColor, ColorChangedEvent);
            picker.OnColorChanged(args);

            // 执行命令
            var param = picker.ColorChangedCommandParameter ?? newColor; // 默认传新颜色
            var cmd = picker.ColorChangedCommand;
            if (cmd != null && cmd.CanExecute(param))
                cmd.Execute(param);
        }

        protected virtual void OnColorChanged(RoutedPropertyChangedEventArgs<Color> e)
            => RaiseEvent(e);
    }
}
