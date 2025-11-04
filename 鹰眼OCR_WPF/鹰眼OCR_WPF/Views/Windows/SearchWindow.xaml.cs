using System.Windows;
using System.Windows.Input;
using 鹰眼OCR_WPF.ViewModels;

namespace 鹰眼OCR_WPF.Views.Windows
{
    /// <summary>
    /// SearchWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SearchWindow : Window
    {
        public SearchWindow(SearchWindowViewModel viewModel)
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
