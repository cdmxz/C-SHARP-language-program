using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenShot
{
    /// <summary>
    /// 截图类
    /// </summary>
    // 避免出现“仅在windows上受支持”警告
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:验证平台兼容性", Justification = "<挂起>")]
    public partial class FrmScreenShot : Form
    {
        /// <summary>
        /// 截图完成委托
        /// </summary>
        /// <param name="img"></param>
        public delegate void CapturedDelegate(Image img);
        /// <summary>
        /// 截图完成事件
        /// </summary>
        public event CapturedDelegate? CapturedEvent;

        /// <summary>
        /// 全屏截图
        /// </summary>
        public FrmScreenShot()
        {
            SetWindowStyle();
            this.destWindowHandle = IntPtr.Zero;
            InitializeComponent();
        }

        /// <summary>
        /// 指定窗口截图
        /// </summary>
        public FrmScreenShot(IntPtr destWindowHandle)
        {
            SetWindowStyle();
            this.destWindowHandle = destWindowHandle;
            InitializeComponent();
        }

        private void SetWindowStyle()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除窗口背景  
            SetStyle(ControlStyles.DoubleBuffer, true);         // 双缓冲
            this.Cursor = Cursors.Cross;
            this.ShowInTaskbar = false;
            this.KeyPreview = true;
        }

        private Point startPos;           // 起始点位置（相对于当前窗口左上角）
        private Rectangle selectedRect;   // 选择的区域大小坐标（坐标相对于当前窗口左上角） 
        private Image? screenImage;       // 截取的全屏图像
        private readonly IntPtr destWindowHandle;  // 要截图的目标窗口句柄
        private IntPtr autoHandle;        // 自动框选的窗口句柄

        /// <summary>
        /// 截图完成后的图像
        /// </summary>
        public Image? CaptureImage { get; private set; }

        /// <summary>
        /// 选取的矩形大小和坐标（截图大小和坐标）
        /// </summary>
        public Rectangle SelectedRect
        {
            get { return selectedRect; }
        }

        /// <summary>
        /// 开始截图
        /// </summary> 
        public virtual DialogResult Start()
        {
            if (destWindowHandle != IntPtr.Zero)
            {
                // destWindowHandle不为空，则表示只针对某个窗口截图，而不是全屏截图
                WinApi.ShowWindowAndWait(destWindowHandle, WinApi.SW_SHOWNORMAL, 500);
                screenImage = ScreenShotHelper.CopyWindowByHandle(destWindowHandle);// 截取这个窗口的图片
            }
            else
            {
                screenImage = ScreenShotHelper.CopyScreen(); // 截取全屏图片
            }
            return this.ShowDialog();// 显示窗口
        }

        // 设置窗口大小
        private void FrmScreenShot_Load(object sender, EventArgs e)
        {
            if (destWindowHandle != IntPtr.Zero)
            {// 窗口截图
                Rectangle rect = WinApi.GetWindowRect(destWindowHandle);
                this.Size = rect.Size;
                this.Location = rect.Location;
            }
            else
            {// 全屏截图
                this.WindowState = FormWindowState.Maximized;
            }
        }

        // 鼠标右键按下退出截图
        private void FrmScreenShot_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                Cancel();
        }

        // 按下esc键退出截图
        private void FrmScreenShot_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (int)Keys.Escape)
                Cancel();
        }

        // 取消截图
        private void Cancel()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // 开始截图
        private void FrmScreenShot_MouseDown(object sender, MouseEventArgs e) => startPos = e.Location; // 获取鼠标起点位置

        // 把选择的矩形的大小坐标记录下来，并绘制选择的矩形
        private void FrmScreenShot_MouseMove(object sender, MouseEventArgs e)
        {
            LockMouseMove();
            if (screenImage == null)
            {
                Cancel();
                return;
            }
            // 计算绘制坐标
            if (e.Button == MouseButtons.Left)
            {
                selectedRect.X = e.Location.X > startPos.X ? startPos.X : e.Location.X;
                selectedRect.Y = e.Location.Y > startPos.Y ? startPos.Y : e.Location.Y;
                selectedRect.Width = Math.Abs(e.Location.X - startPos.X);
                selectedRect.Height = Math.Abs(e.Location.Y - startPos.Y);
            }
            else
            {
                autoHandle = WinApi.GetWindowHandleByPos(e.Location, this);
            }
            this.Invalidate();  // 使窗口重绘引发Paint事件
        }


        // 锁定鼠标光标
        private void LockMouseMove()
        {
            Cursor.Clip = new Rectangle(this.Location, this.Size);
        }

        // 窗体重绘事件
        private void FrmScreenShot_Paint(object sender, PaintEventArgs e)
        {
            if (screenImage == null)
            {
                Cancel();
                return;
            }
            // 将原图显示到窗体
            e.Graphics.DrawImage(screenImage, 0, 0, screenImage.Width, screenImage.Height);
            using (SolidBrush sb = new(Color.FromArgb(150, 0, 0, 0)))
                e.Graphics.FillRectangle(sb, ClientRectangle);
            // 对窗体显示的图片进行灰度处理
            if (MouseButtons == MouseButtons.Left)
                DrawSelectedRect(e.Graphics);// 绘制选择的矩形
            else
                AutoDrawRect(e.Graphics);// 自动绘制鼠标所在的窗口
        }

        // 自动查找鼠标所在的窗口并绘制窗口矩形
        private void AutoDrawRect(Graphics g)
        {
            if (screenImage == null)
                return;
            Rectangle rect = WinApi.GetWindowRect(autoHandle);
            rect.Intersect(this.ClientRectangle);// 避免截图区域超出屏幕
            using (Pen pen = new(Color.Cyan, 3))
            {
                g.DrawImage(screenImage, rect, rect, GraphicsUnit.Pixel);
                g.DrawRectangle(pen, rect.X, rect.Y, rect.Width - 1, rect.Height - 1);
            }
            string str = $"鼠标坐标：{MousePosition.X},{MousePosition.Y}\r\n窗口坐标：{rect.X},{rect.Y}\r\n窗口大小：{rect.Width}x{rect.Height}";
            Point displayPos = new(rect.Left, rect.Top);
            DrawStr(g, displayPos, str);
        }
        private void DrawStr(Graphics g, Point displayPos, string str)
        {
            Font font = new("微软雅黑", 10f);
            DrawStr(g, displayPos, str, font);
        }

        // 在指定位置绘制字符串
        private void DrawStr(Graphics g, Point displayPos, string str, Font font)
        {
            Size strSize = g.MeasureString(str, font).ToSize();// 字体大小
            // 确定显示坐标
            displayPos.Y = (displayPos.Y <= 0) ? 5 : displayPos.Y;
            displayPos.X = (displayPos.X + strSize.Width > this.Width) ? this.Width - strSize.Width : displayPos.X;
            displayPos.X = (displayPos.X <= 0) ? 5 : displayPos.X;
            // 在指定坐标绘制字体
            using SolidBrush Brush = new(Color.FromArgb(125, 0, 0, 0));
            // 在矩形内部填充半透明黑色
            g.FillRectangle(Brush, new Rectangle(displayPos, strSize));
            g.DrawString(str, font, Brushes.Orange, displayPos);
        }

        // 绘制四周的小矩形
        private void DrawSmallRect(Graphics g)
        {
            RectangleF[] rects = new RectangleF[]// 四周的小矩形参数
                {
                    new RectangleF(selectedRect.Left - 2.5F, selectedRect.Top - 2.5F, 5, 5),// 左上角小矩形
                    new RectangleF(selectedRect.Right / 2F + selectedRect.Left / 2F - 2.5F, selectedRect.Top - 2.5F, 5, 5),// 顶部中间小矩形
                    new RectangleF(selectedRect.Right - 2.5F, selectedRect.Top - 2.5F, 5, 5), // 右上角小矩形
                    new RectangleF(selectedRect.Right - 2.5F, selectedRect.Bottom / 2F + selectedRect.Top / 2F - 2.5F, 5, 5),
                    new RectangleF(selectedRect.Right - 2.5F, selectedRect.Bottom - 2.5F, 5, 5),
                    new RectangleF(selectedRect.Right / 2F + selectedRect.Left / 2F - 2.5F, selectedRect.Bottom - 2.5F, 5, 5),
                    new RectangleF(selectedRect.Left - 2.5F, selectedRect.Bottom - 2.5F, 5, 5),
                    new RectangleF(selectedRect.Left - 2.5F, selectedRect.Bottom / 2F + selectedRect.Top / 2F - 2.5F, 5, 5)
                };
            using SolidBrush brush = new SolidBrush(Color.FromArgb(30, 144, 255));
            for (int i = 0; i < rects.Length; i++)
            {
                g.FillRectangle(brush, rects[i]);
                g.DrawRectangle(Pens.DodgerBlue, rects[i].X, rects[i].Y, rects[i].Width, rects[i].Height);
            }
        }

        // 绘制鼠标拖拽选择的矩形
        private void DrawSelectedRect(Graphics g)
        {
            if (screenImage == null)
                return;
            Font font = new("微软雅黑", 10f);
            g.DrawImage(screenImage, selectedRect, srcRect: selectedRect, GraphicsUnit.Pixel);
            g.DrawRectangle(Pens.DodgerBlue, selectedRect.Left, selectedRect.Top, selectedRect.Width - 1, selectedRect.Height - 1);
            // 绘制四周的小矩形
            DrawSmallRect(g);
            string str;
            if (destWindowHandle != IntPtr.Zero)  // 是指定窗口截图
            {
                Point mousePoint = WinApi.ScreenToClient(destWindowHandle, MousePosition);
                Point p = WinApi.ClientToScreen(this.Handle, this.startPos);
                Point startPos = WinApi.ScreenToClient(destWindowHandle, WinApi.ClientToScreen(this.Handle, this.startPos));
                str = string.Format($"按鼠标右键或ESC取消\n起始坐标(相对):{startPos.X},{startPos.Y} 绝对{p.X},{p.Y}" +
                     $"鼠标坐标(相对):{mousePoint.X},{mousePoint.Y}\n截图大小:{selectedRect.Width}x{selectedRect.Height}");
            }
            else // 不是指定窗口截图
            {
                str = string.Format($"按鼠标右键或ESC取消\n起始坐标:{startPos.X},{startPos.Y} " +
                  $"鼠标坐标:{MousePosition.X},{MousePosition.Y}\n截图大小:{selectedRect.Width}x{selectedRect.Height}");
            }
            Size size = g.MeasureString(str, font).ToSize();// 字体大小
            Point displayPos = new(selectedRect.Left, selectedRect.Top - size.Height - 5);
            DrawStr(g, displayPos, str, font);// 绘制文字
        }

        // 鼠标弹起，截图完成

        private void FrmScreenShot_MouseUp(object sender, MouseEventArgs e)
        {
            if (screenImage == null)
            {
                Cancel();
                return;
            }

            if (e.Button != MouseButtons.Left)
                return;
            this.Invalidate(); // 使窗口的整个画面无效并重绘控件
            Rectangle rect;// 目标矩形
            if (startPos.X == e.X && startPos.Y == e.Y && autoHandle != IntPtr.Zero)
                rect = WinApi.GetWindowRect(autoHandle);// 自动选择窗口截图
            else
                rect = selectedRect;     // 手动拖动截图 
            rect.Intersect(this.ClientRectangle);
            try
            {
                // 在全屏图片上裁剪目标矩形
                using (Bitmap bmpImage = new(screenImage))
                    CaptureImage = bmpImage.Clone(rect, bmpImage.PixelFormat);
                //CaptureImage.Save("0.png", ImageFormat.Png);
                if (destWindowHandle != IntPtr.Zero)
                    selectedRect = new Rectangle(WinApi.ScreenToClient(destWindowHandle, WinApi.ClientToScreen(this.Handle, rect.Location)), rect.Size);
                else
                    selectedRect = rect;
            }
            catch { CaptureImage = null; }
            this.Close();
            this.DialogResult = DialogResult.OK;
            OnCapturedEvent();
        }

        // 引发 截图完成 事件
        private void OnCapturedEvent()
        {
            if (CaptureImage == null)
                return;
            CapturedEvent?.Invoke((Image)CaptureImage.Clone());
        }

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer? components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                screenImage?.Dispose();
                CaptureImage?.Dispose();
            }
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FrmScreenShot_Shown(object sender, EventArgs e)
        {
            // 顶置窗口
            WinApi.SetWindowPos(Handle, new IntPtr(-1), 0, 0, 0, 0, WinApi.SWP_NOSIZE | WinApi.SWP_NOMOVE);
        }
    }
}
