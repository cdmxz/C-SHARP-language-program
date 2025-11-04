using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using 翻译神器WPF.Util;
using 翻译神器WPF.ViewModels;

namespace 翻译神器WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 窗口句柄
        /// </summary>
        public IntPtr Handle { get; private set; }

        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Handle = new WindowInteropHelper(this).Handle;
        }


        private void MainWindow_Closed(object sender, EventArgs e)
        {
            try
            {
                // 释放托盘图标
                this.trayIcon?.Dispose();
            }
            catch { }
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //this.ShowInTaskbar = (bool)e.NewValue;
        }
    }
}