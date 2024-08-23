using System.Windows;
using System.Windows.Input;
using 鹰眼OCR_WPFCore.ViewModels;

namespace 鹰眼OCR_WPFCore.Views.Windows
{
    /// <summary>
    /// FindWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FindWindow : Window
    {
        public FindWindow(FindWindowViewModel viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Hide();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
