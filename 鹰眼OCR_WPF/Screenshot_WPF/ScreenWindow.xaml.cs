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
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using Point = System.Windows.Point;


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

#if DEBUG
            this.Topmost = false;
#else
            this.Topmost = true;    
#endif
            this.Topmost = true;
            this.IsVisibleChanged += ScreenWindow_IsVisibleChanged;
        }

        /// <summary>
        /// 截图完成事件
        /// </summary>
        public event EventHandler<Bitmap>? CapturedEvent;

        private Point startPos;           // 起始点位置（相对于当前窗口左上角）
        private Rect selectedRect;   // 选择的区域大小坐标（坐标相对于当前窗口左上角） 
        [NotNull]
        private Bitmap screenImage;       // 截取的全屏图像
        private nint destWindowHandle;  // 要截图的目标窗口句柄
        private nint autoHandle;        // 自动框选的窗口句柄
        [NotNull]
        private WindowInfo[] visibleWindows;  // 可见窗口句柄
        private double dpiScale = 1.0; // dpi缩放比例
        private const int MagnifierSize = 100;   // 放大镜像素显示大小（逻辑）
        private const int MagnifierZoom = 3;     // 放大倍数

        // 是否发送截图完成消息
        private bool sendCapturedMessage;

        /// <summary>
        /// 截图完成后的图像
        /// </summary>
        [NotNull]
        public Bitmap CaptureImage { get; private set; }

        /// <summary>
        /// 选择的截图区域，坐标为屏幕坐标
        /// </summary>
        public System.Drawing.Rectangle CaptureRectangle { get; private set; }


        /// <summary>
        /// 显示截图窗口，在截图完成之后会发送截图完成消息到目标窗口
        /// </summary>
        /// <param name="destWindowHandle">目标窗口句柄。如果为null，则是全屏截图</param>
        public void Show(IntPtr parent, IntPtr? destWindowHandle = null)
        {
            sendCapturedMessage = true;
            this.destWindowHandle = destWindowHandle ?? IntPtr.Zero;
            Show(parent);
        }

        /// <summary>
        /// Displays the window without sending a screenshot completion message to the destination window.
        /// </summary>
        /// <param name="parent">A handle to the parent window that will own the displayed window.</param>
        /// <param name="destWindowHandle">An optional handle to the destination window. If not specified, no destination window is set.</param>
        public void ShowWithoutMessage(IntPtr parent, IntPtr? destWindowHandle = null)
        {
            // 不会发送截图完成消息
            sendCapturedMessage = false;
            this.destWindowHandle = destWindowHandle ?? IntPtr.Zero;
            Show(parent);
        }

        private void Show(IntPtr parent)
        {
            Rect rect;
            if (this.destWindowHandle != nint.Zero)
            {// 窗口截图
                rect = WinApi.GetWindowRect(this.destWindowHandle);
                this.Width = rect.Width;
                this.Height = rect.Height;
                this.Left = rect.X;
                this.Top = rect.Y;
            }
            else
            {// 全屏截图
                rect = MouseApi.GetCurrentScreenRect();
                this.Top = rect.Top;
                this.Left = rect.Left;
                this.Width = rect.Width;
                this.Height = rect.Height;
                this.WindowState = WindowState.Maximized;
            }

            // 获取屏幕DPI缩放比例
            dpiScale = VisualTreeHelper.GetDpi(this).DpiScaleX;

            // 截取全屏图像
            screenImage = ScreenShotHelper.CopyScreen(rect);
            //screenImage.Save("C:\\Users\\Administrator\\Desktop\\1.png", System.Drawing.Imaging.ImageFormat.Png);
            var handle = screenImage.GetHbitmap();
            BitmapSource source = Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            imgCtl.Source = source;
            imgCtl.Width = rect.Width / dpiScale;
            imgCtl.Height = rect.Height / dpiScale;

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

        private void ScreenWindow_KeyDown(object sender, KeyEventArgs e)
        {
            var pos = Mouse.GetPosition(this);

            switch (e.Key)
            {
                case Key.Left:
                    // 向左移动光标1个像素
                    MouseApi.SetCursor((int)((pos.X - 1) * dpiScale), (int)(pos.Y * dpiScale));
                    break;
                case Key.Right:
                    // 向右移动光标1个像素
                    MouseApi.SetCursor((int)((pos.X + 1) * dpiScale), (int)(pos.Y * dpiScale));
                    break;
                case Key.Up:
                    // 向上移动光标1个像素
                    MouseApi.SetCursor((int)(pos.X * dpiScale), (int)((pos.Y - 1) * dpiScale));
                    break;
                case Key.Down:
                    // 向下移动光标1个像素
                    MouseApi.SetCursor((int)(pos.X * dpiScale), (int)((pos.Y + 1) * dpiScale));
                    break;
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
            Point point = Mouse.GetPosition(this);

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
                selectedRect = GetRectFromMousePoint(point);
                // 不是 指定窗口截图 才会自动根据鼠标坐标选择窗口
                AutoDrawRect(selectedRect);
            }
            UpdateMagnifier(); // 使用当前鼠标位置更新放大镜
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
        private void AutoDrawRect(Rect windowRect)
        {
            // 绘制蓝色边框
            Bord.Margin = new Thickness(windowRect.Left, windowRect.Top, 0, 0);
            Bord.Height = windowRect.Height;
            Bord.Width = windowRect.Width;

            UpdateMask(windowRect);
            DrawSmallRect(windowRect);
        }


        // 鼠标弹起，截图完成
        private void ScreenWindow_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Rect rect;// 目标矩形

            var mousePos = e.GetPosition(this);

            // 鼠标点击位置和弹起位置相同，则表示自动选择窗口截图
            if (startPos.X == mousePos.X && startPos.Y == mousePos.Y && autoHandle != IntPtr.Zero)
            {
                rect = GetRectFromMousePoint(mousePos);
            }
            else
            { // 手动拖动截图 
                rect = selectedRect;
            }

            // 避免截图区域超出屏幕
            var ClientRect = GetClientRect();
            rect.Intersect(ClientRect);

            // 逻辑坐标转换为屏幕坐标
            rect.X *= dpiScale;
            rect.Y *= dpiScale;
            rect.Width *= dpiScale;
            rect.Height *= dpiScale;

            System.Drawing.Rectangle rectangle = new((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
            // 做一下校验，确保宽高大于0
            if (rect.Width > 0 && rect.Height > 0)
            {
                // 在全屏图片上裁剪目标矩形
                using Bitmap bmpImage = new(screenImage);
                CaptureImage = bmpImage.Clone(rectangle, bmpImage.PixelFormat);
                //CaptureImage.Save(@"C:\Users\Administrator\Desktop\2.png", System.Drawing.Imaging.ImageFormat.Png);
                CaptureRectangle = rectangle;
                ScreenshotParam param = new()
                {
                    CaptureImage = CaptureImage,
                    CaptureRect = CaptureRectangle,
                    DestHandle = destWindowHandle,
                };
                if (sendCapturedMessage)
                {
                    // 发送截图完成消息
                    WeakReferenceMessenger.Default.Send(new ScreenshotMessage(param), 鹰眼OCR_Common.Constants.MessageTokens.ScreenWindow);
                }
                OnCapturedEvent();
            }
            this.Close();
            Dispose();
        }


        // 引发 截图完成 事件
        private void OnCapturedEvent()
        {
            CapturedEvent?.Invoke(this, CaptureImage);
        }

        private void Dispose()
        {
            screenImage?.Dispose();
            // 释放鼠标捕获
            MainCanvas.ReleaseMouseCapture();
        }


        private void ScreenWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private Rect GetClientRect()
        {
            return new Rect(
                    this.Left,
                    this.Top,
                    this.Width,
                    this.Height
                );
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


        private Rect ToScaleRect(Rect rect)
        {
            return new(
                    (rect.X / dpiScale),
                    (rect.Y / dpiScale),
                    (rect.Width / dpiScale),
                    (rect.Height / dpiScale));
        }

        /// <summary>
        /// 获取鼠标坐标所在窗口的矩形
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        private Rect GetRectFromMousePoint(Point mousePos)
        {
            Rect windowRect = Rect.Empty;
            foreach (var info in visibleWindows)
            {
                // mousePoint使用的是逻辑坐标，visibleWindows是物理坐标，
                // 窗口物理坐标转为逻辑坐标
                Rect scaleRect = ToScaleRect(info.Rect);

                if (scaleRect.Contains(mousePos))
                {
                    info.Childrens.Add(info); // 把自己也加入进去，方便后面处理
                    foreach (var child in info.Childrens)
                    {
                        scaleRect = ToScaleRect(child.Rect);
                        if (scaleRect.Contains(mousePos))
                        {
                            autoHandle = child.Handle;
                            windowRect = scaleRect;
                            break;
                        }
                    }
                    if (windowRect == Rect.Empty)
                    {
                        autoHandle = info.Handle;
                        windowRect = ToScaleRect(info.Rect);
                    }
                    break;
                }
            }

            // 避免截图区域超出屏幕
            Rect ClientRectangle = GetClientRect();
            windowRect.Intersect(ClientRectangle);
            return windowRect;
        }

        // 更新放大镜
        private void UpdateMagnifier()
        {
            if (imgCtl.Source is not BitmapSource bs)
            {
                MagnifierPanel.Visibility = Visibility.Collapsed;
                return;
            }

            // 鼠标相对于 imgCtl 的逻辑坐标
            var pos = Mouse.GetPosition(imgCtl);

            // 计算 imgCtl 上图片的实际显示区域（考虑 Stretch=Uniform），得到 displayWidth/Height 与偏移和缩放
            double controlW = imgCtl.ActualWidth;
            double controlH = imgCtl.ActualHeight;
            if (controlW <= 0 || controlH <= 0)
            {
                MagnifierPanel.Visibility = Visibility.Collapsed;
                return;
            }

            double imgAspect = (double)bs.PixelWidth / bs.PixelHeight;
            double controlAspect = controlW / controlH;
            double displayW, displayH, offsetX, offsetY;
            if (controlAspect > imgAspect)
            {
                displayH = controlH;
                displayW = imgAspect * displayH;
                offsetX = (controlW - displayW) / 2.0;
                offsetY = 0;
            }
            else
            {
                displayW = controlW;
                displayH = displayW / imgAspect;
                offsetX = 0;
                offsetY = (controlH - displayH) / 2.0;
            }


            // 把 imgCtl 的显示坐标映射到位图像素坐标
            double relX = (pos.X - offsetX) / displayW;
            double relY = (pos.Y - offsetY) / displayH;
            int pixelX = (int)(relX * bs.PixelWidth);
            int pixelY = (int)(relY * bs.PixelHeight);

            int cropSize = Math.Max(1, (int)(MagnifierSize / (double)MagnifierZoom));
            int half = cropSize / 2;
            int x = Math.Max(0, pixelX - half);
            int y = Math.Max(0, pixelY - half);
            int w = Math.Min(bs.PixelWidth - x, cropSize);
            int h = Math.Min(bs.PixelHeight - y, cropSize);
            if (w <= 0 || h <= 0)
            {
                MagnifierPanel.Visibility = Visibility.Collapsed;
                return;
            }

            // 裁剪并放大
            var cropped = new CroppedBitmap(bs, new Int32Rect(x, y, w, h));
            var zoomed = new TransformedBitmap(cropped, new ScaleTransform((double)MagnifierZoom, (double)MagnifierZoom));
            MagnifierImage.Source = zoomed;

            // 将放大镜面板放在 Canvas 上（相对于 MainCanvas），避免超出边界
            var mouseOnCanvas = Mouse.GetPosition(MainCanvas);
            double left = mouseOnCanvas.X + 10;
            double top = mouseOnCanvas.Y + 10;
            if (left + MagnifierPanel.ActualWidth > MainCanvas.ActualWidth)
                left = mouseOnCanvas.X - 20 - MagnifierPanel.ActualWidth;
            if (top + MagnifierPanel.ActualHeight > MainCanvas.ActualHeight)
                top = mouseOnCanvas.Y - 20 - MagnifierPanel.ActualHeight;

            Canvas.SetLeft(MagnifierPanel, left);
            Canvas.SetTop(MagnifierPanel, top);

            // 更新信息文本：鼠标屏幕坐标和当前截图大小
            var screenX = (int)(pixelX);
            var screenY = (int)(pixelY);

            // 计算当前选区（逻辑坐标）并转换为像素
            var selRect = selectedRect;
            selRect.X *= dpiScale;
            selRect.Y *= dpiScale;
            selRect.Width *= dpiScale;
            selRect.Height *= dpiScale;
            string infoText = $"X:{screenX} Y:{screenY}\r\nSize:{(int)selRect.Width}x{(int)selRect.Height}";

            MagnifierInfoText.Text = infoText;

            MagnifierPanel.Visibility = Visibility.Visible;
        }

        // 隐藏放大镜
        private void HideMagnifier() => MagnifierPanel.Visibility = Visibility.Collapsed;


    }
}

