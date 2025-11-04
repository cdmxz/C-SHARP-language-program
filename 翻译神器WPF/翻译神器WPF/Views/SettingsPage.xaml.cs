using System.Windows.Automation;
using System.Windows.Controls;
using 翻译神器WPF.Util;
using 翻译神器WPF.ViewModels;

namespace 翻译神器WPF.Views
{
    /// <summary>
    /// SettingsPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage(SettingsPageViewModel settingsPageViewModel)
        {
            this.DataContext = settingsPageViewModel;
            InitializeComponent();
        }


    }
}
