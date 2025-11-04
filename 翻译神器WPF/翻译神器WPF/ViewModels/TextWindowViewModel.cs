using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using 翻译神器WPF.Common;
using 翻译神器WPF.Models;
using 翻译神器WPF.Service;
using 翻译神器WPF.Util;

namespace 翻译神器WPF.ViewModels
{
    public partial class TextWindowViewModel : ObservableObject
    {
        private readonly IWindowService _windowService;
        private readonly DispatcherTimer _hideTimer;
        private readonly int _timerInterval = 500; // 轮询间隔，毫秒
        private double _totalSeconds = 0; // 总时间，秒

        // 是否临时改变属性
        private bool _isTemporarilyChangingProperty = false;

        private TextWindowSnap? _snap;

        private bool _isShowTitleBar;
        public bool IsShowTitleBar
        {
            get => _isShowTitleBar;
            set
            {
                _isShowTitleBar = value;
                OnPropertyChanged();
                if (!value)
                {
                    TitleHeight = 0;
                }
                else
                {
                    TitleHeight = 30;
                }
            }
        }


        [ObservableProperty]
        private SettingsData _settingsData;

        [ObservableProperty]
        private string _text = "TextWindowTextWindowTextWindow";

        [ObservableProperty]
        private double _maxWindowWidth;

        [ObservableProperty]
        private double _maxWindowHeight;

        [ObservableProperty]
        private double _minWindowWidth;

        [ObservableProperty]
        private double _minWindowHeight;

        [ObservableProperty]
        private double _titleHeight = 30;

        // 默认可调整大小
        [ObservableProperty]
        private ResizeMode _resizeMode = ResizeMode.CanResize;

        [ObservableProperty]
        private SizeToContent _sizeToContent = SizeToContent.Manual;


        [RelayCommand]
        private void ContextMenu(string paras)
        {
            switch (paras)
            {
                case "Hide":
                    _hideTimer.Stop();
                    _windowService.Hide<TextWindow>();
                    break;
                case "Topmost":
                    SettingsData.IsTextWindowTopmost = !SettingsData.IsTextWindowTopmost;
                    break;
                case "Copy":
                    CopyToClipboard();
                    //复制后仍然按剩余时间隐藏，不复位计时
                    break;
                default:
                    break;
            }
        }

        [RelayCommand]
        private void MouseRightButtonUp()
        {
            if (!IsShowTitleBar)
            {
                _windowService.Hide<TextWindow>();
            }
        }

        [RelayCommand]
        private void DoubleClickText()
        {
            CopyToClipboard();
        }

        [RelayCommand]
        private void IsVisibleChanged(DependencyPropertyChangedEventArgs e)
        {
            if (_isTemporarilyChangingProperty)
            {
                RestoreSnap();
                _isTemporarilyChangingProperty = false;
            }
        }


        public TextWindowViewModel()
        {
        }

        public TextWindowViewModel(IServiceProvider serviceProvider, IWindowService windowService)
        {
            _hideTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(_timerInterval) };
            _hideTimer.Tick += HideTimer_Tick;

            _windowService = windowService;
            _settingsData = serviceProvider.GetRequiredService<ConfigData>().SettingsData;

            // 设置最大宽度高度
            var screenSize = MonitorHelpler.GetPrimaryScreenSize();
            MaxWindowWidth = screenSize.Width * 0.5;
            MaxWindowHeight = screenSize.Height * 0.5;

            //#if DEBUG
            //            // 浮动模式
            //            //IsShowTitleBar = true;
            //            //SettingsData.TextWindowTop = double.NaN;
            //            //SettingsData.TextWindowLeft = double.NaN;

            //            // 屏幕顶部模式
            //            IsShowTitleBar = false;
            //            SizeToContent = SizeToContent.WidthAndHeight;
            //            SettingsData.TextWindowTop = 0;
            //            //var textWindow = _windowService.Instance<TextWindow>();
            //            double x = (screenSize.Width / 2.0 - SettingsData.TextWindowWidth / 2.0);  // 在顶部中间显示 x坐标=（屏幕宽度/2 - 窗口宽度/2）
            //            SettingsData.TextWindowLeft = x;
            //#endif

            // 注册显示窗口消息
            WeakReferenceMessenger.Default.Register<TextMessage, string>(this, MsgTokens.TextWindow, (r, m) =>
            {
                Text = m.Text;
                ShowWindow();
            });

            // 注册属性改变消息
            WeakReferenceMessenger.Default.Register<TextWindowChangedMessage, string>(
                this,
                MsgTokens.TextWindow,
                (r, m) =>
                {
                    if (!_isTemporarilyChangingProperty)
                    {
                        CreateSnap();
                        _isTemporarilyChangingProperty = true;
                    }
                    WindowPropertyChanged();
                });

