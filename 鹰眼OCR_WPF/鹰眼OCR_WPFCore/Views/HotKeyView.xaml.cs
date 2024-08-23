using System.Windows.Controls;
using 鹰眼OCR_WPFCore.ViewModels;

namespace 鹰眼OCR_WPFCore.Views
{
    /// <summary>
    /// HotKeyView.xaml 的交互逻辑
    /// </summary>
    public partial class HotKeyView : UserControl
    {
        public HotKeyView(HotKeyViewModel viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
