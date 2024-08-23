using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Screenshot_WPF.Api;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using 鹰眼OCR_Common.Constants;
using 鹰眼OCR_Extensions.QRCode;
using 鹰眼OCR_WPFCore.Helper;
using 鹰眼OCR_WPFCore.Messages;

namespace 鹰眼OCR_WPFCore.ViewModels
{
    public partial class QrCodeWindowViewModel : ObservableObject
    {

        [ObservableProperty]
        private ImageSource _image;


        [ObservableProperty]
        private double _imageSize;


        private string _text;
        /// <summary>
        /// 二维码文本
        /// </summary>
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                try
                {
                    ShowQRCode();
                }
                catch (Exception ex)
                {
                    NotifierHelper.ShowError(ex.Message, "");
                }
            }
        }

        private readonly Microsoft.Win32.SaveFileDialog _saveFileDialog;

        public QrCodeWindowViewModel()
        {
            _saveFileDialog = new()
            {
                Filter = "图片(*.png) | *.png|所有文件(*.*)|*.*",
                FileName = "二维码.png",
                RestoreDirectory = true,
                DefaultExt = "txt",
                AddExtension = true
            };
            ImageSize = 200;
            WeakReferenceMessenger.Default.Register<TextMessage, string>(this, MessageTokens.QrCodeWindow, (r, m) => Text = m.Value);
        }


        private void ShowQRCode()
        {
            var img = QRCode.GenerateQRCode(200, Text);
            var handle = img.GetHbitmap();
            Image = Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            // 释放资源 重要！！！
            WinApi.DeleteObject(handle);
            img.Dispose();
        }


        [RelayCommand]
        public void Save()
        {
            try
            {
                if (_saveFileDialog.ShowDialog() == true)
                {
                    string selectedFilePath = _saveFileDialog.FileName;
                    using var img = QRCode.GenerateQRCode((int)ImageSize, Text);
                    img.Save(selectedFilePath, ImageFormat.Png);
                }
            }
            catch (Exception e)
            {
                NotifierHelper.ShowError(e.Message, "");
            }

        }

    }
}