            // 窗口可见性改变消息，从TextWindow的IsVisibleChanged触发
            WeakReferenceMessenger.Default.Register<VisibilityChangedMessage, string>(this, MsgTokens.TextWindow, (r, m) =>
            {
                if (!m.NewValue && _isTemporarilyChangingProperty)
                {
                    RestoreSnap();
                    _isTemporarilyChangingProperty = false;
                }
            });
        }

        void CreateSnap()
        {
            _snap = new TextWindowSnap
            {
                DelayTime = SettingsData.DelayTime,
                Top = SettingsData.TextWindowTop,
                Left = SettingsData.TextWindowLeft,
                WindowLocation = SettingsData.SelectedTextWindowLocation,
                Height = SettingsData.TextWindowHeight,
                Width = SettingsData.TextWindowWidth,
                Topmost = SettingsData.IsTextWindowTopmost,
            };
        }

        void RestoreSnap()
        {
            if (_snap != null)
            {
                SettingsData.TextWindowTop = _snap.Top;
                SettingsData.TextWindowLeft = _snap.Left;
                SettingsData.SelectedTextWindowLocation = _snap.WindowLocation;
                SettingsData.TextWindowHeight = _snap.Height;
                SettingsData.TextWindowWidth = _snap.Width;
                SettingsData.IsTextWindowTopmost = _snap.Topmost;
            }
        }

        /// <summary>
        /// 在调整窗口属性时，短暂改变窗口样式
        /// </summary>
        private void WindowPropertyChanged()
        {
            // 设置为浮动
            SettingsData.SelectedTextWindowLocation = WindowLocations.Floating;
            Text = "测试文字\r\n测试文字\r\n测试文字\r\n测试文字\r\n测试文字\r\n";
            var mw = _windowService.Instance<MainWindow>();
            SettingsData.TextWindowTop = mw.Top;
            SettingsData.TextWindowLeft = mw.Left + mw.ActualWidth + 5;
            SettingsData.TextWindowHeight = 200;
            SettingsData.TextWindowWidth = 300;
            SettingsData.IsTextWindowTopmost = true;
            // 显示窗口
            ShowWindow();
        }

        private void ShowWindow()
        {
            // 翻译后延迟显示的时间（秒），<=0 时不启动定时器
            if (SettingsData.DelayTime > 0)
            {
                StartHideTimer();
            }


            string mode = SettingsData.SelectedTextWindowLocation;
            if (mode == WindowLocations.Floating)
            {
                IsShowTitleBar = true;
                // 浮动模式允许调整大小
                ResizeMode = ResizeMode.CanResize;
                MinWindowHeight = 150;
                MinWindowWidth = 100;
                SizeToContent = SizeToContent.Manual;

                _windowService.Show<TextWindow>();
            }
            else
            {
                SettingsData.IsTextWindowTopmost = true;
                IsShowTitleBar = false;
                ResizeMode = ResizeMode.NoResize;
                MinWindowHeight = 50;
                MinWindowWidth = 100;

                // 切换多次 SizeToContent 触发重新测量
                SizeToContent = SizeToContent.WidthAndHeight;
                SizeToContent = SizeToContent.Manual;
                SizeToContent = SizeToContent.WidthAndHeight;

                //// 2) 在渲染优先级强制一次布局+渲染，确保 ActualWidth/Height 立即更新
                //Application.Current.Dispatcher.Invoke(() =>
                //{
                //    var win = _windowService.Instance<TextWindow>();
                //    if (win != null)
                //    {
                //        win.InvalidateMeasure();
                //        win.InvalidateArrange();
                //        win.InvalidateVisual();
                //        win.UpdateLayout();
                //    }
                //}, DispatcherPriority.Render);
                _windowService.Show<TextWindow>();
                SettingsData.TextWindowTop = 0;
                var textWindow = _windowService.Instance<TextWindow>();
                var screenSize = MonitorHelpler.GetPrimaryScreenSize();
                double x = (screenSize.Width / 2.0 - textWindow.ActualWidth / 2.0);  // 在顶部中间显示 x坐标=（屏幕宽度/2 - 窗口宽度/2）
                SettingsData.TextWindowLeft = x;

            }
        }

        private void StartHideTimer()
        {
            _hideTimer.Stop();
            _hideTimer.Start();
            _totalSeconds = 0;
        }

        private void HideTimer_Tick(object? sender, EventArgs e)
        {
            _totalSeconds += (_timerInterval / 1000.0);
            // 计时到达时，如按着 Ctrl，则继续显示，直到松开 Ctrl 再隐藏
            if (_totalSeconds >= (SettingsData.DelayTime) && !IsCtrlPressed())
            {
                _hideTimer.Stop();
                _windowService.Hide<TextWindow>();
                return;
            }
        }

        private static bool IsCtrlPressed()
        {
            return Keyboard.IsKeyDown(Key.LeftCtrl) ||
                   (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control;
        }

        private void CopyToClipboard()
        {
            try
            {
                Clipboard.SetText(Text);
            }
            catch
            { }
        }
    }
}
