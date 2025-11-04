using System.Windows.Controls;
using 鹰眼OCR_WPF.ViewModels;

namespace 鹰眼OCR_WPF.Views
{
    /// <summary>
    /// OptionView.xaml 的交互逻辑
    /// </summary>
    public partial class OptionView : UserControl
    {
        public OptionView(OptionViewModel viewModel)
        {

            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
