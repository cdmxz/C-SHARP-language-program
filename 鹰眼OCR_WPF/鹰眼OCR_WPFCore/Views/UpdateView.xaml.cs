using System.Windows.Controls;
using 鹰眼OCR_WPFCore.ViewModels;

namespace 鹰眼OCR_WPFCore.Views
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
