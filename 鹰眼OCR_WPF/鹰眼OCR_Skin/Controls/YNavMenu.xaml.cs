using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

// **************************************************************
// 菜单控件
// **************************************************************

namespace 鹰眼OCR_Skin.Controls
{
    /// <summary>
    /// YNavMenu.xaml 的交互逻辑
    /// </summary>
    public partial class YNavMenu : UserControl
    {
        public YNavMenu()
        {
            InitializeComponent();
            this.border.DataContext = this;
        }


        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(YNavMenu));


        public ImageSource Icon
        {
            get => (ImageSource)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        // Using a DependencyProperty as the backing store for MaxValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(YNavMenu));

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public static readonly DependencyProperty IsSelectedProperty =
        DependencyProperty.Register("IsSelected", typeof(bool), typeof(YNavMenu), new PropertyMetadata(false));

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register("Command", typeof(ICommand), typeof(YNavMenu), new PropertyMetadata(null));


        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(YNavMenu), new PropertyMetadata(default(object)));


        /// <summary>
        /// 点击YNavMenu时，设置当前控件的IsSelected为true
        /// 设置其它YNavMenu控件的IsSelected为false
        /// </summary>
        /// <param name="curObj"></param>
        [RelayCommand]
        public void Click(object curObj)
        {
            if (curObj is DependencyObject dependencyObject)
            {
                var parent = VisualTreeHelper.GetParent(dependencyObject);
                if (parent != null)
                {
                    // 判断父控件为Panel
                    if (parent is Panel panel)
                    {
                        // 在父控件中查找所有YNavMenu的控件
                        foreach (var child in panel.Children)
                        {
                            if (child is YNavMenu)
                            {
                                // 设置所有YNavMenu的IsSelected为false
                                ((YNavMenu)child).IsSelected = false;
                            }
                        }

                    }
                }
            }
            // 设置当前控件的IsSelected为true
            if (curObj is YNavMenu)
            {
                ((YNavMenu)curObj).IsSelected = true;
            }

        }

        private void UserControl_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Click(sender);
            // 执行Command
            Command?.Execute(CommandParameter);
        }

    }
}
