using CommunityToolkit.Mvvm.Messaging;
using System.Windows;
using 鹰眼OCR_Common.Constants;
using 鹰眼OCR_WPFCore.Messages;
using 鹰眼OCR_WPFCore.ViewModels;

namespace 鹰眼OCR_WPFCore.Views.Windows
{
    /// <summary>
    /// CameraWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CameraWindow : Window
    {
        public CameraWindow(CameraWindowViewModel viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // 窗口隐藏和显示时，给viewmodel发送消息 控制摄像头的开启和关闭
            if (this.Visibility == Visibility.Visible)
            {
                WeakReferenceMessenger.Default.Send(new DictionaryMessage(new Dictionary<string, object>() { { MessageTokens.CameraWindow, "Start" } }), MessageTokens.CameraWindowViewModel);
            }
            else if (this.Visibility == Visibility.Hidden)
            {
                WeakReferenceMessenger.Default.Send(new DictionaryMessage(new Dictionary<string, object>() { { MessageTokens.CameraWindow, "Stop" } }), MessageTokens.CameraWindowViewModel);
            }
        }
    }
}
