using CommunityToolkit.Mvvm.Messaging;
using Screenshot_WPF.Api;
using Screenshot_WPF.Helper;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using 鹰眼OCR_Common.Messages;
using 鹰眼OCR_Common.Messages.MessageParam;
using Rectangle = System.Drawing.Rectangle;


namespace Screenshot_WPF
{
    /// <summary>
    /// ScreenWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ScreenWindow : Window
    {
        public ScreenWindow()
        {
            InitializeComponent();

            this.Topmost = true;
            this.IsVisibleChanged += ScreenWindow_IsVisibleChanged;
        }

        // 截图完成事件
        public event EventHandler<Bitmap>? CapturedEvent;

        private System.Windows.Point startPos;           // 起始点位置（相对于当前窗口左上角）
        private Rect selectedRect;   // 选择的区域大小坐标（坐标相对于当前窗口左上角） 
        [NotNull]
        private Bitmap screenImage;       // 截取的全屏图像
        private nint destWindowHandle;  // 要截图的目标窗口句柄
        private nint autoHandle;        // 自动框选的窗口句柄
        [NotNull]
        private WindowInfo[] visibleWindows;  // 可见窗口句柄

        /// <summary>
        /// 截图完成后的图像
        /// </summary>
        [MaybeNull]
        public Bitmap CaptureImage { get; private set; }

        /// <summary>
        /// 选择的截图区域，坐标为屏幕坐标
        /// </summary>
        public Rectangle CaptureRectangle { get; private set; }

        /// <summary>
        /// 显示截图窗口
        /// </summary>
        /// <param name="destWindowHandle">目标窗口句柄。如果为null，则是全屏截图</param>
        public void Start(IntPtr parent, IntPtr? destWindowHandle = null)
        {
            if (destWindowHandle != null && destWindowHandle != nint.Zero)
            {// 窗口截图
                this.destWindowHandle = destWindowHandle.Value;
                Rectangle rect = WinApi.GetWindowRect(this.destWindowHandle);
                this.Width = rect.Width;
                this.Height = rect.Height;
                this.Left = rect.X;
                this.Top = rect.Y;
            }
            else
            {// 全屏截图
                Rectangle rect = MouseApi.GetCurrentScreenRectangle();
                this.Top = rect.Top;
                this.Left = rect.Left;
                this.WindowState = WindowState.Maximized;
            }

            // 截取全屏图像
            screenImage = ScreenShotHelper.CopyScreen();
            var handle = screenImage.GetHbitmap();
            BitmapSource source = Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            imgCtl.Source = source;
            // 释放资源 重要！！！
            WinApi.DeleteObject(handle);

            // 获取桌面上所有的可见窗口，去父窗口句柄
            visibleWindows = WinApi.GetVisibleWindows(parent);
            this.ShowDialog();
        }


        // 按下esc键退出截图
        private void ScreenWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Cancel();
            }
        }


        // 取消截图
        private void Cancel()
        {
            Dispose();
            this.Close();
        }


        private void ScreenWindow_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Cancel();
        }

        private void ScreenWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is bool visible && visible)
            {
                Bord.Visibility = Visibility.Visible;
                ShowEllipses();
            }
            else
            {
                Bord.Visibility = Visibility.Collapsed;
                HideEllipses();
            }
        }


        // 开始截图
        private void ScreenWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPos = e.GetPosition(this); // 获取鼠标起点位置
            MainCanvas.CaptureMouse();  // 捕获鼠标
        }


        private void ScreenWindow_MouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Point point = Mouse.GetPosition(this);
            // 计算绘制坐标
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                selectedRect = new Rect(startPos, point);
                Bord.Margin = new Thickness(selectedRect.Left, selectedRect.Top, 0, 0);
                Bord.Height = selectedRect.Height;
                Bord.Width = selectedRect.Width;

                // 更新遮罩
                UpdateMask(selectedRect);
                DrawSmallRect(selectedRect);
            }
            else if (destWindowHandle == IntPtr.Zero)
            {
                // 不是 指定窗口截图 才会自动根据鼠标坐标选择窗口
                AutoDrawRect(point);
            }
        }


        // 更新遮罩
        private void UpdateMask(Rect clipRect)
        {
            Mask.Clip = new CombinedGeometry(GeometryCombineMode.Exclude,
                new RectangleGeometry(new Rect(0, 0, Mask.Width, Mask.Height)),
                new RectangleGeometry(clipRect));
        }


        // 绘制选择区域四个角落的小矩形
        private void DrawSmallRect(Rect selectedRect)
        {
            // 设置 Ellipse 的位置
            Canvas.SetLeft(Ellipse_LeftTop, selectedRect.Left - (Ellipse_RightTop.Width / 2d));
            Canvas.SetTop(Ellipse_LeftTop, selectedRect.Top - (Ellipse_RightTop.Height / 2d));

            Canvas.SetLeft(Ellipse_RightTop, selectedRect.Right - (Ellipse_RightTop.Width / 2d));
            Canvas.SetTop(Ellipse_RightTop, selectedRect.Top - (Ellipse_RightTop.Height / 2d));

            Canvas.SetLeft(Ellipse_RightBottom, selectedRect.Right - (Ellipse_RightBottom.Width / 2d));
            Canvas.SetTop(Ellipse_RightBottom, selectedRect.Bottom - (Ellipse_RightBottom.Height / 2d));

            Canvas.SetLeft(Ellipse_LeftBottom, selectedRect.Left - (Ellipse_LeftBottom.Width / 2d));
            Canvas.SetTop(Ellipse_LeftBottom, selectedRect.Bottom - (Ellipse_LeftBottom.Height / 2d));
        }


        // 自动查找鼠标所在的窗口并绘制窗口矩形
        private void AutoDrawRect(System.Windows.Point mousePos)
        {
            var p = new System.Drawing.Point((int)mousePos.X, (int)mousePos.Y);
            Rectangle windowRect = Rectangle.Empty;
            foreach (var info in visibleWindows)
            {
                if (info.Rect.Contains(p))
                {
                    autoHandle = info.Handle;
                    windowRect = info.Rect;
                    break;
                }
            }

            Rectangle ClientRectangle = GetClientRectangle();
            // 避免截图区域超出屏幕
            windowRect.Intersect(ClientRectangle);

            // 绘制蓝色边框
            Bord.Margin = new Thickness(windowRect.Left, windowRect.Top, 0, 0);
            Bord.Height = windowRect.Height;
            Bord.Width = windowRect.Width;

            var rect = new Rect(windowRect.X, windowRect.Y, windowRect.Width, windowRect.Height);
            UpdateMask(rect);
            DrawSmallRect(rect);
        }


        // 鼠标弹起，截图完成
        private void ScreenWindow_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect;// 目标矩形

            var mousePos = e.GetPosition(this);

            // 鼠标点击位置和弹起位置相同，自动选择窗口截图
            if (startPos.X == mousePos.X && startPos.Y == mousePos.Y && autoHandle != IntPtr.Zero)
            {
                rect = WinApi.GetWindowRect(autoHandle);// 自动选择窗口截图
            }
            else
            { // 手动拖动截图 
                int x = (int)selectedRect.X;
                int y = (int)selectedRect.Y;
                int width = (int)selectedRect.Width;
                int height = (int)selectedRect.Height;
                rect = new Rectangle(x, y, width, height);
            }

            var ClientRectangle = GetClientRectangle();
            rect.Intersect(ClientRectangle);

            // 在全屏图片上裁剪目标矩形
            using System.Drawing.Bitmap bmpImage = new(screenImage);
            CaptureImage = bmpImage.Clone(rect, bmpImage.PixelFormat);
            //CaptureImage.Save(@"C:\Users\Administrator\Desktop\2.png", System.Drawing.Imaging.ImageFormat.Png);

            CaptureRectangle = rect;

            this.Close();

            Dispose();

            ScreenshotParam param = new()
            {
                CaptureImage = CaptureImage,
                CaptureRectangle = CaptureRectangle,
                DestHandle = destWindowHandle,
            };
            WeakReferenceMessenger.Default.Send(new ScreenshotMessage(param), 鹰眼OCR_Common.Constants.MessageTokens.ScreenWindow);
            OnCapturedEvent();
        }


        // 引发 截图完成 事件
        private void OnCapturedEvent()
        {
            CapturedEvent?.Invoke(this, CaptureImage);
        }


        private Rectangle GetClientRectangle()
        {
            return new Rectangle((int)this.Left, (int)this.Top, (int)this.ActualWidth, (int)this.ActualHeight);
        }


        private void Dispose()
        {
            screenImage?.Dispose();
            // 释放鼠标捕获
            MainCanvas.ReleaseMouseCapture();
        }


        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void HideEllipses()
        {
            foreach (var child in MainCanvas.Children)
            {
                if (child is Ellipse ellipse)
                {
                    ellipse.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void ShowEllipses()
        {
            foreach (var child in MainCanvas.Children)
            {
                if (child is Ellipse ellipse)
                {
                    ellipse.Visibility = Visibility.Visible;
                }
            }
        }
    }
}

