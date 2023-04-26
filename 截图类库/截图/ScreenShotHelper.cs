using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenShot
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
            Size bmpSize = new(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);
            //// 创建一个和屏幕一样大的空白图片
            //Bitmap bmp = new(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height);
            //using (Graphics g = Graphics.FromImage(bmp)) // 把屏幕图片拷贝到创建的空白图片中
            //{
            //    g.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(Screen.AllScreens[0].Bounds.Width, Screen.AllScreens[0].Bounds.Height));
            //}
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
                g.CopyFromScreen(x, y, 0, 0, new Size(w, h));
            return bmp;
        }

        /// <summary>
        /// 根据窗口句柄拷贝整个窗口
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static Bitmap CopyWindowByHandle(IntPtr handle)
        {
            Rectangle rect = WinApi.GetWindowSizeByHandle(handle);
            return CopyScreen(rect.X, rect.Y, rect.Width, rect.Height);
        }
    }
}
