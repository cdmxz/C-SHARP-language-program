using System.Windows.Controls;
using 鹰眼OCR_WPF.ViewModels;

namespace 鹰眼OCR_WPF.Views
{
    /// <summary>
    /// UpdateView.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateView : UserControl
    {
        public UpdateView(UpdateViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
