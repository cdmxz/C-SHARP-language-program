using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using Screenshot_WPF;
using Screenshot_WPF.Helper;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using 鹰眼OCR_Common.Constants;
using 鹰眼OCR_Common.Messages;
using 鹰眼OCR_Extensions.Audio;
using 鹰眼OCR_Extensions.OCR;
using 鹰眼OCR_Extensions.PDF;
using 鹰眼OCR_Extensions.PDF.EventArgss;
using 鹰眼OCR_Extensions.QRCode;
using 鹰眼OCR_Extensions.TTS;
using 鹰眼OCR_WPF;
using 鹰眼OCR_WPF.Views.Windows;
using 鹰眼OCR_WPFCore.Constants;
using 鹰眼OCR_WPFCore.Helper;
using 鹰眼OCR_WPFCore.Messages;
using 鹰眼OCR_WPFCore.Models;
using 鹰眼OCR_WPFCore.Service;
using 鹰眼OCR_WPFCore.Views.Windows;


namespace 鹰眼OCR_WPFCore.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly IServiceProvider _services;
        private readonly IWindowService _windowService;

        private readonly Microsoft.Win32.OpenFileDialog _openFileDialog;
        private readonly Microsoft.Win32.SaveFileDialog _saveFileDialog;
        private readonly OpenFolderDialog _openFolderDialog;

        private readonly PlayAudio _playAudio;// 播放mp3
        private readonly PdfToImage _pdfToImage;
        private DateTime _lastTime;// 上次处理热键时间

        private readonly OptionViewData _optionViewData;
        private readonly HotKeyViewData _hotKeyViewData;


        // 使用属性，防止内存泄漏
        private Bitmap _globeImage;

        private Bitmap GlobeImage
        {
            get => _globeImage;
            set
            {
                _globeImage?.Dispose();
                _globeImage = value;
            }
        }

        private readonly Baidu _baidu;
        private CancellationTokenSource? _ocrTokenSource;


        public HomeViewModel(IServiceProvider services)
        {
            _windowService = services.GetRequiredService<IWindowService>();
            _services = services;
            var configData = services.GetRequiredService<ConfigData>();
            _optionViewData = configData.OptionViewData;
            _hotKeyViewData = configData.HotKeyViewData;

            _openFileDialog = new();
            _saveFileDialog = new();
            _openFolderDialog = new();


            // 设置文件框参数
            _openFileDialog.Filter = "图片和文档(*.jpg、*.png、*.bmp、*.webp、*.jpeg*.、*.pdf) | *.jpg;*.png;*.bmp;*.webp;*.jpeg;*.pdf|所有文件(*.*)|*.*";
            //设置默认文件名
            _openFileDialog.FileName = "";
            //设置默认文件类型显示顺序
            _openFileDialog.FilterIndex = 1;
            //是否记忆上次打开的目录
            _openFileDialog.RestoreDirectory = true;

            _saveFileDialog.Filter = "文本文件(*.txt) | *.txt|所有文件(*.*)|*.*";
            _saveFileDialog.FileName = "识别结果.txt";
            _saveFileDialog.RestoreDirectory = true;
            _saveFileDialog.DefaultExt = "txt";
            _saveFileDialog.AddExtension = true;

            _openFolderDialog.Title = "选择文件夹";
            _openFolderDialog.Multiselect = false;


            _baidu = services.GetRequiredService<Baidu>();
            _baidu.BaiduKey = new()
            {
                ApiKey = _optionViewData.ApiKey,
                SecretKey = _optionViewData.SecretKey,
                AppId = _optionViewData.AppId,
                Password = _optionViewData.Password
            };
            _playAudio = services.GetRequiredService<PlayAudio>();
            _pdfToImage = services.GetRequiredService<PdfToImage>();
            _pdfToImage.GotOnePageEvent += PdfCallback;


            OCRTypes = [
               new OCRTypeItem("通用文字",new BitmapImage(new Uri("pack://application:,,,/Resources/Images/通用文字识别.png"))),
                    new OCRTypeItem("通用_高精度",new BitmapImage(new Uri("pack://application:,,,/Resources/Images/通用文字识别_高精度.png"))) ,
                    new OCRTypeItem("手写文字",new BitmapImage(new Uri("pack://application:,,,/Resources/Images/手写文字识别.png"))) ,
                     new OCRTypeItem("二维码",new BitmapImage(new Uri("pack://application:,,,/Resources/Images/二维码.png"))) ,
                     new OCRTypeItem("表格",new BitmapImage(new Uri("pack://application:,,,/Resources/Images/表格.png"))) ,
                     new OCRTypeItem("数字",new BitmapImage(new Uri("pack://application:,,,/Resources/Images/数字.png"))) ,
                     new OCRTypeItem("公式",new BitmapImage(new Uri("pack://application:,,,/Resources/Images/公式.png"))) ,
                     new OCRTypeItem("身份证",new BitmapImage(new Uri("pack://application:,,,/Resources/Images/身份证.png"))) ,
                     new OCRTypeItem("银行卡",new BitmapImage(new Uri("pack://application:,,,/Resources/Images/银行卡.png"))) ,
                ];

            OCRLangTypes = [.. Baidu.GetOCRLangChinese()];
            IdCardSides = [.. Baidu.GetIdCardSides()];
            SourceLanguages = [.. Baidu.GetTranslateLangChinese()];
            DestLanguages = [.. Baidu.GetTranslateLangChinese()];
            DestLanguages.RemoveAt(0);// 移除“自动检测”
            TtsPersons = [.. MsTts.GetPersons()];

            SelectedOCRType = OCRTypes[0];
            SelectedPerson = TtsPersons[0];
            SelectedLangType = OCRLangTypes[0];
            SelectedIdCardSide = IdCardSides[0];
            SelectedSourceLanguage = SourceLanguages[0];
            SelectedDestLanguage = DestLanguages[0];


            BottomGridVisibility = Visibility.Collapsed;

            // 注册消息
            WeakReferenceMessenger.Default.Register<DictionaryMessage, string>(this, MessageTokens.HomeViewModel, MessageHandle);

            // 注册截图窗体消息
            WeakReferenceMessenger.Default.Register<ScreenshotMessage, string>(this, MessageTokens.ScreenWindow, async (obj, param) =>
            {
                var val = param.Value;
                GlobeImage = val.CaptureImage;

                if (_optionViewData.IsSaveScreenshotToLocal)
                {
                    BitmapHelper.SaveImage(val.CaptureImage, ConfigData.OCRDir);
                }

                if (_optionViewData.IsSaveScreenshotToClipboard)
                {
                    BitmapHelper.SaveToClipboard(val.CaptureImage);
                }

                await StartOCRAsync();

            });

        }



        #region 属性

        /// <summary>
        /// 是否显示右侧面板
        /// </summary>
        [ObservableProperty]
        private bool _isRightPanelVisible;

        /// <summary>
        /// 选中的文字识别类型
        /// </summary>
        [ObservableProperty]
        private OCRTypeItem _selectedOCRType;

        /// <summary>
        /// 选中的文字识别语言类型
        /// </summary>
        [ObservableProperty]
        private string _selectedLangType;

        /// <summary>
        /// 选中的身份证正反面
        /// </summary>
        [ObservableProperty]
        private string _selectedIdCardSide;

        /// <summary>
        /// 选中的语音合成发音人
        /// </summary>
        [ObservableProperty]
        private string _selectedPerson;

        /// <summary>
        /// 语音合成音量
        /// </summary>
        [ObservableProperty]
        private double _sliderSpeed;

        /// <summary>
        /// 左侧文本
        /// </summary>
        [ObservableProperty]
        private FlowDocument _leftDocument;


        [ObservableProperty]
        private string _leftText;


        /// <summary>
        /// 左侧选中文本
        /// </summary>
        [ObservableProperty]
        private string _leftSelectedText;

        /// <summary>
        /// 右侧文本
        /// </summary>
        [ObservableProperty]
        private FlowDocument _rightDocument;

        [ObservableProperty]
        private string _rightText;

        /// <summary>
        /// 选中的翻译源语言
        /// </summary>
        private string _selectedSourceLanguage;
        public string SelectedSourceLanguage
        {
            get => _selectedSourceLanguage;
            set
            {
                _selectedSourceLanguage = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 选中的翻译目标语言
        /// </summary>
        private string _selectedDestLanguage;
        public string SelectedDestLanguage
        {
            get => _selectedDestLanguage;
            set
            {
                _selectedDestLanguage = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 文字识别类型集合
        /// </summary>
        public ObservableCollection<OCRTypeItem> OCRTypes { get; set; }

        /// <summary>
        /// 文字识别语言类型集合
        /// </summary>
        public ObservableCollection<string> OCRLangTypes { get; set; }

        /// <summary>
        /// 身份证正反面集合
        /// </summary>
        public ObservableCollection<string> IdCardSides { get; set; }

        /// <summary>
        /// 翻译源语言集合
        /// </summary>
        public ObservableCollection<string> SourceLanguages { get; set; }

        /// <summary>
        /// 翻译目标语言集合
        /// </summary>
        public ObservableCollection<string> DestLanguages { get; set; }

        /// <summary>
        /// TTS发音人集合
        /// </summary>
        public ObservableCollection<string> TtsPersons { get; set; }

        /// <summary>
        /// 底部面板可见性
        /// </summary>
        [ObservableProperty]
        private Visibility _bottomGridVisibility = Visibility.Visible;

        /// <summary>
        /// 进度条值
        /// </summary>

        [ObservableProperty]
        private double _progressValue;
        #endregion


        /// <summary>
        /// 消息处理器
        /// </summary>
        /// <param name="recipient"></param>
        /// <param name="message"></param>
        private void MessageHandle(object recipient, DictionaryMessage message)
        {
            // 从CameraWindow发来的消息
            if (message.Value.TryGetValue(MessageTokens.CameraWindow, out object? value))
            {
                if (value is Bitmap img)
                {
                    App.Current.Dispatcher.Invoke(() => NotifierHelper.ShowInformation("拍照识别"));
                    GlobeImage = img;
                    if (_optionViewData.IsSavePhoto)
                    {
                        BitmapHelper.SaveImage(img, ConfigData.PhotographDir);
                    }

                    StartOCRAsync();
                }
            }
            // 从OptionViewModel发来的消息
            else if (message.Value.TryGetValue(MessageTokens.OptionViewModel, out object? val))
            {
                // 更新百度key
                if (val is string str && str.Equals("BaiduKeyUpdate"))
                {
                    App.Current.Dispatcher.Invoke(() => NotifierHelper.ShowInformation("更新百度key"));
                    _baidu.BaiduKey = new BaiduKey()
                    {
                        ApiKey = _optionViewData.ApiKey,
                        SecretKey = _optionViewData.SecretKey,
                        AppId = _optionViewData.AppId,
                        Password = _optionViewData.Password
                    };
                }
            }
        }


        /// <summary>
        /// UserControl初始化
        /// </summary>
        [RelayCommand]
        private void Loaded()
        {
            // 添加监听方法，监听Windows消息
            // 创建 HwndSource 并附加消息处理程序
            var window = _windowService.Instance<MainWindow>();
            var source = HwndSource.FromHwnd(window.Handle);
            source.AddHook(WndProc);
        }

        /// <summary>
        /// 监听Windows消息
        /// 处理热键
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="handled"></param>
        /// <returns></returns>
        private nint WndProc(nint hwnd, int msg, nint wParam, nint lParam, ref bool handled)
        {
            if (msg == 0x0312)
            {
                // 防止热键重复触发
                if (DateTime.Now - _lastTime < TimeSpan.FromMilliseconds(300))
                {
                    return IntPtr.Zero;
                }
                _lastTime = DateTime.Now;
                // 标记处理了
                handled = true;
                HotKeyId hotKeyId = Enum.Parse<HotKeyId>(wParam.ToString());
                if (hotKeyId == HotKeyId.Screenshot)
                {
                    Screenshot();
                }
                else if (hotKeyId == HotKeyId.Photograph)
                {
                    Camera(true);
                }
                else if (hotKeyId == HotKeyId.FixedScreen)
                {
                    FixedScreenshot();
                }
                else if (hotKeyId == HotKeyId.Record)
                {

                }
            }
            else if (msg == WindowsMsg.ShowWindowMsg)
            {
                // 自定义消息，激活主窗口
                _windowService.Instance<MainWindow>().Activate();
                NotifierHelper.Show(Notification.Wpf.NotificationType.Information, "当前程序已运行", "");
            }
            return IntPtr.Zero;
        }


        /// <summary>
        /// 左侧面板命令
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="ArgumentException"></exception>
        [RelayCommand]
        private void LeftExecute(object obj)
        {
            string action = (string)obj;

            switch (action)
            {
                case "ImportSingle":
                    ImportSingle();
                    break;
                case "ImportBatch":
                    ImportBatch();
                    break;
                case "Export":
                    Export(LeftText);
                    break;
                case "Camera":
                    Camera();
                    break;
                case "Screenshot":
                    Screenshot();
                    break;
                case "Tts":
                    Tts(LeftText);
                    break;
                case "Asr":
                    Asr();
                    break;
                case "Qrcode":
                    Qrcode();
                    break;
                case "Translate":
                    TranslateAsync(LeftText);
                    break;
                case "Search":
                    ShowSearchWindow();
                    break;
                case "Retry":
                    RetryAsync();
                    break;
                default:
                    throw new ArgumentException("Unknown action", nameof(obj));
            }
        }


        /// <summary>
        /// 导入单个图片、pdf进行识别
        /// </summary>
        private void ImportSingle()
        {
            try
            {
                if (_openFileDialog.ShowDialog() == true)
                {
                    string selectedFilePath = _openFileDialog.FileName;
                    if (Path.GetExtension(_openFileDialog.FileName) == ".pdf")
                    {
                        StartOCR_Multiple(_openFileDialog.FileName, true);
                    }
                    else
                    {
                        FileToGlobeImg(_openFileDialog.FileName);
                        StartOCRAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                NotifierHelper.ShowError(ex);
            }
        }

        private void FileToGlobeImg(string imgPath)
        {
            // 防止文件被锁
            using FileStream stream = new(imgPath, FileMode.Open, FileAccess.Read);
            var img = new Bitmap(stream);
            GlobeImage = new Bitmap(img);
        }


        /// <summary>
        /// 批量OCR
        /// pdf识别文件路径为pdf文件
        /// 图片识别文件路径为图片文件夹
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="isPdf">是否为pdf</param>
        private void StartOCR_Multiple(string path, bool isPdf)
        {
            try
            {
                if (_ocrTokenSource != null)
                {
                    CancellTask();
                    NotifierHelper.ShowWarning("正在停止批量识别...");
                    return;
                }
                BottomGridVisibility = Visibility.Visible;
                _ocrTokenSource = new CancellationTokenSource();
                if (_optionViewData.IsAddToEndAfterRecognition == false)
                {
                    LeftText = "";
                }
                if (isPdf)
                {
                    StartOCR_PDF(path, _ocrTokenSource.Token);
                }
                else
                {
                    StartOCR_Images(path, _ocrTokenSource.Token);
                }
            }
            catch (Exception ex)
            {
                CancellTask();
                NotifierHelper.ShowError(ex);
            }
        }

        /// <summary>
        /// 取消批量识别任务
        /// </summary>
        private void CancellTask()
        {
            _ocrTokenSource?.Cancel();
            _ocrTokenSource = null;
            BottomGridVisibility = Visibility.Collapsed;
        }

        /// <summary>
        /// PDF识别
        /// </summary>
        /// <param name="fileName"></param>
        /// <exception cref="Exception"></exception>
        private void StartOCR_PDF(string fileName, CancellationToken ocrToken)
        {
            if (!PdfHelper.IsPDFile(fileName))
            {
                throw new Exception("不是PDF文件！");
            }

            _pdfToImage.GetImagesAsync(fileName, _optionViewData.PdfRecognitionDelay, ocrToken);
        }

        // pdf识别回调函数
        private async void PdfCallback(object? sender, GotOnePageEventArgs e)
        {
            await StartOCRAsync(e.Image, true);
            App.Current.Dispatcher.Invoke(() => AddLeftText($"\r\n", true));
            // 释放资源
            e.Image.Dispose();
            RefreshProgress(e.Current / (double)e.Total);
        }


        // 批量图片识别
        private void StartOCR_Images(string path, CancellationToken ocrToken)
        {
            if (!Directory.Exists(path))
            {
                throw new Exception("路径不存在");
            }

            Task.Run(async () =>
            {
                string[] images = PathHelper.GetImages(path);

                for (int i = 0; i < images.Length; i++)
                {
                    if (ocrToken.IsCancellationRequested)
                    {
                        break;
                    }

                    RefreshProgress(i / (double)(images.Length - 1));
                    try
                    {
                        App.Current.Dispatcher.Invoke(() => AddLeftText($"\r\n图片：{images[i]}\r\n", true));

                        using Bitmap bmp = BitmapHelper.BitmapFromFile(images[i]);

                        await StartOCRAsync(bmp, true);
                    }
                    catch (Exception ex)
                    {
                        string msg = $"识别图片：{Path.GetFileName(images[i])}出错\r\n{ex.Message}";
                        App.Current.Dispatcher.Invoke(() => NotifierHelper.ShowError(msg));
                    }
                    Thread.Sleep(_optionViewData.PdfRecognitionDelay);
                }
            }, ocrToken);
        }


        // 刷新进度条
        private void RefreshProgress(double percent)
        {
            if (percent >= 1)
            {
                // 识别完成，把token置空
                CancellTask();
                App.Current.Dispatcher.Invoke(() => NotifierHelper.ShowSuccess("批量识别完成！"));
            }
            ProgressValue = percent;
        }


        /// <summary>
        /// ocr 并把结果显示在ui上
        /// </summary>
        /// <param name="isBatch">是否为批量识别</param>
        /// <returns></returns>

        private async Task StartOCRAsync(Bitmap? img = null, bool isBatch = false)
        {
            string text;
            bool addEnd = _optionViewData.IsAddToEndAfterRecognition;
            bool isTranslate = _optionViewData.IsTranslateAfterRecognition;
            string mode = SelectedOCRType.ItemText;

            // 如果img=null则使用全局图片
            img ??= GlobeImage;

            try
            {
                if (img == null)
                {
                    throw new Exception("请选择图片！");
                }
                if (_baidu.BaiduKey.IsOcrEmpty)
                {
                    throw new Exception("请设置百度OCR key！");
                }
                if (mode.Contains("二维码"))
                {
                    text = QRCode.IdentifyQRCode(img);
                }
                else
                {
                    BaiduOCRParameter parameter = new BaiduOCRParameter()
                    {
                        Mode = mode,
                        LangType = SelectedLangType,
                        IdCardSide = SelectedIdCardSide,
                        Image = img,
                        DownloadedPath = ConfigData.OCRDir
                    };
                    text = await _baidu.OCRActionAsync(parameter);
                }

                await App.Current.Dispatcher.InvokeAsync(() =>
                  {
                      // 截图时按下Alt键将文本添加到末尾，或则取消将文本添加到末尾
                      if ((Keyboard.IsKeyDown(Key.LeftAlt)))
                      {
                          addEnd = !addEnd;
                      }
                  });

                if (isBatch)
                {
                    // 批量文件识别始终添加到末尾
                    addEnd = true;
                }

                AddLeftText(text, addEnd);

                if (_optionViewData.IsCopyAfterRecognition)
                {
                    System.Windows.Clipboard.SetText(text);
                }

                // 自动翻译
                if (isTranslate && !isBatch)
                {
                    await TranslateAsync(text);
                }
            }
            catch (Exception ex)
            {
                NotifierHelper.ShowError(ex);
            }
        }


        private void AddLeftText(string text, bool addEnd)
        {
            if (addEnd)  // 添加到末尾
            {
                LeftText += "\r\n" + text;
            }
            else
            {
                LeftText = text;
            }
        }

        private static string GetFileName(string path, string ext)
        {
            return path + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ext;
        }

        /// <summary>
        /// 导入多个图片进行识别
        /// </summary>
        private void ImportBatch()
        {
            try
            {
                if (_openFolderDialog.ShowDialog() == true)
                {
                    StartOCR_Multiple(_openFolderDialog.FolderName, false);
                }
            }
            catch (Exception ex)
            {
                NotifierHelper.ShowError(ex);
            }
        }


        /// <summary>
        /// 导出文本
        /// </summary>
        private void Export(string text)
        {
            try
            {
                if (text == null || text.Length == 0)
                {
                    throw new Exception("要保存的内容为空！");
                }
                if (_saveFileDialog.ShowDialog() == true)
                {
                    File.WriteAllText(_saveFileDialog.FileName, text);
                }
            }
            catch (Exception ex)
            {
                NotifierHelper.ShowError(ex);
            }
        }

        /// <summary>
        /// 拍照识别
        /// </summary>
        private void Camera(bool sendMsg = false)
        {
            try
            {
                // 用全局消息代替事件
                var cameraWindow = _services.GetRequiredService<CameraWindow>();
                if (cameraWindow.IsVisible)
                {
                    // cameraWindow.PhotographEvent += PhotoRecognition;
                    cameraWindow.Activate();
                }
                else
                {
                    cameraWindow.Show();
                }
                if (sendMsg)
                {
                    WeakReferenceMessenger.Default.Send(new DictionaryMessage(new Dictionary<string, object>() { { MessageTokens.HomeViewModel, "Photograph" } }), MessageTokens.CameraWindowViewModel);
                }
            }
            catch (Exception ex)
            {
                NotifierHelper.ShowError(ex);
            }
        }

        /// <summary>
        /// 截图识别
        /// </summary>
        private void Screenshot()
        {
            IntPtr handle = _windowService.Instance<MainWindow>().Handle;
            _windowService.Hide<MainWindow>();
            ScreenWindow screen = _services.GetRequiredService<ScreenWindow>();
            if (screen.IsVisible)
            {
                screen.Activate();
                return;
            }
            screen.Start(handle);
            _windowService.Show<MainWindow>();
        }


        /// <summary>
        /// 语音合成
        /// </summary>
        private void Tts(string text)
        {
            try
            {
                // 如果正在播放
                if (_playAudio.IsPlaying())
                {   // 则停止播放
                    _playAudio.CancelPlay();
                    return;
                }
                if (text == null || text.Length == 0)
                {
                    throw new Exception("要合成的内容为空！");
                }

                if (!Directory.Exists(ConfigData.TTSDir))
                {
                    Directory.CreateDirectory(ConfigData.TTSDir);
                }

                string fileName = GetFileName(ConfigData.TTSDir, ".wav");

                int spd = (int)SliderSpeed;
                string per = SelectedPerson;

                MsTts.MSSpeechSynthesis(text, spd, per, fileName);
                if (File.Exists(fileName))
                {
                    // 播放合成的音频文件
                    _playAudio.PlayAsync(fileName);
                }
            }
            catch (Exception ex)
            {
                _playAudio.CancelPlay();
                NotifierHelper.ShowError(ex);
            }
        }

        /// <summary>
        /// 语音识别
        /// </summary>
        private void Asr()
        {
            try
            {
                _windowService.Show<RecordWindow>();
       
            }
            catch (Exception ex)
            {
                NotifierHelper.ShowError(ex);
            }
        }

        /// <summary>
        /// 二维码
        /// </summary>
        private void Qrcode()
        {
            try
            {
                if (string.IsNullOrEmpty(LeftText))
                {
                    throw new ArgumentException("要生成二维码的内容为空！");
                }
                _windowService.Show<QrCodeWindow>();
                // 发送text消息到QrCodeWindow
                WeakReferenceMessenger.Default.Send(new TextMessage(LeftText), MessageTokens.QrCodeWindow);
            }
            catch (Exception ex)
            {
                NotifierHelper.ShowError(ex);
            }
        }

        /// <summary>
        /// 翻译并把结果显示在ui上
        /// </summary>
        private async Task TranslateAsync(string text)
        {
            IsRightPanelVisible = true;
            string from = SelectedSourceLanguage;
            string to = SelectedDestLanguage;
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            try
            {
                if (_baidu.BaiduKey.IsTranslateEmpty)
                {
                    throw new Exception("请设置百度翻译key！");
                }
                RightText = await _baidu.TranslateAsync(text, from, to);
            }
            catch (Exception ex)
            {
                NotifierHelper.ShowError(ex);
            }
        }

        /// <summary>
        /// 搜索
        /// </summary>
        private void ShowSearchWindow()
        {
            _windowService.Show<FindWindow>();
            WeakReferenceMessenger.Default.Send(new DictionaryMessage(new Dictionary<string, object>() { { MessageTokens.HomeViewModel, LeftSelectedText } }), MessageTokens.FindWindow);
        }


        /// <summary>
        /// 选择的文本发生变化命令
        /// </summary>
        /// <param name="obj"></param>
        [RelayCommand]
        private void SelectionChanged(object obj)
        {
            var richTextBox = (System.Windows.Controls.RichTextBox)obj;
            if (richTextBox.Selection.IsEmpty == false)
            {
                LeftSelectedText = richTextBox.Selection.Text;
            }
        }

        /// <summary>
        /// 重试
        /// </summary>
        private async Task RetryAsync()
        {
            await StartOCRAsync();
        }


        /// <summary>
        /// 右侧面板命令
        /// </summary>
        /// <param name="obj"></param>
        [RelayCommand]
        private async Task RightExecute(string obj)
        {
            switch (obj)
            {
                case "Hidden":
                    IsRightPanelVisible = false;
                    break;
                case "Tts":
                    Tts(RightText);
                    break;
                case "Translate":
                    await TranslateAsync(LeftText);
                    break;
                case "Switch":
                    ExchangeLanguage();
                    break;
                default:
                    throw new ArgumentException("command not found", nameof(obj));
            }

        }


        /// <summary>
        /// 交换两侧的翻译语言
        /// </summary>
        private void ExchangeLanguage()
        {
            string temp = SelectedSourceLanguage;
            if (!temp.Contains("自动检测") && !temp.Equals(SelectedDestLanguage))
            {
                SelectedSourceLanguage = SelectedDestLanguage;
                SelectedDestLanguage = temp;
            }
        }

        /// <summary>
        /// textbox右键菜单命令
        /// </summary>
        [RelayCommand]

        private void ContextMenu(object paras)
        {
            object[] arr = (object[])paras;
            string operation = (string)arr[0];
            var textbox = (System.Windows.Controls.RichTextBox)arr[1];
            switch (operation)
            {
                case "Undo":
                    textbox.Undo();
                    break;
                case "Cut":
                    // textbox.Cut()有bug
                    textbox.Cut();
                    break;
                case "Copy":
                    textbox.Copy();
                    break;
                case "Paste":
                    ClipboardHelper.ClearTextFormat();
                    textbox.Paste();
                    break;
                case "Delete":
                    textbox.Selection.Text = "";
                    break;
                case "SelectAll":
                    textbox.SelectAll();
                    break;
                case "Export":
                    Export(textbox.Selection.Text);
                    break;
                case "Translate":
                    TranslateAsync(textbox.Selection.Text);
                    break;
            }
        }




        /// <summary>
        /// 在UserControl中按下Ctrl+V粘贴图片
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        [RelayCommand]
        private void PreviewKeyDown(KeyEventArgs e)
        {

            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.V)
            {
                // 粘贴剪切板图片，自动识别
                IDataObject data = Clipboard.GetDataObject();
                if (data.GetDataPresent(DataFormats.Bitmap))
                {
                    GlobeImage = (Bitmap)data.GetData(DataFormats.Bitmap);
                    StartOCRAsync();
                    e.Handled = true;
                }
                else
                {
                    // 如果粘贴文本，清除格式
                    ClipboardHelper.ClearTextFormat();
                }


            }
        }


        // 固定截图
        private void FixedScreenshot()
        {
            _windowService.Hide<MainWindow>();
            try
            {
                if (_hotKeyViewData.Rect.Width <= 0 || _hotKeyViewData.Rect.Height <= 0)
                {
                    throw new Exception("请设置固定截图坐标！");
                }

                GlobeImage = ScreenShotHelper.CopyScreen(_hotKeyViewData.Rect.X, _hotKeyViewData.Rect.Y, _hotKeyViewData.Rect.Width, _hotKeyViewData.Rect.Height);

                if (_optionViewData.IsSaveScreenshotToLocal)
                {
                    BitmapHelper.SaveImage(GlobeImage, ConfigData.OCRDir);
                }

                if (_optionViewData.IsSaveScreenshotToClipboard)
                {
                    BitmapHelper.SaveToClipboard(GlobeImage);
                }

                StartOCRAsync();
            }
            catch (Exception ex)
            {
                NotifierHelper.ShowError(ex);
            }
            finally
            {
                _windowService.Show<MainWindow>();
            }
        }

    }
}
