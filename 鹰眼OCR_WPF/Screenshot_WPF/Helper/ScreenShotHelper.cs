using Screenshot_WPF.Api;
using System.Drawing;


namespace Screenshot_WPF.Helper
{
    /// <summary>
    /// 截图助手类
    /// </summary>
    public class ScreenShotHelper
    {
        /// <summary>
        /// 拷贝整个屏幕
        /// </summary>
        /// <returns></returns>
        public static Bitmap CopyScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            Size bmpSize = new((int)screenWidth, (int)screenHeight);
            return CopyScreen(0, 0, bmpSize.Width, bmpSize.Height);
        }

        /// <summary>
        /// 拷贝部分屏幕
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <returns></returns>
        public static Bitmap CopyScreen(int x, int y, int w, int h)
        {
            Bitmap bmp = new(w, h);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(x, y, 0, 0, new Size(w, h));
            }

            return bmp;
        }

        /// <summary>
        /// 根据窗口句柄拷贝整个窗口
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static Bitmap CopyWindowByHandle(nint handle)
        {
            Rectangle rect = WinApi.GetWindowSizeByHandle(handle);
            return CopyScreen(rect.X, rect.Y, rect.Width, rect.Height);
        }
    }
}
