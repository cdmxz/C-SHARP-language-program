using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using 翻译神器WPF.ViewModels;

namespace 翻译神器WPF.Views
{
    /// <summary>
    /// KeyPage.xaml 的交互逻辑
    /// </summary>
    public partial class KeyPage : Page
    {
        public KeyPage(KeyPageViewModel keyPageViewModel)
        {
            this.DataContext = keyPageViewModel;
            InitializeComponent();
        }
    }
}
