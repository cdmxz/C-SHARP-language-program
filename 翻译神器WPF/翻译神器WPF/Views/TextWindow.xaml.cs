using CommunityToolkit.Mvvm.Messaging;
using System.Windows;
using System.Windows.Input;
using 翻译神器WPF.Common;
using 翻译神器WPF.ViewModels;

namespace 翻译神器WPF
{
    /// <summary>
    /// TextWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TextWindow : Window
    {
        public TextWindow(TextWindowViewModel viewModel)
        {

            this.DataContext = viewModel;
            InitializeComponent();
        }

        private void DockPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // 本窗口关闭时，隐藏窗口，因为要重复打开
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        // 当窗口可见性改变时，和视图模型通信
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            WeakReferenceMessenger.Default.Send(new VisibilityChangedMessage((bool)e.NewValue), MsgTokens.TextWindow);
        }
    }
}
