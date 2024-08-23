using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Animation;
using 鹰眼OCR_WPFCore.ViewModels;

namespace 鹰眼OCR_WPF.Views.Windows
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 窗口句柄
        /// </summary>
        public IntPtr Handle { get; private set; }

        public MainWindow(MainWindowViewModel viewModel)
        {
            this.Loaded += MainWindow_Loaded;
            this.ContentRendered += MainWindow_ContentRendered;
            this.DataContext = viewModel;
            InitializeComponent();
        }

        private void MainWindow_ContentRendered(object? sender, EventArgs e)
        {
            // 窗口内容渲染完成，隐藏动画面板
            Canvas.Visibility = Visibility.Collapsed;
        }


        // 最小化
        private void button_Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        // 最大化
        private void button_Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

        // 关闭
        private void button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // 窗口移动
        // 要设置grid的背景颜色才会触发
        private void grid_Top_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void topMostBotton_TopChanged(object sender, 鹰眼OCR_Skin.Controls.TopMostEventArgs e)
        {
            this.Topmost = e.IsTopMost;
        }


        // 双击顶部标题栏最大化，还原
        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var pos = Mouse.GetPosition(grid_Top);

            // 检查 pos 是否在 grid_Top 的范围内
            if (pos.X >= 0 && pos.X <= grid_Top.ActualWidth && pos.Y >= 0 && pos.Y <= grid_Top.ActualHeight)
            {
                if (this.WindowState == WindowState.Maximized)
                {
                    this.WindowState = WindowState.Normal;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                }
            }
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Handle = new WindowInteropHelper(this).Handle;
        }
    }
}


