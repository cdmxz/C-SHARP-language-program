using System.Windows.Controls;
using 鹰眼OCR_WPFCore.ViewModels;

namespace 鹰眼OCR_WPFCore.Views
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
