using System.Windows.Controls;
using 翻译神器WPF.ViewModels;

namespace 翻译神器WPF.Views
{
    /// <summary>
    /// UpdatePage.xaml 的交互逻辑
    /// </summary>
    public partial class UpdatePage : Page
    {
        public UpdatePage(UpdatePageViewModel viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
