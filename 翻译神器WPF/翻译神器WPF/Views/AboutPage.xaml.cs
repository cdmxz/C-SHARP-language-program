using System.Windows.Controls;
using 翻译神器WPF.ViewModels;

namespace 翻译神器WPF.Views
{
    /// <summary>
    /// AboutPage.xaml 的交互逻辑
    /// </summary>
    public partial class AboutPage : Page
    {
        public AboutPage(AboutPageViewModel viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
