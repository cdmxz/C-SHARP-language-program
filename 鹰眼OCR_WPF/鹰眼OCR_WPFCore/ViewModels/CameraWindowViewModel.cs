using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using OpenCvSharp;
using System.Windows.Media;
using 鹰眼OCR_Common.Constants;
using 鹰眼OCR_WPF;
using 鹰眼OCR_WPFCore.Helper;
using 鹰眼OCR_WPFCore.Messages;

namespace 鹰眼OCR_WPFCore.ViewModels
{
    public partial class CameraWindowViewModel : ObservableObject
    {
        public CameraWindowViewModel()
        {
            CameraList = CameraHelper.GetCameras();
            SelectedCameraIndex = 0;
            // 注册消息 用于和CameraWindow.xaml.cs交互
            WeakReferenceMessenger.Default.Register<DictionaryMessage, string>(this, MessageTokens.CameraWindowViewModel, (r, m) =>
            {
                // key表示从哪个窗口发来的消息

                // 打开摄像头和停止摄像头 消息
                if (m.Value.TryGetValue(MessageTokens.CameraWindow, out object? value))
                {
                    if (value is string val)
                    {
                        if (val == "Start")
                        {
                            StartCapture();
                        }
                        else if (val == "Stop")
                        {
                            StopCapture();
                        }
                    }
                }
                else
                {
                    // 拍照消息
                    if (m.Value.TryGetValue(MessageTokens.HomeViewModel, out object? homeValue))
                    {
                        if (homeValue is string val)
                        {
                            if (val == "Photograph")
                            {
                                Photograph();
                            }
                        }
                    }
                }
            });
        }


        private ImageSource? _videoSource;
        public ImageSource? VideoSource
        {
            get => _videoSource;
            set
            {
                _videoSource = value;
                OnPropertyChanged();
            }
        }

        [ObservableProperty]
        private string[] _cameraList;


        private int _selectedCameraIndex;
        public int SelectedCameraIndex
        {
            get => _selectedCameraIndex;
            set => _selectedCameraIndex = value;
        }



        private VideoCapture? _capture;
        private CancellationTokenSource? _tokenSource;
        private Mat? _image;

        /// <summary>
        /// 拍照
        /// </summary>
        [RelayCommand]
        private void Photograph()
        {
            if (_capture != null && _image != null)
            {
                var img = BitmapHelper.MatToBitmap(_image);
                WeakReferenceMessenger.Default.Send(new DictionaryMessage(new Dictionary<string, object>() { { MessageTokens.CameraWindow, img } }), MessageTokens.HomeViewModel);
            }

        }

        private void StartCapture()
        {
            CloseThread();
            _tokenSource = new CancellationTokenSource();
            Task.Run(() =>
            {
                try
                {
                    RunCap(_tokenSource.Token);
                }
                catch (Exception e)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        NotifierHelper.ShowError(e.Message, "");
                    });
                }
            });
        }

        private void StopCapture()
        {
            VideoSource = null;
            CloseThread();
        }

        private void RunCap(CancellationToken token)
        {
            _capture = VideoCapture.FromCamera(SelectedCameraIndex);
            int sleepTime = (int)Math.Round(1000 / _capture.Fps);

            if (_image == null || _image.IsDisposed)
            {
                _image ??= new Mat();
            }

            // 进入读取视频每帧的循环
            while (!token.IsCancellationRequested)
            {
                _capture.Read(_image);
                //判断是否还有没有视频图像 
                if (_image.Empty())
                {
                    break;
                }

                VideoSource = BitmapHelper.MatToBitmapImage(_image);
                Cv2.WaitKey(sleepTime);
            }
            _image.Dispose();
            _image = null;
            _capture.Dispose();
        }



        // 关闭线程
        private void CloseThread()
        {
            if (_tokenSource?.IsCancellationRequested == false)
            {
                _tokenSource?.Cancel();
            }
        }

    }
}
