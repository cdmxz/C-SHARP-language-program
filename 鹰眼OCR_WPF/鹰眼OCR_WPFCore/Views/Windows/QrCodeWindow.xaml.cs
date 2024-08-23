using System.Windows;
using 鹰眼OCR_WPFCore.ViewModels;

namespace 鹰眼OCR_WPFCore.Views.Windows
{
    /// <summary>
    /// QrCodeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class QrCodeWindow : Window
    {
        public QrCodeWindow(QrCodeWindowViewModel viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();


        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}